using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryAnalyzer.Interfaces
{
    public interface ICommandStrategy : IStrategy
    {
        void RecordCommand(Type variableType, IRuleCommand ruleCommand);

        Task<bool> Invoke(Type variableType, object variableValue, IFilterRule rule);
    }
    
    public interface IRuleCommand
    {
        Task<bool> ExecuteRule(object variableValue, RuleOperator operatorType, string operandsValue);
    }
}
