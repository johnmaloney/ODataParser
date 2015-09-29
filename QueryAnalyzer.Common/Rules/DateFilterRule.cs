using QueryAnalyzer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryAnalyzer.Common
{
    public class DateFilterRule : IFilterRule
    {
        #region Fields



        #endregion

        #region Properties
        public string Operands
        {
            get;
            set;
        }

        public RuleOperator Operator
        {
            get;
            set;
        }

        public RuleCombinationOperator RuleCombinationOperator
        {
            get;
            set;
        }

        public string VariableName
        {
            get;
            set;
        }

        #endregion

        #region Methods



        #endregion
    }
}
