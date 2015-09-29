using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryAnalyzer.Interfaces
{
    public abstract class AFilterPipe : APipe, IFilterPipe
    {
        #region Fields
        
        protected IFilterRule filterRule;
        protected readonly RuleCombinationOperator filterRuleCombinator;

        #endregion

        #region Properties

        public IFilterRule FilterRule
        {
            get { return filterRule; }
        }

        #endregion

        #region Methods


        public abstract void Filter(string filteredText);

        /// <summary>
        /// Allows for the children of this Filter to be called while operating 
        /// on the parents FilterRule, effectively passing the parent data to the
        /// children for manipulation.
        /// </summary>
        /// <param name="filteredText">String original text to parse.</param>
        /// <param name="filterRule">IFilterRule from the parent</param>
        public void Filter(string filteredText, IFilterRule filterRule)
        {
            this.filterRule = filterRule;
            this.Filter(filteredText);
        }

        //public abstract APipe Add(APipe childPipe);

        public AFilterPipe() : base()
        {

        }

        public AFilterPipe(IFilterRuleCriteria filterRuleCriteria)
            : this()
        {
            switch (filterRuleCriteria.FilterLogic)
            {
                case "AND":
                    filterRuleCombinator = RuleCombinationOperator.AND;
                    break;
                case "OR":
                    filterRuleCombinator = RuleCombinationOperator.OR;
                    break;
                default:
                    filterRuleCombinator = RuleCombinationOperator.AND;
                    break;
            }
        }

        #endregion
    }
}
