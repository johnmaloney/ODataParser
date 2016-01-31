using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryAnalyzer.Interfaces
{
    public interface IAnalyzerStrategy : IStrategy
    {
        IModule Default { get; set; }
        IEnumerable<IFilterRule> Analyze(string statement);
    }
}
