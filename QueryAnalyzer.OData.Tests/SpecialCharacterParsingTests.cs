using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueryAnalyzer.Common.Criteria;
using QueryAnalyzer.Interfaces;

namespace QueryAnalyzer.Modules.OData.Tests
{
    [TestClass]
    public class SpecialCharacterParsingTests
    {
        [TestMethod]
        public void ensure_special_character_single_returns_rules()
        {
            var oDataConsumer = new ODataConsumer(new FilterRuleCriteria
            {
                FilterStatement = "indexof(tolower(Name),'#') ge 0",
                FilterLogic = "and"
            });

            var rules = oDataConsumer.BuildRules();
            Assert.IsTrue(rules.Count == 1);
            Assert.AreEqual(RuleOperator.Contains, rules[0].Operator);
            Assert.AreEqual("#", rules[0].Operands);
            Assert.AreEqual("Name", rules[0].VariableName);
        }
    }
}
