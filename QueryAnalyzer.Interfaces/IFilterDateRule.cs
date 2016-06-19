using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryAnalyzer.Interfaces
{
    public interface IFilterDateRule : IFilterRule
    {
        DateTime DateOperands { get; set; }
        DateTypeOperator DateType { get; set; }
    }
}
