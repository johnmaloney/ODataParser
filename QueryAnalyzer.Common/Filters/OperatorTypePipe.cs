using QueryAnalyzer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace QueryAnalyzer.Common.Filters
{
    public class OperatorTypePipe : AFilterPipe
    {
        #region Fields

        private readonly Dictionary<string, RuleOperator> ruleOperations = RuleOperatorExtensions.RuleOperations();

        #endregion

        #region Properties



        #endregion

        #region Methods

        /// <summary>
        /// Assumption is made that this filter is working with the text that has had the variable
        /// name removed.
        /// </summary>
        /// <param name="filteredText"></param>
        public override void Filter(string filteredText)
        {
            // Remove the quoted text to ensure there is no confusion between the operator and operands //
            // for example tolower() ne 'Sunrise Energy Conglomerate' would confuse the ne in the operator //
            // and the ne in the word Energy so we remove the internal to the quotes to avoid this //
            var nonQuotedText = filteredText.RemoveQuotedText();
            var ruleOperatorCheck = Strategy.For<IRegex>().OperatorCheck;
            var regex = Strategy.For<IRegex>().Operator;

            if (ruleOperatorCheck.IsMatch(nonQuotedText))
                regex = Strategy.For<IRegex>().OperatorAlt;

            var combinedRuleOperatorMatches = new List<string>();
            var textToMatch = nonQuotedText.Contains("null") ? nonQuotedText.Replace(" ", string.Empty) : nonQuotedText.Trim();
            foreach (Match matchingRuleOperator in regex.Matches(textToMatch))
            {
                if (matchingRuleOperator.Success)
                    combinedRuleOperatorMatches.Add(matchingRuleOperator.Value.Replace(" ", string.Empty));
            }

            var ruleOperatorKey = string.Concat(combinedRuleOperatorMatches.Distinct().ToArray());

            if (ruleOperations.ContainsKey(ruleOperatorKey))
                FilterRule.Operator = ruleOperations[ruleOperatorKey];


            while (HasNextPipe)
                Next<AFilterPipe>().Filter(filteredText, FilterRule);
        }

        #endregion
    }
}
