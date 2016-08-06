using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueryAnalyzer.Interfaces;

namespace QueryAnalyzer.Common.Strategies.Commands
{
    public class NumberRuleCommand<T> : ARuleCommand<T>, IRuleCommand<T>
    {
        private Func<object, T> parseDelegate;

        public NumberRuleCommand(IReceiver<T> receiver)
            : base(receiver)
        {
        }

        public async Task<bool> ExecuteRule(object variableValue, RuleOperator operatorType, string operands)
        {
            var convertedVariable = this.Parse(variableValue);
            var convertedOperands = this.Parse(operands);
            return await ReceiverDelegates[operatorType].Invoke(convertedVariable, convertedOperands);
        }

        public T Parse(object parsible)
        {
            if (parsible is T)
            {
                return (T)parsible;
            }
            else {
                try
                {
                    return (T)Convert.ChangeType(parsible, typeof(T));
                }
                catch (InvalidCastException)
                {
                    throw new InvalidCastException(string.Format("Value was not convertible to an {0}, value: {1}", typeof(T), parsible));
                }
            }
        }
    }

    public class Int32RuleReceiver : IReceiver<Int32>
    {
        public Task<bool> Always(int variableValue, int operands)
        {
            return Task.FromResult(variableValue == operands);
        }

        public Task<bool> Contains(int variableValue, int operands)
        {
            return Task.FromResult(
                variableValue.ToString().Contains(operands.ToString()));
        }

        public Task<bool> DoesNotContain(int variableValue, int operands)
        {
            return Task.FromResult(variableValue == operands);
        }

        public Task<bool> EndsWith(int variableValue, int operands)
        {
            return Task.FromResult(variableValue == operands);
        }

        public Task<bool> In(int variableValue, int operands)
        {
            return Task.FromResult(variableValue == operands);
        }

        public Task<bool> IsBetween(int variableValue, int operands)
        {
            return Task.FromResult(variableValue == operands);
        }

        public Task<bool> IsDBNull(int variableValue, int operands)
        {
            return Task.FromResult(variableValue == operands);
        }

        public Task<bool> IsEqualTo(Int32 variableValue, Int32 operands)
        {
            return Task.FromResult(variableValue == operands);
        }

        public Task<bool> IsGreaterThan(int variableValue, int operands)
        {
            return Task.FromResult(variableValue == operands);
        }

        public Task<bool> IsGreaterThanOrEqual(int variableValue, int operands)
        {
            return Task.FromResult(variableValue == operands);
        }

        public Task<bool> IsLessThan(int variableValue, int operands)
        {
            return Task.FromResult(variableValue == operands);
        }

        public Task<bool> IsLessThanOrEqual(int variableValue, int operands)
        {
            return Task.FromResult(variableValue == operands);
        }

        public Task<bool> IsNotDBNull(int variableValue, int operands)
        {
            return Task.FromResult(variableValue == operands);
        }

        public Task<bool> NeverHas(int variableValue, int operands)
        {
            return Task.FromResult(variableValue == operands);
        }

        public Task<bool> NotEqual(int variableValue, int operands)
        {
            return Task.FromResult(variableValue == operands);
        }

        public Task<bool> NotIn(int variableValue, int operands)
        {
            return Task.FromResult(variableValue == operands);
        }

        public Task<bool> StartsWith(int variableValue, int operands)
        {
            return Task.FromResult(variableValue == operands);
        }
    }
    
    //public class Int64RuleReceiver : AReceiver<Int64>, IReceiver<Int64>
    //{
    //    public Task<bool> Always(Int64 variableValue, Int64 operands)
    //    {
    //        return Task.FromResult(
    //            variableValue == operands);
    //    }

    //    public Task<bool> Contains(Int64 variableValue, Int64 operands)
    //    {
    //        return Task.FromResult((variableValue - operands) > 0);
    //    }

    //    public Task<bool> DoesNotContain(Int64 variableValue, Int64 operands)
    //    {
    //        return Task.FromResult(
    //            variableValue == operands);
    //    }

    //    public Task<bool> EndsWith(Int64 variableValue, Int64 operands)
    //    {
    //        return Task.FromResult(
    //            variableValue.EndsWith(operands));
    //    }

    //    public Task<bool> In(Int64 variableValue, Int64 operands)
    //    {
    //        return Task.FromResult((variableValue - operands) > 0);
    //    }

    //    public Task<bool> IsBetween(Int64 variableValue, Int64 operands)
    //    {
    //        return Task.FromResult(false);
    //    }

    //    public Task<bool> IsDBNull(Int64 variableValue, Int64 operands)
    //    {
    //        return Task.FromResult(
    //            string.IsNullOrEmpty(variableValue));
    //    }

    //    public Task<bool> IsEqualTo(Int64 variableValue, Int64 operands)
    //    {
    //        return Task.FromResult(
    //            variableValue.IsEqualTo(operands));
    //    }

    //    public Task<bool> IsGreaterThan(Int64 variableValue, Int64 operands)
    //    {
    //        return Task.FromResult(
    //            (int)variableValue.ToCharArray().Sum(c => (int)c)
    //            < (int)operands.ToCharArray().Sum(c => (int)c));

    //    }

    //    public Task<bool> IsGreaterThanOrEqual(Int64 variableValue, Int64 operands)
    //    {
    //        return Task.FromResult(
    //            (int)variableValue.ToCharArray().Sum(c => (int)c)
    //            <= (int)operands.ToCharArray().Sum(c => (int)c));
    //    }

    //    public Task<bool> IsLessThan(Int64 variableValue, Int64 operands)
    //    {
    //        return Task.FromResult(
    //            (int)variableValue.ToCharArray().Sum(c => (int)c)
    //            > (int)operands.ToCharArray().Sum(c => (int)c));
    //    }

    //    public Task<bool> IsLessThanOrEqual(Int64 variableValue, Int64 operands)
    //    {
    //        return Task.FromResult(
    //            (int)variableValue.ToCharArray().Sum(c => (int)c)
    //            >= (int)operands.ToCharArray().Sum(c => (int)c));
    //    }

    //    public Task<bool> IsNotDBNull(Int64 variableValue, Int64 operands)
    //    {
    //        return Task.FromResult(
    //            !string.IsNullOrEmpty(variableValue));
    //    }

    //    public Task<bool> NeverHas(Int64 variableValue, Int64 operands)
    //    {
    //        return Task.FromResult(
    //            !Strategy.For<IStringSearchStrategy>().HasWithin(variableValue, operands));
    //    }

    //    public Task<bool> NotEqual(Int64 variableValue, Int64 operands)
    //    {
    //        return Task.FromResult(
    //            !variableValue.IsEqualTo(operands));
    //    }

    //    public Task<bool> NotIn(Int64 variableValue, Int64 operands)
    //    {
    //        return Task.FromResult(
    //            !Strategy.For<IStringSearchStrategy>().HasWithin(variableValue, operands));
    //    }

    //    public Task<bool> StartsWith(Int64 variableValue, Int64 operands)
    //    {
    //        return Task.FromResult(variableValue.StartsWith(operands));
    //    }
    //}
}
