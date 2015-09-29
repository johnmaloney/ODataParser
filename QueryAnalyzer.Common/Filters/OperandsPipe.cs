using QueryAnalyzer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryAnalyzer.Common.Filters
{
    public class OperandsPipe : AFilterPipe
    {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Methods

        public override void Filter(string filteredText)
        {
            var textOperands = Strategy.For<IRegex>().TextOperands;
            var numericOperands = Strategy.For<IRegex>().NumericOperands;

            // Trim the whitespace from the beginning and the end to ensure good reg ex matching //
            filteredText = filteredText.Trim();

            if (textOperands.IsMatch(filteredText))
                FilterRule.Operands = this.GetTextOperands(filteredText);

            else if (numericOperands.IsMatch(filteredText))
                FilterRule.Operands = this.GetNumericOperands(filteredText);

            while (HasNextPipe)
                Next<AFilterPipe>().Filter(filteredText, FilterRule);
        }

        /// <summary>
        /// Exposes the RegEx for getting a numeric value. 
        /// Matches a numeric value such as 'eq 10', it will find the value 10
        /// </summary>
        public string GetNumericOperands(string filteredText)
        {
            return Strategy.For<IRegex>().NumericOperands.Match(filteredText).Value.Trim();
        }

        /// <summary>
        /// Exposes the RegEx for getting a numeric value, will parse to an integer.
        /// Matches a numeric value such as 'eq 10', it will find the value 10
        /// </summary>
        public int GetNumericOperandsNumber(string filteredText)
        {
            if (!Strategy.For<IRegex>().NumericOperands.IsMatch(filteredText))
                throw new NotSupportedException("The filtered text did not have a match for numeric values.");

            return int.Parse(Strategy.For<IRegex>().NumericOperands.Match(filteredText).Value.Trim());
        }

        /// <summary>
        /// Exposes the RegEx for getting a numeric value. 
        /// Matches the 'a' in the filter expression
        /// </summary>
        public string GetTextOperands(string filteredText)
        {
            // Return the whole statement MINUS the first and last single quotes // 
            string foundOperandWithQuotes = Strategy.For<IRegex>().TextOperands.Match(filteredText).Value;
            string sansFirstQuote = foundOperandWithQuotes.Remove(foundOperandWithQuotes.IndexOf('\''), 1);
            string sansExteriorQuotes = sansFirstQuote.Remove(sansFirstQuote.LastIndexOf('\''));

            return sansExteriorQuotes;
        }
        #endregion
    }
}
