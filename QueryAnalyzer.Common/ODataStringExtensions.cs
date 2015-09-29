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

        /// <summary>
        /// This provides support for removing unneeded text that is encapsulated within quotes.
        /// For example the string tolower('PrimaryKey') ne 'Google Operations' becomes tolower('PrimaryKey') ne
        /// </summary>
        /// <param name="objectWithQuotes"></param>
        /// <returns></returns>
        public static string RemoveQuotedText(this string objectWithQuotes)
        {
            var originalTextArray = objectWithQuotes.ToCharArray();
            var newTextWithoutQuotes = new List<string>();
            bool isOperationInsideQuotes = false;

            // Get the quotes location up front ensues that we only have the first and last, while //
            // accounting for the possibility of internal quotes within quotes i.e. 'the moon is 'out of this world' tonight' //
            int lastIndexOfQuotes = objectWithQuotes.LastIndexOf("\'");
            int firstIndexOfQuotes = objectWithQuotes.IndexOf("\'");
            var isOpeningQuote = new Predicate<int>((index) => { return index == firstIndexOfQuotes; });
            var isClosingQuote = new Predicate<int>((index) => { return index == lastIndexOfQuotes; });

            for (int i = 0; i < originalTextArray.Length; i++)
            {
                string currentValue = originalTextArray[i].ToString();
                if (currentValue == "'" && isOpeningQuote(i))
                {
                    isOperationInsideQuotes = true;
                }
                else if (currentValue == "'" && isOperationInsideQuotes && isClosingQuote(i))
                {
                    isOperationInsideQuotes = false;
                }
                else if(isOperationInsideQuotes)
                {
                    // Intentionaly left blank to allow for the removal of the internally quoted words //
                }
                else
                {
                    isOperationInsideQuotes = false;
                }

                // when we are outside the quoted strings then add the value //
                if (!isOperationInsideQuotes && currentValue != "'")
                    newTextWithoutQuotes.Add(currentValue);
            }
            return string.Join("", newTextWithoutQuotes);
        }
    }
}
