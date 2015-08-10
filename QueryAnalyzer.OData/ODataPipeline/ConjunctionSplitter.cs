using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueryAnalyzer.Interfaces;

namespace QueryAnalyzer.OData.ODataPipeline
{
    public class ConjunctionSplitter : AFilterPipe
    {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Methods

        public override void Filter(string filteredText)
        {

            while (HasNextPipe)
                Next<AFilterPipe>().Filter(filteredText, FilterRule);
        }

        #endregion
    }
}
