using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using QueryAnalyzer.Interfaces;

namespace QueryAnalyzer.Modules.JSON
{
    public class JSONConsumer<TStatement> : IConsumer
    {
        #region Fields

        private readonly IFilterRuleCriteria filterRuleCriteria;
        private JSchema schema;
        private JToken json;

        #endregion

        #region Properties

        public IEnumerable<IFilterRule> Rules
        {
            get
            {
                return this.BuildRules();
            }
        }

        #endregion

        #region Methods

        public JSONConsumer(IFilterRuleCriteria criteria)
        {
            filterRuleCriteria = criteria;
            schema = JSchema.Parse(Encoding.Default.GetString(Properties.Resources.StatementSchema));
            json = JToken.Parse(criteria.FilterStatement);

            IList<ValidationError> errors;
            if (!json.IsValid(schema, out errors))
                throw new JsonSerializationException("The statement JSON did not meet the validation standards.");
        }

        public List<IFilterRule> BuildRules()
        {
            // Collect each individual rule that exists in the incoming filter statement //
            var compiledFilterRules = new List<IFilterRule>();

            // Deserialize the filterStatement //
            var jsonObject = JsonConvert.DeserializeObject<TStatement>(filterRuleCriteria.FilterStatement);

            return compiledFilterRules;
        }

        #endregion
    }
}
