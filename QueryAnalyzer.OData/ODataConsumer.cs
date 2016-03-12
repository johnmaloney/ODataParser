using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueryAnalyzer.Common.Filters;
using QueryAnalyzer.Common;
using QueryAnalyzer.Interfaces;
using QueryAnalyzer.Modules.OData.ODataPipeline;

namespace QueryAnalyzer.Modules.OData
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
            // Collect each individual rule that exists in the incoming filter statement //
            var compiledFilterRules = new List<IFilterRule>();
            if (!string.IsNullOrEmpty(filterRuleCriteria.FilterStatement))
            {
                // Used to replace the seperators in a date time structure. //
                var primaryPipeline = new FindReplacePipe();

                // Get the new filter statement after the Find and replace is executed //
                var originalFilterStatement = primaryPipeline.Filter(filterRuleCriteria.FilterStatement);

                // Break the filter statement into seperate clauses to be individually parsed //
                var sections = originalFilterStatement.SplitByConjunction(filterRuleCriteria.FilterLogic);

                // Iterate and build the section filters //
                for (int i = 0; i < sections.Count(); ++i)
                {
                    // Build a Filter pipeline by combining in order the processing objects //
                    var secondaryPipeline = new FilterRuleDiscoveryPipe(filterRuleCriteria);
                    secondaryPipeline.Add(new DateTimePipe()
                                    .Add(new VariableNamePipe()
                                    .Add(new OperatorTypePipe()
                                    .Add(new OperandsPipe()))));

                    secondaryPipeline.Filter(sections.ElementAt(i));
                    
                    compiledFilterRules.Add(secondaryPipeline.FilterRule);
                }

                if (!string.IsNullOrEmpty(filterRuleCriteria.OrderBy) & !string.IsNullOrEmpty(filterRuleCriteria.OrderByColumnName))
                {
                    compiledFilterRules.Add(new DefaultFilterRule() { VariableName = filterRuleCriteria.OrderByColumnName, Operator = RuleOperator.Sort, Operands = filterRuleCriteria.OrderBy });
                }
            }

            return compiledFilterRules;
        }

        #endregion
    }
}
