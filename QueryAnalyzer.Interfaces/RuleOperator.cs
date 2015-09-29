using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryAnalyzer.Interfaces
{
    public enum RuleOperator
    {
        Default,
        Equal,
        LessThan,
        GreaterThan,
        LessThanEqual,
        GreaterThanEqual,
        NotEqual,
        In,
        Between,
        NotIn,
        Always,
        Never,
        Sort,
        StartsWith,
        EndsWith,
        Contains,
        DoesNotContain,
        IsDBNull,
        IsNotDBNull
    }

    public static class RuleOperatorExtensions
    {
        private static object SyncRoot = new object();
        private static Dictionary<string, RuleOperator> ruleOperations;
        public static Dictionary<string, RuleOperator> RuleOperations()
        {
            if (ruleOperations == null)
            {
                lock(SyncRoot)
                {
                    if (ruleOperations == null)
                    {
                        ruleOperations = new Dictionary<string, RuleOperator>();
                        ruleOperations.Add("eqnull", RuleOperator.IsDBNull);
                        ruleOperations.Add("nenull", RuleOperator.IsNotDBNull);

                        ruleOperations.Add("indexofeq", RuleOperator.Contains);
                        ruleOperations.Add("indexofge", RuleOperator.Contains);
                        ruleOperations.Add("indexofeq-1", RuleOperator.DoesNotContain);
                        ruleOperations.Add("startswitheq", RuleOperator.StartsWith);
                        ruleOperations.Add("endswitheq", RuleOperator.EndsWith);
                        ruleOperations.Add("tolowereq", RuleOperator.Equal);
                        ruleOperations.Add("eq", RuleOperator.Equal);
                        ruleOperations.Add("tolowerne", RuleOperator.NotEqual);
                        ruleOperations.Add("ne", RuleOperator.NotEqual);
                        ruleOperations.Add("ge", RuleOperator.GreaterThanEqual);
                        ruleOperations.Add("gt", RuleOperator.GreaterThan);
                        ruleOperations.Add("lt", RuleOperator.LessThan);
                        ruleOperations.Add("le", RuleOperator.LessThanEqual);
                        ruleOperations.Add("dayeq", RuleOperator.Equal);
                        ruleOperations.Add("dayne", RuleOperator.NotEqual);
                        ruleOperations.Add("montheq", RuleOperator.Equal);
                        ruleOperations.Add("monthne", RuleOperator.NotEqual);
                        ruleOperations.Add("yeareq", RuleOperator.Equal);
                        ruleOperations.Add("yearne", RuleOperator.NotEqual);
                        ruleOperations.Add("in", RuleOperator.In);
                    }
                }
            }
            return ruleOperations;
        }
    }
}
