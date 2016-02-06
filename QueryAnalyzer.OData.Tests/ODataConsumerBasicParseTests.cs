using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueryAnalyzer.Common.Criteria;
using QueryAnalyzer.Interfaces;

namespace QueryAnalyzer.Modules.OData.Tests
{
    [TestClass]
    public class ODataConsumerBasicParseTests
    {
        [TestMethod]
        public void consume_equals_url_expect_filter_rules()
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

        [TestMethod]
        public void consume_greater_or_equal_url_expect_rules()
        {
            var oDataConsumer = new ODataConsumer(new FilterRuleCriteria
            {
                FilterStatement = "PrimaryKey ge 100",
                FilterLogic = "and"
            });

            var rules = oDataConsumer.BuildRules();
            Assert.IsTrue(rules.Count == 1);
            Assert.AreEqual(RuleOperator.GreaterThanEqual, rules[0].Operator);
            Assert.AreEqual("100", rules[0].Operands);
            Assert.AreEqual("PrimaryKey", rules[0].VariableName);
        }

        [TestMethod]
        public void consume_less_than_url_expect_rules()
        {
            var oDataConsumer = new ODataConsumer(new FilterRuleCriteria
            {
                FilterStatement = "PrimaryKey lt 100",
                FilterLogic = "and"
            });

            var rules = oDataConsumer.BuildRules();
            Assert.IsTrue(rules.Count == 1);
            Assert.AreEqual(RuleOperator.LessThan, rules[0].Operator);
            Assert.AreEqual("100", rules[0].Operands);
            Assert.AreEqual("PrimaryKey", rules[0].VariableName);
        }

        [TestMethod]
        public void consume_less_or_equal_than_url_expect_rules()
        {
            var oDataConsumer = new ODataConsumer(new FilterRuleCriteria
            {
                FilterStatement = "PrimaryKey le 100",
                FilterLogic = "and"
            });

            var rules = oDataConsumer.BuildRules();
            Assert.IsTrue(rules.Count == 1);
            Assert.AreEqual(RuleOperator.LessThanEqual, rules[0].Operator);
            Assert.AreEqual("100", rules[0].Operands);
            Assert.AreEqual("PrimaryKey", rules[0].VariableName);
        }


        [TestMethod]
        public void consume_not_equal_url_expect_rules()
        {
            var oDataConsumer = new ODataConsumer(new FilterRuleCriteria
            {
                FilterStatement = "PrimaryKey ne 100",
                FilterLogic = "and"
            });

            var rules = oDataConsumer.BuildRules();
            Assert.IsTrue(rules.Count == 1);
            Assert.AreEqual(RuleOperator.NotEqual, rules[0].Operator);
            Assert.AreEqual("100", rules[0].Operands);
            Assert.AreEqual("PrimaryKey", rules[0].VariableName);
        }

        [TestMethod]
        public void consume_in_url_expect_rules()
        {
            var oDataConsumer = new ODataConsumer(new FilterRuleCriteria
            {
                FilterStatement = "indexof(tolower(PrimaryKey),'A') ge 0",
                FilterLogic = "and"
            });

            var rules = oDataConsumer.BuildRules();
            Assert.IsTrue(rules.Count == 1);
            Assert.AreEqual(RuleOperator.Contains, rules[0].Operator);
            Assert.AreEqual("A", rules[0].Operands);
            Assert.AreEqual("PrimaryKey", rules[0].VariableName);

        }

        [TestMethod]
        public void consume_date_url_expect_rules()
        {
            var oDataConsumer = new ODataConsumer(new FilterRuleCriteria
            {
                FilterStatement = "day(DateOpened) eq 1 and month(DateOpened) eq 10 and year(DateOpened) eq 2014",
                FilterLogic = "and"
            });

            var rules = oDataConsumer.BuildRules();
            Assert.IsTrue(rules.Count == 1);

            var dateRule = rules[0] as IFilterDateRule;
            Assert.IsNotNull(dateRule);

            Assert.AreEqual(10, dateRule.DateOperands.Month);
            Assert.AreEqual(1, dateRule.DateOperands.Day);
            Assert.AreEqual(2014, dateRule.DateOperands.Year);
        }

        [TestMethod]
        public void consume_exact_date_url_expect_rules()
        {
            var oDataConsumer = new ODataConsumer(new FilterRuleCriteria
            {
                FilterStatement = "DateOpened gt DateTime'2014-10-01T23:59:59'",
                FilterLogic = "and"
            });

            var rules = oDataConsumer.BuildRules();
            Assert.IsTrue(rules.Count == 1);

            var dateRule = rules[0] as IFilterDateRule;
            Assert.IsNotNull(dateRule);

            Assert.AreEqual(10, dateRule.DateOperands.Month);
            Assert.AreEqual(1, dateRule.DateOperands.Day);
            Assert.AreEqual(2014, dateRule.DateOperands.Year);
        }

        
    }
}
