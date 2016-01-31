using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueryAnalyzer.Interfaces;

namespace QueryAnalyzer.Common.Tests.MockItems
{
    internal class MockConsumer : IConsumer
    {

        #region Fields

        private readonly IFilterRuleCriteria criteria;

        #endregion

        #region Properties

        public IEnumerable<IFilterRule> Rules
        {
            get
            {
                return this.BuildRules();
            }
        }

        #endregion

        #region Methods

        public MockConsumer(IFilterRuleCriteria criteria)
        {
            this.criteria = criteria;
        }

        public IEnumerable<IFilterRule> BuildRules()
        {
            return new List<IFilterRule>();
        }

        #endregion
    }
}
