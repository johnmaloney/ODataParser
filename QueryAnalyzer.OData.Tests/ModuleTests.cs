using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueryAnalyzer.Common;
using QueryAnalyzer.Interfaces;
using System.Linq;

namespace QueryAnalyzer.Modules.OData.Tests
{
    [TestClass]
    public class ModuleTests
    {
        [TestMethod]
        public void add_odata_module_as_default_expect_match()
        {
            var module = new ODataModule();
            Strategy.For<IAnalyzerStrategy>().Default = module;
            Assert.AreEqual(module, Strategy.For<IAnalyzerStrategy>().Default);
        }

        [TestMethod]
        public void add_odata_module_build_expect_rules()
        {
            var module = new ODataModule();
            Strategy.For<IAnalyzerStrategy>().Default = module;
            
            var rules = Strategy.For<IAnalyzerStrategy>().Analyze("PrimaryKey eq 100");
            Assert.AreEqual(rules.First().VariableName, "PrimaryKey");
            Assert.AreEqual(rules.First().Operands, "100");
            Assert.AreEqual(rules.First().Operator, RuleOperator.Equal);

        }
    }
}
