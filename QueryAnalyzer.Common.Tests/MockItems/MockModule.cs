using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueryAnalyzer.Common.Criteria;
using QueryAnalyzer.Interfaces;

namespace QueryAnalyzer.Common.Tests.MockItems
{
    internal class MockModule : IModule
    {
        public IFilterRuleCriteria BuildCriteria(string statement)
        {
            return new FilterRuleCriteria() { FilterStatement = statement };
        }

        public IConsumer Consume(IFilterRuleCriteria filterCriteria)
        {
            var mockConsumer = new MockConsumer(filterCriteria);
            mockConsumer.BuildRules();
            return mockConsumer;
        }
    }
}
