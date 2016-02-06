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
        Regex DefaultVariableName { get; }
        Regex NumericVariableName { get; }


        Regex OperatorCheck { get; }
        Regex Operator { get;}
        Regex OperatorAlt { get;  }

        Regex TextOperands { get; }
        Regex NumericOperands { get; }

        Regex Date { get; }
        Regex DateValue { get; }
        Regex DayMonthYear { get; }
        Regex MonthYear { get; }
        Regex Year { get; }
        Regex AndConjunction { get; }
        Regex OrConjunction { get; }
        string AndConjunctionPattern { get; }
        string OrConjunctionPattern { get; }
    }
}
