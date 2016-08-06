using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueryAnalyzer.Common;
using QueryAnalyzer.Common.Criteria;
using QueryAnalyzer.Interfaces;
using QueryAnalyzer.Common.Utility;
using System.Threading.Tasks;

namespace QueryAnalyzer.Modules.OData.Tests
{
    [TestClass]
    public class CommandInvokeTests
    {
        [TestMethod]
        public async Task use_string_matching_to_find_matching_data()
        {
            var data = new List<Data>
            {
                new Data { PrimaryKey = 1, Description = "Something to test."  },
                new Data { PrimaryKey = 2, Description = "Something to test one."  },
                new Data { PrimaryKey = 3, Description = "Something to test two."  },
                new Data { PrimaryKey = 4, Description = "Something to test three."  },
                new Data { PrimaryKey = 5, Description = "Something to test four."  },
            };

            var rules = Strategy.For<IAnalyzerStrategy>().Analyze("Description eq 'Something to test.'");

            foreach (var item in data)
            {
                foreach (var rule in rules)
                {
                    var passes = await Strategy.For<IAnalyzerStrategy>().Passes<Data>(item, rule);
                }
            }
        }

        [TestMethod]
        public async Task use_int32_matching_to_find_matching_data()
        {
            var data = new List<Data>
            {
                new Data { PrimaryKey = 1, Description = "Something to test."  },
                new Data { PrimaryKey = 2, Description = "Something to test one."  },
                new Data { PrimaryKey = 3, Description = "Something to test two."  },
                new Data { PrimaryKey = 4, Description = "Something to test three."  },
                new Data { PrimaryKey = 5, Description = "Something to test four."  },
            };

            var rules = Strategy.For<IAnalyzerStrategy>().Analyze("PrimaryKey eq 3");

            var passes = false;
            foreach (var item in data)
            {
                foreach (var rule in rules)
                {
                    if (item.PrimaryKey == 3)
                        passes = await Strategy.For<IAnalyzerStrategy>().Passes<Data>(item, rule);
                }
            }

            Assert.IsTrue(passes);
        }

        internal class Data
        {
            public int PrimaryKey { get; set; }
            public string Description { get; set; }
        }
    }
}
