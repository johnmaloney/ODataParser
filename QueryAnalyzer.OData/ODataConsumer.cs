using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueryAnalyzer.Common;
using QueryAnalyzer.Interfaces;
using QueryAnalyzer.OData.ODataPipeline;

namespace QueryAnalyzer.OData
{
    public class ODataConsumer : IConsumer
    {
        #region Fields

        private readonly IFilterRuleCriteria filterRuleCriteria;

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

        public ODataConsumer(IFilterRuleCriteria criteria)
        {
            this.filterRuleCriteria = criteria;
        }

        public List<IFilterRule> BuildRules()
        {
            var compiledFilterRules = new List<IFilterRule>();
            if (!string.IsNullOrEmpty(filterRuleCriteria.FilterStatement))
            {
                var primaryPipeline = new FindReplacePipe();
                var originalFilterStatement = primaryPipeline.Filter(filterRuleCriteria.FilterStatement);

                var sections = originalFilterStatement.SplitByConjunction(filterRuleCriteria.FilterLogic);

                for (int i = 0; i < sections.Count(); ++i)
                {
                    var secondaryPipeline =
                        new FilterRuleDiscoveryPipe(sections.ElementAt(i), criteria.ClientFilterConditions[i])
                            .Add(new DateTimePipe()
                            .Add(new ColumnNamePipe()
                            .Add(new OperatorTypePipe()
                            .Add(new OperandsPipe()))));

                    secondaryPipeline.Filter(sections.ElementAt(i));

                    secondaryPipeline.FilterRule.CombinationOperator = String.Equals(criteria.FilterLogic.ToLowerInvariant(),
                                                                           FilterRuleOperator.OR.ToString(),
                                                                           StringComparison.InvariantCultureIgnoreCase)
                        ? FilterRuleOperator.OR
                        : FilterRuleOperator.AND;

                    compiledFilterRules.Add(secondaryPipeline.FilterRule);
                }

                //if (!string.IsNullOrEmpty(criteria.OrderBy) & !string.IsNullOrEmpty(criteria.OrderByColumnName))
                //{
                //    compiledFilterRules.Add(new FilterRule() { ColumnName = criteria.OrderByColumnName, Operator = RuleOperator.Sort, Operands = criteria.OrderBy });
                //}
            }

            return compiledFilterRules;
        }

        #endregion
    }
}
