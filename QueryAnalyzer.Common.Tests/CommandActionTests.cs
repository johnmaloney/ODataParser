using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueryAnalyzer.Common.Strategies;
using QueryAnalyzer.Common.Tests.MockItems;
using QueryAnalyzer.Interfaces;

namespace QueryAnalyzer.Common.Tests
{
    [TestClass]
    public class CommandActionTests
    {
        [TestMethod]
        public async Task given_set_of_filter_rules_execute_delegates_expect_correct_comparison()
        {
            var bootStrapper = new BootLoader();

            var rules = new List<IFilterRule>
            {
                new DefaultFilterRule { VariableName = "Key", Operator =RuleOperator.Equal, Operands ="10"  }
            };

            var data = new Dictionary<string, string>
            {
                { "Key", "10" }
            };

            foreach (var kvp in data)
            {
                foreach (var rule in rules)
                {
                    var passes = await Strategy.For<ICommandStrategy>()
                        .Invoke(kvp.Value.GetType(), kvp.Value, rule);
                    Assert.IsTrue(passes);
                }
            }
        }
    }
}
