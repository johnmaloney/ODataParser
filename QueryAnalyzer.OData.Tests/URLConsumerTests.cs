using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueryAnalyzer.Common.Criteria;

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
        }
    }
}
