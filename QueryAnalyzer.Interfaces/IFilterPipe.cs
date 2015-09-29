using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryAnalyzer.Interfaces
{
    public interface IFilterPipe
    {
        void Filter(string filterStatement);
    }
}
