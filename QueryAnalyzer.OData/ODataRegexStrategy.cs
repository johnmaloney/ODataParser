using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using QueryAnalyzer.Interfaces;

namespace QueryAnalyzer.Modules.OData
{
    public class ODataRegexStrategy : IRegex
    {
        #region Fields
        
        #endregion

        #region Properties
        public Regex DefaultVariableName { get; private set; }

        public Regex NumericVariableName { get; private set; }


        public Regex OperatorCheck { get; private set; }

        public Regex Operator { get; private set; }

        public Regex OperatorAlt { get; private set; }

        public Regex TextOperands { get; private set; }

        public Regex NumericOperands { get; private set; }


        public Regex Date { get; private set; }

        public Regex DateValue { get; set; }

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
            DefaultVariableName = new Regex(@"\((?!tolower)([^)]*)\)");
            NumericVariableName = new Regex(@"\A([^\s]+)");

            OperatorCheck = new Regex(@"(?:\A^[indexof]*|(?:eq\s-1|eq-1$))");
            Operator = new Regex(@"(?:\A^[^\( ^\d]*|(eqnull|nenull|ne|gt|ge|lt|le|add|sub|mul|div|mod)|(?:eq\s-1|eq-1$))");
            OperatorAlt = new Regex(@"(?:\A^[^\( ^\d]*|(eqnull|nenull|eq|ne|gt|ge|lt|le|add|sub|mul|div|mod))");

            TextOperands = new Regex(@"\'(.+?)+\'");
            NumericOperands = new Regex(@"[-\d.]+$");

            Date = new Regex(@"\b(?:DateTime)\b");
            DateValue = new Regex(@"([""'])(?:(?=(\\?))\2.)*?\1");
            DayMonthYear = new Regex(@"\b(?:day|month|year)\b");
            MonthYear = new Regex(@"\bmonth\b.*?\byear\b.*?\d+");
            Year = new Regex(@"\byear\b.*?\d+");
            AndConjunction = new Regex(this.AndConjunctionPattern);
            OrConjunction = new Regex(this.OrConjunctionPattern);
        }

        #endregion

    }
}
