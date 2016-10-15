using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueryAnalyzer.Interfaces;

namespace QueryAnalyzer.Common.Strategies.Commands
{
    public class DateRuleCommand : ARuleCommand<DateTime>, IRuleCommand<DateTime>
    {
        public DateRuleCommand(IReceiver<DateTime> receiver)
            : base(receiver)
        {
        }
        
        public async Task<bool> ExecuteRule(object variableValue, RuleOperator operatorType, string operands)
        {
            var convertedVariable = this.Parse(variableValue);
            var convertedOperands = this.Parse(operands);
            return await ReceiverDelegates[operatorType].Invoke(convertedVariable, convertedOperands);
        }

        public DateTime Parse(object parsible)
        {
            if (parsible == null)
                return DateTime.MinValue;

            return DateTime.Parse(parsible.ToString());
        }
    }


    public class DateRuleReceiver : IReceiver<DateTime>
    {
        public Task<bool> Always(DateTime variableValue, DateTime operands)
        {
            return Task.FromResult(variableValue == operands);
        }

        public Task<bool> Contains(DateTime variableValue, DateTime operands)
        {
            return Task.FromResult(
                variableValue.Year == operands.Year ||
                variableValue.Day == operands.Day ||
                variableValue.Month == operands.Month);
        }

        public Task<bool> DoesNotContain(DateTime variableValue, DateTime operands)
        {
            return Task.FromResult(
                variableValue.Year != operands.Year ||
                variableValue.Day != operands.Day ||
                variableValue.Month != operands.Month);
        }

        public Task<bool> EndsWith(DateTime variableValue, DateTime operands)
        {
            return Task.FromResult(variableValue.Year == operands.Year);
        }

        public Task<bool> In(DateTime variableValue, DateTime operands)
        {
            return Task.FromResult(variableValue.Year == operands.Year);
        }

        public Task<bool> IsBetween(DateTime variableValue, DateTime operands)
        {
            return Task.FromResult(operands < variableValue);
        }

        public Task<bool> IsDBNull(DateTime variableValue, DateTime operands)
        {
            return Task.FromResult(variableValue == DateTime.MinValue);
        }

        public Task<bool> IsEqualTo(DateTime variableValue, DateTime operands)
        {
            return Task.FromResult(
                variableValue == operands);
        }

        public Task<bool> IsGreaterThan(DateTime variableValue, DateTime operands)
        {
            return Task.FromResult(operands > variableValue);

        }

        public Task<bool> IsGreaterThanOrEqual(DateTime variableValue, DateTime operands)
        {
            return Task.FromResult(operands >= variableValue);
        }

        public Task<bool> IsLessThan(DateTime variableValue, DateTime operands)
        {
            return Task.FromResult(operands < variableValue);
        }

        public Task<bool> IsLessThanOrEqual(DateTime variableValue, DateTime operands)
        {
            return Task.FromResult(operands <= variableValue);
        }

        public Task<bool> IsNotDBNull(DateTime variableValue, DateTime operands)
        {
            return Task.FromResult(variableValue != DateTime.MinValue);
        }

        public Task<bool> NeverHas(DateTime variableValue, DateTime operands)
        {
            return Task.FromResult(operands > variableValue);
        }

        public Task<bool> NotEqual(DateTime variableValue, DateTime operands)
        {
            return Task.FromResult(operands != variableValue);
        }

        public Task<bool> NotIn(DateTime variableValue, DateTime operands)
        {
            return Task.FromResult(operands > variableValue);
        }

        public Task<bool> StartsWith(DateTime variableValue, DateTime operands)
        {
            return Task.FromResult(operands.Year == variableValue.Year && operands.Month == variableValue.Month);
        }
    }
}
