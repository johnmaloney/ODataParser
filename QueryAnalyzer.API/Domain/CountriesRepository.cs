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
    public class CountriesRepository
    {
        #region Fields

        private IEnumerable<Country> countries;

        #endregion

        #region Properties
        #endregion

        #region Methods

        public CountriesRepository()
        {
            countries = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Country>>(Properties.Resources.Countries);
        }

        public async Task<IEnumerable<Country>> GetAll()
        {
            return countries;
        }

        public async Task<IEnumerable<Country>> Get(IEnumerable<IFilterRule> filters)
        {
            var matchingCountries = new List<Country>();
            Parallel.ForEach(countries, (country) =>
            {
                foreach (var rule in filters)
                {
                    if (country.HasValueEqualTo(rule.VariableName, rule.Operands))
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