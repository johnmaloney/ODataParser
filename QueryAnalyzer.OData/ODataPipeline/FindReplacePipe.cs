using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using QueryAnalyzer.Common;
using QueryAnalyzer.Interfaces;

namespace QueryAnalyzer.OData.ODataPipeline
{
    public class FindReplacePipe
    {        
        public string Filter(string filteredText)
        {
            // The reason for this filter check is to allow for the DateTime text to not be split into parsable //
            // items when iterating, ex = Deposits gt 10 and day(BranchOpened) eq 1 and month(BranchOpened) eq 10 and year(BranchOpened) eq 2014 //
            // should be parsed into only two sections, by replacing the 'and' words it allows this to occur. //
            if (Strategy.For<IRegex>().DayMonthYear.IsMatch(filteredText) | 
                Strategy.For<IRegex>().MonthYear.IsMatch(filteredText) | 
                Strategy.For<IRegex>().Year.IsMatch(filteredText))
            {
                filteredText = filteredText.Replace("and month", "&&& month");
                filteredText = filteredText.Replace("and year", "&&& year");
                filteredText = filteredText.Replace("or month", "||| month");
                filteredText = filteredText.Replace("or year", "||| year");
            }
            return filteredText;
        }
    }
}
