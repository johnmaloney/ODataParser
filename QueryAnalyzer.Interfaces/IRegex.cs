using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QueryAnalyzer.Interfaces
{
    public interface IRegex : IStrategy
    {
        Regex DayMonthYear { get; }
        Regex MonthYear { get; }
        Regex Year { get; }
        Regex AndConjunction { get; }
        Regex OrConjunction { get; }
        string AndConjunctionPattern { get; }
        string OrConjunctionPattern { get; }
    }
}
