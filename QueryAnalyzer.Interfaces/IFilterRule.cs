using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryAnalyzer.Interfaces
{
    public interface IFilterRule
    {
        string VariableName { get; }
        RuleOperator Operator { get; }
        string Operands { get; }
        RuleCombinationOperator RuleCombinationOperator { get; }
    }
}
