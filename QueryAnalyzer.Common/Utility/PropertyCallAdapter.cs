using System;
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
		private static readonly Dictionary<string, IPropertyCallAdapter<TThis>> _instances =
            new Dictionary<string, IPropertyCallAdapter<TThis>>();

        public static IPropertyCallAdapter<TThis> GetInstance(string forPropertyName)
        {
            IPropertyCallAdapter<TThis> instance;
            if (!_instances.TryGetValue(forPropertyName, out instance))
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
                instance = Activator
                    .CreateInstance(concreteAdapterType, getterInvocation)
                        as IPropertyCallAdapter<TThis>;

                _instances.Add(forPropertyName, instance);
            }

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
