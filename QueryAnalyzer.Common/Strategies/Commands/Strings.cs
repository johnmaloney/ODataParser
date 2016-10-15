using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueryAnalyzer.Interfaces;

namespace QueryAnalyzer.Common.Strategies.Commands
{
    public class StringRuleCommand : ARuleCommand<string>, IRuleCommand<string>
    {
        public StringRuleCommand(IReceiver<string> receiver)
            : base(receiver)
        {
        }

        public async Task<bool> ExecuteRule(object variableValue, RuleOperator operatorType, string operands)
        {
            var convertedVariable = this.Parse(variableValue);
            var convertedOperands = this.Parse(operands);
            return await ReceiverDelegates[operatorType].Invoke(convertedVariable, convertedOperands);
        }

        public string Parse(object parsible)
        {
            if (parsible == null)
                return string.Empty;

            return parsible.ToString();
        }
    }

    public class StringRuleReceiver : IReceiver<string>
    {
        public Task<bool> Always(string variableValue, string operands)
        {
            return Task.FromResult(
                variableValue.ToLowerInvariant().Contains(operands.ToLowerInvariant()));
        }

        public Task<bool> Contains(string variableValue, string operands)
        {
            return Task.FromResult(
                variableValue.ToLowerInvariant().Contains(operands.ToLowerInvariant()));
        }

        public Task<bool> DoesNotContain(string variableValue, string operands)
        {
            return Task.FromResult(
                !variableValue.Contains(operands));
        }

        public Task<bool> EndsWith(string variableValue, string operands)
        {
            return Task.FromResult(
                variableValue.EndsWith(operands));
        }

        public Task<bool> In(string variableValue, string operands)
        {
            return Task.FromResult(
                variableValue.Contains(operands));
        }

        public Task<bool> IsBetween(string variableValue, string operands)
        {
            return Task.FromResult(
                Strategy.For<IStringSearchStrategy>().HasWithin(variableValue, operands));
        }

        public Task<bool> IsDBNull(string variableValue, string operands)
        {
            return Task.FromResult(
                string.IsNullOrEmpty(variableValue));
        }

        public Task<bool> IsEqualTo(string variableValue, string operands)
        {
            return Task.FromResult(
                variableValue.IsEqualTo(operands));
        }

        public Task<bool> IsGreaterThan(string variableValue, string operands)
        {
            return Task.FromResult(
                (int)variableValue.ToCharArray().Sum(c => (int)c)
                < (int)operands.ToCharArray().Sum(c => (int)c));
               
        }

        public Task<bool> IsGreaterThanOrEqual(string variableValue, string operands)
        {
            return Task.FromResult(
                (int)variableValue.ToCharArray().Sum(c => (int)c)
                <= (int)operands.ToCharArray().Sum(c => (int)c));
        }

        public Task<bool> IsLessThan(string variableValue, string operands)
        {
            return Task.FromResult(
                (int)variableValue.ToCharArray().Sum(c => (int)c)
                > (int)operands.ToCharArray().Sum(c => (int)c));
        }

        public Task<bool> IsLessThanOrEqual(string variableValue, string operands)
        {
            return Task.FromResult(
                (int)variableValue.ToCharArray().Sum(c => (int)c)
                >= (int)operands.ToCharArray().Sum(c => (int)c));
        }

        public Task<bool> IsNotDBNull(string variableValue, string operands)
        {
            return Task.FromResult(
                !string.IsNullOrEmpty(variableValue));
        }

        public Task<bool> NeverHas(string variableValue, string operands)
        {
            return Task.FromResult(
                !Strategy.For<IStringSearchStrategy>().HasWithin(variableValue, operands));
        }

        public Task<bool> NotEqual(string variableValue, string operands)
        {
            return Task.FromResult(
                !variableValue.IsEqualTo(operands));
        }

        public Task<bool> NotIn(string variableValue, string operands)
        {
            return Task.FromResult(
                !Strategy.For<IStringSearchStrategy>().HasWithin(variableValue, operands));
        }

        public Task<bool> StartsWith(string variableValue, string operands)
        {
            return Task.FromResult(variableValue.ToLower().StartsWith(operands.ToLower()));
        }
    }
}
