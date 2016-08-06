using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueryAnalyzer.Common;
using QueryAnalyzer.Common.Utility;
using QueryAnalyzer.Interfaces;

namespace QueryAnalyzer
{
    /// <summary>
    /// Allows the Default Module to be set and offers the Main 
    /// method for analyzing an expression for rules.
    /// </summary>
    public class AnalyzerStrategy : IAnalyzerStrategy
    {
        public IModule Default { get; set; }

        public IEnumerable<IFilterRule> Analyze(string statement)
        {
            var criteria = Default.BuildCriteria(statement);
            var consumer = Default.Consume(criteria);
            return consumer.Rules;
        }

        public async Task<bool> Passes<T>(T dataContainer, IFilterRule rule)
        {
            var value = PropertyProvider<T>.GetInstance(rule.VariableName).InvokeGet(dataContainer);
            Type t = value.GetType();
            return await Strategy.For<ICommandStrategy>().Invoke(t, value, rule);
        }
    }
}
