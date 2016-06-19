using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryAnalyzer.Interfaces
{
    public enum DateTypeOperator
    {
        Unknown = 0,
        FullDate = 1,
        DayOnly = 2,
        MonthOnly = 3,
        YearOnly = 4,
        MonthYear = 5
    }
}
