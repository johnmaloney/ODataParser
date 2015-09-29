using QueryAnalyzer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryAnalyzer.Common.Filters
{
    public class VariableNamePipe : AFilterPipe
    {
        #region Fields



        #endregion

        #region Properties



        #endregion

        #region Methods

        public override void Filter(string filteredText)
        {
            var defaultVariableName = Strategy.For<IRegex>().DefaultVariableName;
            var numericVariableName = Strategy.For<IRegex>().NumericVariableName;
            string variableName = string.Empty;

            // Sometime there is white space in the text, we need to remove it //
            filteredText = filteredText.Trim();

            // Verify that the 'tolower' is prepended onto the columnName //
            if (defaultVariableName.IsMatch(filteredText))
            {
                variableName = defaultVariableName.Match(filteredText).Value.Trim('(', ')');
            }
            else if (numericVariableName.IsMatch(filteredText)) // In the case of this being a numeric column the 'tolower' is not prepended to the columnName //
            {
                variableName = numericVariableName.Match(filteredText).Value;
            }
            else
                throw new ApplicationException("The VariableName was not found on the filteredText in the VariableNameFilter.");

            filteredText = filteredText.Replace(variableName, string.Empty);
            FilterRule.VariableName = variableName;

            while (HasNextPipe)
                Next<AFilterPipe>().Filter(filteredText, FilterRule);
        }

        #endregion
    }
}
