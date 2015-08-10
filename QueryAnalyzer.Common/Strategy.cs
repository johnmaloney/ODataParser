using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueryAnalyzer.Interfaces;

namespace QueryAnalyzer.Common
{
    public static class Strategy
    {
        private static ConcurrentDictionary<Type, IStrategy> domainStrategies;

        internal static ConcurrentDictionary<Type, IStrategy> DomainStrategies
        {
            get
            {
                if (Strategy.domainStrategies == null)
                    Strategy.domainStrategies = Strategy.DefineStrategyStore();

                return Strategy.domainStrategies;
            }
        }

        public static T For<T>() where T : class, IStrategy
        {
            IStrategy policy;

            if (Strategy.DomainStrategies.TryGetValue(typeof(T), out policy))
                return policy as T;
            else
                throw new ApplicationException("The policy of type " + typeof(T).ToString() + " was not available.");
        }

        internal static ConcurrentDictionary<Type, IStrategy> DefineStrategyStore()
        {
            var strategyStore = new ConcurrentDictionary<Type, IStrategy>();
            return strategyStore;
        }

        public static void AddStrategy<T>(T policyInstance) where T : IStrategy
        {
            Strategy.DomainStrategies.AddOrUpdate(typeof(T), policyInstance, (key, oldValue) => oldValue = policyInstance);
        }
    }
}
