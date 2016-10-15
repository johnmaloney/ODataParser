using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;
using System.Web;
using QueryAnalyzer.Interfaces;
using QueryAnalyzer.Common;

namespace QueryAnalyzer.API.Domain
{
    /// <summary>
    /// Allows access to the Countries data. Sourced from this http://restcountries.eu/
    /// </summary>
    public class CountriesRepository
    {
        #region Fields

        private IEnumerable<Country> countries;

        #endregion

        #region Properties
        #endregion

        #region Methods

        /// <summary>
        /// This class gets instantiated only once. Will convert a compiled version of the Coutries into JSON.
        /// </summary>
        public CountriesRepository()
        {
            countries = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Country>>(Properties.Resources.Countries);
        }

        /// <summary>
        /// Retrieve all the Listed countries from http://restcountries.eu/
        /// </summary>
        /// <returns>Collection of <see cref="Country"/></returns>
        public async Task<IEnumerable<Country>> GetAll()
        {
            return countries;
        }

        /// <summary>
        /// Retrieve a specific Country by the search criteria.
        /// </summary>
        /// <param name="filters"></param>
        /// <returns>Filtered collection of <see cref="Country"/></returns>
        public async Task<IEnumerable<Country>> Get(IEnumerable<IFilterRule> filters)
        {
            var matchingCountries = new List<Country>();
            var type = typeof(Country);
            Parallel.ForEach(countries, async (country) =>
            //foreach(var country in countries)
            {
                foreach (var rule in filters)
                {
                    var passed = await Strategy.For<IAnalyzerStrategy>().Passes<Country>(country, rule);
                    if (passed)
                        matchingCountries.Add(country);
                }
            });
            return matchingCountries;
        }

        #endregion
    }

    #region Classes

    public class Country
    {
        public string Name { get; set; }
        public string Capital { get; set; }
        public string[] AltSpellings { get; set; }
        public string Relevance { get; set; }
        public string Region { get; set; }
        public string Subregion { get; set; }
        public Translations Translations { get; set; }
        public int Population { get; set; }
        public float?[] Latlng { get; set; }
        public string Demonym { get; set; }
        public float? Area { get; set; }
        public float? Gini { get; set; }
        public string[] Timezones { get; set; }
        public string[] Borders { get; set; }
        public string NativeName { get; set; }
        public string[] CallingCodes { get; set; }
        public string[] TopLevelDomain { get; set; }
        public string Alpha2Code { get; set; }
        public string Alpha3Code { get; set; }
        public string[] Currencies { get; set; }
        public string[] Languages { get; set; }

        /// <summary>
        /// Uses reflection to find out if a property exists with a value.
        /// </summary>
        /// <param name="variableName"></param>
        /// <param name="operands"></param>
        /// <returns></returns>
        public bool HasValueEqualTo(string variableName, string operands)
        {
            var property = this.GetType().GetProperty(variableName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (property == null)
                return false; 

            try
            {
                var value = property.GetValue(this);
                var newValue = Convert.ChangeType(operands, property.PropertyType);

                if (property.PropertyType == typeof (string))
                {
                    return value.ToString().IsEqualTo(operands);
                }
                else
                {
                    return newValue.Equals(value);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    public class Translations
    {
        public string de { get; set; }
        public string es { get; set; }
        public string fr { get; set; }
        public string ja { get; set; }
        public string it { get; set; }
    }


    #endregion
}