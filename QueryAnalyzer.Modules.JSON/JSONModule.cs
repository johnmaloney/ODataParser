using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueryAnalyzer.Common.Criteria;
using QueryAnalyzer.Interfaces;

namespace QueryAnalyzer.Modules.JSON
{
    public class JSONModule<TStatement> : IModule
    {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Methods

        public JSONModule()
        {

        }

        public IFilterRuleCriteria BuildCriteria(string statement)
        {
            return new FilterRuleCriteria
            {
                FilterStatement = statement,
                FilterLogic = "and"
            };
        }

        public IConsumer Consume(IFilterRuleCriteria filterCriteria)
        {
            var jsonConsumer = new JSONConsumer<TStatement>(filterCriteria);
            jsonConsumer.BuildRules();
            return jsonConsumer;
        }

        #endregion
    }
}
