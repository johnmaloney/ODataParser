using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueryAnalyzer.Common.Strategies;
using QueryAnalyzer.Interfaces;

namespace QueryAnalyzer.Common.Tests
{
    [TestClass]
    public class AssemblyInitializer
    {
        [AssemblyInitialize()]
        public static void AssemblyInitialize(TestContext context)
        {
            ATest.AssemblyInitialize(context);
            
            Strategy.AddStrategy<IAnalyzerStrategy>(new AnalyzerStrategy());
            Strategy.AddStrategy<ICommandStrategy>(new DefaultInvokerStrategy());
            Strategy.AddStrategy<IStringSearchStrategy>(new StringSearchStrategy());
        }
    }
}
