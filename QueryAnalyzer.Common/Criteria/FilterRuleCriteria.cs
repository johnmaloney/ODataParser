using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueryAnalyzer.Interfaces;

namespace QueryAnalyzer.Common.Criteria
{
    public class FilterRuleCriteria : IFilterRuleCriteria
    {
        public string FilterLogic { get; set; }

        public string FilterStatement { get; set; }

        public string OrderBy { get; set; }

        public string OrderByColumnName { get; set; }

        public FilterRuleCriteria()
        {
            this.OrderBy = "asc";
        }
    }
}
