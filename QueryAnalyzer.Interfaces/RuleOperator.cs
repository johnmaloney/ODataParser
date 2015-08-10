using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryAnalyzer.Interfaces
{
    public enum RuleOperator
    {
        Default,
        Equal,
        LessThan,
        GreaterThan,
        LessThanEqual,
        GreaterThanEqual,
        NotEqual,
        In,
        Between,
        NotIn,
        Always,
        Never,
        Sort,
        StartsWith,
        EndsWith,
        Contains,
        DoesNotContain,
        IsDBNull,
        IsNotDBNull
    }
}
