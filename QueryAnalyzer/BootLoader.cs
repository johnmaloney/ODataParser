using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueryAnalyzer.Common;
using QueryAnalyzer.Interfaces;

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
            Strategy.AddStrategy<IAnalyzerStrategy>(new AnalyzerStrategy());
        }

        public void RegisterModule(string moduleIdentifier, IModule module)
        {
            //TO DO: Use regex to analyze the incoming moduleIdentifier for the following: "primary", "default" or "main"//
            try
            {
                this.registeredModule.Add(moduleIdentifier, module);

                // Register this as the Default //
                if (this.registeredModule.Count == 1)
                    Strategy.For<IAnalyzerStrategy>().Default = module;
            } 
            catch(ArgumentException ex)
            {
                throw new ArgumentException($"The identifier used is already registered ({moduleIdentifier}), please use a different identifier (e.g. primary, secondary, tertiary, etc).", ex);
            }
        }

        #endregion
    }
}
