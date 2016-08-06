using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryAnalyzer.Interfaces
{
    public interface IPropertyCallAdapter<TThis>
    {
        object InvokeGet(TThis @this);
        //add void InvokeSet(TThis @this, object value) if necessary
    }
}
