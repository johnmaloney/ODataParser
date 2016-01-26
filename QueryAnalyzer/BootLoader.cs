using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueryAnalyzer.Interfaces;
using QueryAnalyzer.Modules.OData;

namespace QueryAnalyzer
{
    public class BootLoader
    {
        #region Fields

        private readonly Dictionary<string, IModule> registeredModule = new Dictionary<string, IModule>();

        #endregion

        #region Properties



        #endregion

        #region Methods

        public BootLoader()
        {
            AnalyzerFactory.DefaultModule = new ODataModule();
        }

        #endregion
    }
}
