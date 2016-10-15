using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using QueryAnalyzer.Interfaces;

namespace QueryAnalyzer.Common.Utility
{
    /// <summary>
    /// Adaptation of the following concepts
    /// https://codeblog.jonskeet.uk/2008/08/09/making-reflection-fly-and-exploring-delegates/comment-page-1/
    /// http://stackoverflow.com/questions/26731159/fastest-way-for-get-value-of-a-property-reflection-in-c-sharp
    /// Working Fiddle:
    /// https://dotnetfiddle.net/fFZ0fO
    /// </summary>
    /// <typeparam name="TThis"></typeparam>
    public class PropertyProvider<TThis>
	{
        public static int PropertyCallVolume = 0;

        public static int TypeCallVolume = 0;
        /// <summary>
        ///  This is a collection of type and that types properties. 
        ///  Example Dictionary of CountryObject, Dictionary of Country.Name and PropertyDelegate
        /// </summary>
		private static readonly ConcurrentDictionary<Type, Dictionary<string, IPropertyCallAdapter<TThis>>> _instances =
            new ConcurrentDictionary<Type, Dictionary<string, IPropertyCallAdapter<TThis>>>();

        private struct PropertyContainer
        {
            public string PropertyName;

            public IPropertyCallAdapter<TThis> PropertyCallAdapter;
        }

        public static IPropertyCallAdapter<TThis> GetInstance(string forPropertyName)
        {
            var type = typeof(TThis);
            Dictionary<string, IPropertyCallAdapter<TThis>> objectProperties;

            // Does the Type Dictionary contain the TThis instance //
            if (_instances.TryGetValue(type, out objectProperties))
            {
                // The Dictionary Found the TYPE but it 
                if (objectProperties == null)
                {
                    // It obviously wont have the properties so return the value //
                    objectProperties = new Dictionary<string, IPropertyCallAdapter<TThis>>();
                    var propertyDelegate = getPropertyCallAdapter(forPropertyName);
                    objectProperties.Add(forPropertyName, propertyDelegate);

                    // Store it //
                    _instances[type] = objectProperties;

                    // Just return this, no need to do anymore searching //
                    return propertyDelegate;
                }

                // Does the TThis Dictionary entry contain the Property //
                if (!objectProperties.ContainsKey(forPropertyName))
                {
                    var propertyDelegate = getPropertyCallAdapter(forPropertyName);

                    objectProperties.Add(forPropertyName, propertyDelegate);
                    return propertyDelegate;
                }

                // If we are here it means the following two things //
                // 1. The objectProperties collection was not null //
                // 2. The objectProperties collection contains a delegate for this propertyName//
                return objectProperties[forPropertyName];                  
            }
            else
            {                 
                //lock (_instances)
                //{
                    TypeCallVolume++;

                    IPropertyCallAdapter<TThis> instance = getPropertyCallAdapter(forPropertyName);
                 
                    // Type is not stored in the Dictionary //
                    _instances.TryAdd(type, new Dictionary<string, IPropertyCallAdapter<TThis>>() { { forPropertyName, instance } });
                    
                    return instance;
                //}
            }

            // If we make it here there is a major malfunction //
            throw new InvalidOperationException(string.Format("The retrieval of the Property value {0} from the Type {1} Failed.", forPropertyName, type.Name));
        }

        private static IPropertyCallAdapter<TThis> getPropertyCallAdapter(string forPropertyName)
        {
            var property = typeof(TThis).GetProperty(
                    forPropertyName,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            MethodInfo getMethod;
            Delegate getterInvocation = null;
            if (property != null && (getMethod = property.GetGetMethod(true)) != null)
            {
                var openGetterType = typeof(Func<,>);
                var concreteGetterType = openGetterType
                    .MakeGenericType(typeof(TThis), property.PropertyType);

                getterInvocation =
                    Delegate.CreateDelegate(concreteGetterType, null, getMethod);
            }
            else
            {
                //throw exception or create a default getterInvocation returning null
            }

            var openAdapterType = typeof(PropertyCallAdapter<,>);

            var concreteAdapterType = openAdapterType
                .MakeGenericType(typeof(TThis), property.PropertyType);

            var instance = Activator
                       .CreateInstance(concreteAdapterType, getterInvocation)
                           as IPropertyCallAdapter<TThis>;

            PropertyCallVolume++;
            return instance;
        }
    }

    public class PropertyCallAdapter<TThis, TResult> : IPropertyCallAdapter<TThis>
    {
        private readonly Func<TThis, TResult> _getterInvocation;

        public PropertyCallAdapter(Func<TThis, TResult> getterInvocation)
        {
            _getterInvocation = getterInvocation;
        }

        public object InvokeGet(TThis @this)
        {
            return _getterInvocation.Invoke(@this);
        }
    }
}
