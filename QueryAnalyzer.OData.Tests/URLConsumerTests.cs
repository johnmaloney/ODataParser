using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueryAnalyzer.Common.Criteria;
using QueryAnalyzer.Interfaces;

namespace QueryAnalyzer.Modules.OData.Tests
{
    [TestClass]
    public class URLConsumerTests
    {
        [TestMethod]
        public void consume_simple_url_expect_filter_rules()
        {
            var oDataConsumer = new ODataConsumer(new FilterRuleCriteria
            {
                FilterStatement = "PrimaryKey eq 100",
                FilterLogic = "and"
            });

            var rules = oDataConsumer.BuildRules();
            Assert.IsTrue(rules.Count == 1);
            Assert.AreEqual(RuleOperator.Equal, rules[0].Operator);
            Assert.AreEqual("100", rules[0].Operands);
            Assert.AreEqual("PrimaryKey", rules[0].VariableName);

        }

        [TestMethod]
        public void consume_greater_than_url_expect_rules()
        {
            var oDataConsumer = new ODataConsumer(new FilterRuleCriteria
            {
                FilterStatement = "PrimaryKey gt 100",
                FilterLogic = "and"
            });

            var rules = oDataConsumer.BuildRules();
            Assert.IsTrue(rules.Count == 1);
            Assert.AreEqual(RuleOperator.GreaterThan, rules[0].Operator);
            Assert.AreEqual("100", rules[0].Operands);
            Assert.AreEqual("PrimaryKey", rules[0].VariableName);
        }
    }
}
