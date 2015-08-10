using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using QueryAnalyzer.Interfaces;

namespace QueryAnalyzer.Common
{
    public static class ODataStringExtensions
    {
        public static IEnumerable<string> SplitByConjunction(this string originalODataStatement, string filterRuleOperator)
        {
            var andConjunctionRegex = Strategy.For<IRegex>().AndConjunction;
            var orConjunctionRegex = Strategy.For<IRegex>().OrConjunction;
            var ruleOperator = (RuleCombinationOperator)Enum.Parse(typeof(RuleCombinationOperator), filterRuleOperator, true);

            switch (ruleOperator)
            {
                case RuleCombinationOperator.AND:
                    return Regex.Split(originalODataStatement, 
                        Strategy.For<IRegex>().AndConjunctionPattern, 
                        RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.ExplicitCapture);

                case RuleCombinationOperator.OR:
                    return Regex.Split(originalODataStatement, 
                        Strategy.For<IRegex>().OrConjunctionPattern, 
                        RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.ExplicitCapture);
            }
            throw new NotSupportedException("The conjuction type of: " + filterRuleOperator.ToString() + " is not supported");
        }
    }
}
