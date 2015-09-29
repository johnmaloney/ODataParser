using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryAnalyzer.Interfaces
{
    public interface IFilterRule
    {
        string VariableName { get; set; }
        RuleOperator Operator { get; set; }
        string Operands { get; set; }
        RuleCombinationOperator RuleCombinationOperator { get; set; }
    }
}
