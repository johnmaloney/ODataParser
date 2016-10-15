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
                new Data { PrimaryKey = int.MaxValue-1, Description = "Something to test."  },
                new Data { PrimaryKey = int.MaxValue-2, Description = "Something to test one."  },
                new Data { PrimaryKey = int.MaxValue, Description = "Something to test two."  },
                new Data { PrimaryKey = int.MaxValue -3, Description = "Something to test three."  },
                new Data { PrimaryKey = int.MaxValue -4, Description = "Something to test four."  },
            };

            var rules = Strategy.For<IAnalyzerStrategy>().Analyze("PrimaryKey eq 2147483647");

            var passes = false;
            foreach (var item in data)
            {
                foreach (var rule in rules)
                {
                    if (item.PrimaryKey == int.MaxValue)
                        passes = await Strategy.For<IAnalyzerStrategy>().Passes<Data>(item, rule);
                }
            }

            Assert.IsTrue(passes);
        }
        
        [TestMethod]
        public async Task use_int64_matching_to_find_matching_data()
        {
            var data = new List<DataLarge>
            {
                new DataLarge { PrimaryKey = Int64.MinValue, Description = "Something to test."  },
                new DataLarge { PrimaryKey = Int64.MinValue, Description = "Something to test one."  },
                new DataLarge { PrimaryKey = Int64.MaxValue, Description = "Something to test two."  },
                new DataLarge { PrimaryKey = Int64.MinValue, Description = "Something to test three."  },
                new DataLarge { PrimaryKey = Int64.MinValue, Description = "Something to test four."  },
            };

            var rules = Strategy.For<IAnalyzerStrategy>().Analyze("PrimaryKey eq 9223372036854775807");

            var passes = false;
            foreach (var item in data)
            {
                foreach (var rule in rules)
                {
                    if (item.PrimaryKey == Int64.MaxValue)
                        passes = await Strategy.For<IAnalyzerStrategy>().Passes<DataLarge>(item, rule);
                }
            }

            Assert.IsTrue(passes);
        }

        [TestMethod]
        public async Task use_datetime_matching_to_find_matching_data()
        {
            var data = new List<Data>
            {
                new Data { PrimaryKey = 1, Date = DateTime.MinValue, Description = "Something to test."  },
                new Data { PrimaryKey = 2, Date = DateTime.MinValue, Description = "Something to test one."  },
                new Data { PrimaryKey = 3, Date = new DateTime(2014, 5, 27), Description = "Something to test two."  },
                new Data { PrimaryKey = 4, Date = DateTime.MinValue, Description = "Something to test three."  },
                new Data { PrimaryKey = 5, Date = DateTime.MinValue, Description = "Something to test four."  },
            };

            var rules = Strategy.For<IAnalyzerStrategy>().Analyze("Date eq DateTime'2014-05-27'");

            var passes = false;
            foreach (var item in data)
            {
                foreach (var rule in rules)
                {
                    passes = await Strategy.For<IAnalyzerStrategy>().Passes<Data>(item, rule);
                    if (item.PrimaryKey == 3)
                        Assert.IsTrue(passes);
                    else
                        Assert.IsFalse(passes);
                }
            }
        }

        internal class Data
        {
            public int PrimaryKey { get; set; }
            public string Description { get; set; }
            public DateTime Date { get; set; }
        }

        internal class DataLarge
        {
            public Int64 PrimaryKey { get; set; }
            public string Description { get; set; }
        }
    }
}
