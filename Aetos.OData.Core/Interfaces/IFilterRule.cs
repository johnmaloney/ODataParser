using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aetos.Filtering.Core;

namespace Aetos.Filtering.Core
{
    public interface IFilterRule
    {
        string ColumnName { get; set; }
        RuleOperator Operator { get; set; }
        string Operands { get; set; }
        FilterRuleOperator CombinationOperator { get; set; }
    }
}
