using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueryAnalyzer.Interfaces;

namespace QueryAnalyzer
{
    public class AnalyzerFactory
    {
        public static IModule DefaultModule { get; set; }

        public IEnumerable<IFilterRule> Analyze(string statement)
        {
            var criteria = DefaultModule.BuildCriteria(statement);
            var consumer = DefaultModule.Consume(criteria);
            return consumer.Rules;
        }
    }
}
