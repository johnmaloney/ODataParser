using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using QueryAnalyzer.Interfaces;

namespace QueryAnalyzer.OData
{
    public class ODataRegexStrategy : IRegex
    {
        #region Fields
        
        #endregion

        #region Properties

        public Regex DayMonthYear { get; private set; }

        public Regex MonthYear { get; private set; }

        public Regex Year { get; private set; }

        public Regex AndConjunction { get; private set; }
        public string AndConjunctionPattern { get { return @"(?<=^([^']|'[^']*')*)\band\b"; } }

        public Regex OrConjunction { get; private set; }
        public string OrConjunctionPattern { get { return @"(?<=^([^']|'[^']*')*)\bor\b"; } }

        #endregion

        #region Methods

        public ODataRegexStrategy()
        {
            DayMonthYear = new Regex(@"\bday\b.*?\bmonth\b.*?\byear\b.*?\d + ");
            MonthYear = new Regex(@"\bmonth\b.*?\byear\b.*?\d+");
            Year = new Regex(@"\byear\b.*?\d+");
            AndConjunction = new Regex(this.AndConjunctionPattern);
            OrConjunction = new Regex(this.OrConjunctionPattern);
        }

        #endregion

    }
}
