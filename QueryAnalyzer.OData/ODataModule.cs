using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueryAnalyzer.Common.Criteria;
using QueryAnalyzer.Interfaces;

namespace QueryAnalyzer.Modules.OData
{
    public class ODataModule : IModule
    {
        #region Fields
        
        #endregion

        #region Properties
        
        #endregion

        #region Methods

        public ODataModule()
        {

        }

        public IFilterRuleCriteria BuildCriteria(string statement)
        {
            return new FilterRuleCriteria()
            {
                FilterStatement = statement,
                FilterLogic = "and"
            };
        }

        public IConsumer Consume(IFilterRuleCriteria criteria)
        {
            var oDataConsumer = new ODataConsumer(criteria);
            oDataConsumer.BuildRules();
            return oDataConsumer;
        }
        
        #endregion
    }
}
