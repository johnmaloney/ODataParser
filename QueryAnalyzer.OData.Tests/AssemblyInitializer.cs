using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueryAnalyzer.Common;
using QueryAnalyzer.Common.Strategies;
using QueryAnalyzer.Common.Tests;
using QueryAnalyzer.Interfaces;

namespace QueryAnalyzer.Modules.OData.Tests
{
    [TestClass]
    public class AssemblyInitializer
    {
        [AssemblyInitialize()]
        public static void AssemblyInitialize(TestContext context)
        {
            ATest.AssemblyInitialize(context);

            Strategy.AddStrategy<IRegex>(new ODataRegexStrategy());
            Strategy.AddStrategy<IAnalyzerStrategy>(new AnalyzerStrategy());
            Strategy.AddStrategy<ICommandStrategy>(new DefaultInvokerStrategy());
            
            // Set the OData Module as the Default //
            var module = new ODataModule();
            Strategy.For<IAnalyzerStrategy>().Default = module;
        }
    }
}
