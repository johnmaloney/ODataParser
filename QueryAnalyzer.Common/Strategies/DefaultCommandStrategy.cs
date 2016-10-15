using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueryAnalyzer.Common.Strategies.Commands;
using QueryAnalyzer.Interfaces;

namespace QueryAnalyzer.Common.Strategies
{
    public interface IRuleCommand<T> : IRuleCommand
    {
        int Compare(T itemA, T itemB);
        T Parse(object parsible);
     }

    public interface IReceiver<T>
    {
        Task<bool> Always(T variableValue, T operands);
        Task<bool> IsBetween(T variableValue, T operands);
        Task<bool> IsEqualTo(T variableValue, T operands);
        Task<bool> EndsWith(T variableValue, T operands);
        Task<bool> Contains(T variableValue, T operands);
        Task<bool> DoesNotContain(T variableValue, T operands);
        Task<bool> IsGreaterThan(T variableValue, T operands);
        Task<bool> IsGreaterThanOrEqual(T variableValue, T operands);
        Task<bool> In(T variableValue, T operands);
        Task<bool> IsDBNull(T variableValue, T operands);
        Task<bool> IsNotDBNull(T variableValue, T operands);
        Task<bool> IsLessThan(T variableValue, T operands);
        Task<bool> IsLessThanOrEqual(T variableValue, T operands);
        Task<bool> NeverHas(T variableValue, T operands);
        Task<bool> NotEqual(T variableValue, T operands);
        Task<bool> NotIn(T variableValue, T operands);
        Task<bool> StartsWith(T variableValue, T operands);
    }

    public abstract class ARuleCommand<T> : IComparer<T>
    {
        protected IReceiver<T> Receiver;
        protected Dictionary<RuleOperator, Func<T, T, Task<bool>>> ReceiverDelegates;

        public ARuleCommand(IReceiver<T> receiver)
        {
            Receiver = receiver;

            ReceiverDelegates = new Dictionary<RuleOperator, Func<T, T, Task<bool>>>
            {
                { RuleOperator.Always, receiver.IsEqualTo },
                { RuleOperator.Between, receiver.IsBetween },
                { RuleOperator.Contains, receiver.Contains},
                { RuleOperator.DoesNotContain, receiver.DoesNotContain },
                { RuleOperator.EndsWith, receiver.EndsWith },
                { RuleOperator.Equal, receiver.IsEqualTo },
                { RuleOperator.GreaterThan, receiver.IsGreaterThan },
                { RuleOperator.GreaterThanEqual, receiver.IsGreaterThanOrEqual },
                { RuleOperator.In, receiver.Contains },
                { RuleOperator.IsDBNull, receiver.IsDBNull },
                { RuleOperator.IsNotDBNull, receiver.IsNotDBNull },
                { RuleOperator.LessThan, receiver.IsLessThan },
                { RuleOperator.LessThanEqual, receiver.IsLessThanOrEqual },
                { RuleOperator.Never, receiver.NeverHas },
                { RuleOperator.NotEqual, receiver.NotEqual },
                { RuleOperator.NotIn, receiver.NotIn },
                { RuleOperator.StartsWith, receiver.StartsWith}
            };
        }

        public int Compare(T x, T y)
        {
            throw new NotImplementedException();
        }
    }

    public class DefaultInvokerStrategy : ICommandStrategy
    {
        #region Fields
        
        private Dictionary<Type, IRuleCommand> comparisonDelegates = new Dictionary<Type, IRuleCommand>();

        #endregion

        #region Properties



        #endregion

        #region Methods

        public DefaultInvokerStrategy()
        {
            this.RecordCommand(typeof(string), new StringRuleCommand(new StringRuleReceiver()));
            this.RecordCommand(typeof(Int32), new NumberRuleCommand<Int32>(new Int32RuleReceiver()));

            this.RecordCommand(typeof(Int64), new NumberRuleCommand<Int64>(new Int64RuleReceiver()));
            this.RecordCommand(typeof(DateTime), new DateRuleCommand(new DateRuleReceiver()));
        }

        public void RecordCommand(Type variableType, IRuleCommand ruleCommand)
        {
            if(this.comparisonDelegates.ContainsKey(variableType))
            {
                this.comparisonDelegates[variableType] = ruleCommand;
            }
            else
            {
                this.comparisonDelegates.Add(variableType, ruleCommand);
            }
        }
        
        public async Task<bool> Invoke(Type type, object variableValue, IFilterRule rule)
        {
            IRuleCommand delegateCommand;

            if (comparisonDelegates.TryGetValue(type, out delegateCommand))
            {
                var command = delegateCommand;
                return await command.ExecuteRule(variableValue, rule.Operator, rule.Operands);
            }
            
            return false;
        }
        
        #endregion
    }

}
