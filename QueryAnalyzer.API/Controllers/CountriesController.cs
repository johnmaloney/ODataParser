using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using QueryAnalyzer.API.Domain;
using QueryAnalyzer.Common;
using QueryAnalyzer.Interfaces;

namespace QueryAnalyzer.API.Controllers
{
    public class CountriesController : ApiController
    {
        #region Fields

        private CountriesRepository repository;

        #endregion

        #region Properties
        #endregion

        #region Methods

        // GET api/values
        /// <summary>
        /// Pulls all Countries from this REST endpoint - http://restcountries.eu/
        /// </summary>
        /// <returns>IEnumerable of Country</returns>
        public async Task<IEnumerable<Country>> Get()
        {
            return await repository.GetAll();
        }

        /// <summary>
        /// Finds the Countries that match the query parameter.
        /// </summary>
        /// <param name="query">OData string representing the filters to apply to the data</param>
        /// <example>[server]/api/coutries?query=name eq 'United State'</example>
        /// <returns>IEnumerable of Country</returns>
        public async Task<IEnumerable<Country>> Get(string query)
        {
            var rules = Strategy.For<IAnalyzerStrategy>().Analyze(query);
            return await repository.Get(rules);
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        public CountriesController(CountriesRepository repository)
        {
            this.repository = repository;
        }

        #endregion        
    }
}
