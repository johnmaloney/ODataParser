using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueryAnalyzer.Common.Utility;
using System.Diagnostics;
using System.Collections.Concurrent;

namespace QueryAnalyzer.Common.Tests
{
    [TestClass]
    public class ReflectionTests
    {
        private static Random idGenerator = new Random(1);
        private static IList<Data> data = null;

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            data = new List<Data>();
            for (int i = 0; i < 2000000; i++)
            {
                data.Add(new Data());
            }
            
        }

        [TestMethod]
        public void use_property_provider_ensure_no_race_condition()
        {
            var nameValues = new ConcurrentBag<string>();
            var timer = Stopwatch.StartNew();
            Parallel.ForEach(data, (item) =>
            {
                nameValues.Add(PropertyProvider<Data>.GetInstance("Name").InvokeGet(item).ToString());
            });
            timer.Stop();
            //var possibleDuplicates = nameValues.GroupBy(v => v).Where(s => s.Count() > 1).Select(s => s);

            Assert.IsTrue(timer.ElapsedMilliseconds < 650);
        }

        private class Data
        {
            public Data()
            {
                Id = idGenerator.Next();
                Name = "Data " + Id.ToString();
                City = "CityName " + Id.ToString();
                Orders = new List<Child>
                {
                    new Child(), 
                    new Child(),
                    new Child()
                };
            }

            public int Id { get; set; }

            public string Name { get; set; }

            public string City { get; set; }

            public IEnumerable<Child> Orders { get; set; }
        }

        private class Child
        {
            public Child()
            {
                Id = idGenerator.Next(100000);
                Name = "Data " + Id.ToString();
                Created = DateTime.Now.AddDays(Id);
            }

            public int Id { get; set; }

            public string Name { get; set; }

            public DateTime Created { get; set; }
        }
    }
}
