using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryAnalyzer.Interfaces
{
    public interface IModule
    {
        IFilterRuleCriteria BuildCriteria(string statement);
        IConsumer Consume(IFilterRuleCriteria filterCriteria);
    }
}
