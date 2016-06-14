using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueryAnalyzer.Common.Criteria;
using QueryAnalyzer.Common.Tests.MockItems;
using QueryAnalyzer.Interfaces;

namespace QueryAnalyzer.Common.Tests
{
    [TestClass]
    public class BootStrapperTests
    {        
        [TestMethod]
        public void register_module_as_default_expect_result()
        {
            var bootStrapper = new BootLoader();
            var module = new MockModule();
            bootStrapper.RegisterModule("primary", module);
            Assert.AreEqual(module, Strategy.For<IAnalyzerStrategy>().Default);
        }

        [TestMethod]
        public void register_module_expect_expect_consumer()
        {
            var bootStrapper = new BootLoader();
            var module = new MockModule();
            bootStrapper.RegisterModule("primary", module);

            var criteria = module.BuildCriteria("");
            var rules = module.Consume(criteria);

            Assert.IsNotNull(rules);
        }

        [TestMethod]
        public void register_module_use_factory_expect_rules()
        {
            var bootStrapper = new BootLoader();
            bootStrapper.RegisterModule("primary", new MockModule());
            
            var rules = Strategy.For<IAnalyzerStrategy>().Analyze("");

            Assert.IsNotNull(rules);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void register_module_add_module_twice_expect_exception()
        {
            var bootStrapper = new BootLoader();
            bootStrapper.RegisterModule("primary", new MockModule());
            bootStrapper.RegisterModule("primary", new MockModule());
        }
    }
}
