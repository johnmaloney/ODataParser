using QueryAnalyzer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryAnalyzer.Common.Filters
{
    public class DateTimePipe : AFilterPipe
    {

        #region Fields



        #endregion

        #region Properties



        #endregion

        #region Methods

        public DateTimePipe()
        {

        }

        public DateTimePipe(string filterStatement)
        {

        }

        public override void Filter(string filteredText)
        {
            while (HasNextPipe)
                Next<AFilterPipe>().Filter(filteredText, FilterRule);
        }

        #endregion
    }
}
