using QueryAnalyzer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryAnalyzer.Common.Filters
{
    /// <summary>
    /// Discovers the difference between an incoming query whether it is a DateTime
    /// type or a numrice/text type.
    /// </summary>
    public class FilterRuleDiscoveryPipe : AFilterPipe
    {
        #region Fields



        #endregion

        #region Properties



        #endregion

        #region Methods

        public FilterRuleDiscoveryPipe(IFilterRuleCriteria filterRuleCriteria)
            :base(filterRuleCriteria)
        { 
        }

        public override void Filter(string filteredText)
        {
            bool hasDayMonthYear = Strategy.For<IRegex>().DayMonthYear.IsMatch(filteredText);
            bool hasExplicitDate = Strategy.For<IRegex>().Date.IsMatch(filteredText);

            if (hasDayMonthYear | hasExplicitDate)
            {
                this.filterRule = new DateFilterRule();
            }
            else
            {
                this.filterRule = new DefaultFilterRule();
            }

            while (HasNextPipe)
                Next<AFilterPipe>().Filter(filteredText, FilterRule);
        }

        #endregion
    }
}
