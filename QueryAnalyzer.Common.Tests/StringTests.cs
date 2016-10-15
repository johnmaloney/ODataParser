using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueryAnalyzer.Common.Strategies.Commands;
using QueryAnalyzer.Interfaces;

namespace QueryAnalyzer.Common.Tests
{
    [TestClass]
    public class StringTests
    {
        [TestMethod]
        public async Task search_through_string_verify_expect_matches()
        {
            var receiver = new StringRuleReceiver();
            var command = new StringRuleCommand(receiver);

            Assert.IsFalse(await command.ExecuteRule("This string contains something", RuleOperator.Always, "ing"));
            Assert.IsTrue(await command.ExecuteRule("This string contains something", RuleOperator.Between, "contains"));
            Assert.IsTrue(await command.ExecuteRule("This string contains something", RuleOperator.Contains, "string"));
            Assert.IsTrue(await command.ExecuteRule("This string contains something", RuleOperator.DoesNotContain, "tree"));
            Assert.IsTrue(await command.ExecuteRule("This string contains something", RuleOperator.EndsWith, "ing"));
            Assert.IsTrue(await command.ExecuteRule("This string contains something", RuleOperator.Equal, "This string contains something"));
            Assert.IsTrue(await command.ExecuteRule("This string contains something", RuleOperator.GreaterThan, "This string contains something plus 1"));
            Assert.IsTrue(await command.ExecuteRule("This string contains something", RuleOperator.GreaterThanEqual, "This string contains something"));
            Assert.IsTrue(await command.ExecuteRule("This string contains something", RuleOperator.In, "string"));
            Assert.IsTrue(await command.ExecuteRule("This string contains something", RuleOperator.NotEqual, "test"));
            Assert.IsFalse(await command.ExecuteRule("This string contains something", RuleOperator.NotIn, "abc"));
            Assert.IsTrue(await command.ExecuteRule("This string contains something", RuleOperator.StartsWith, "this"));
        }
    }
}
