using QueryAnalyzer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace QueryAnalyzer.Common.Filters
{
    public class DateTimePipe : AFilterPipe
    {

        #region Fields

        private readonly Regex dayMonthYear;
        private readonly Regex exactDate;

        #endregion

        #region Properties
        #endregion

        #region Methods
        
        public DateTimePipe()
        {
            this.dayMonthYear = Strategy.For<IRegex>().DayMonthYear;
            this.exactDate = Strategy.For<IRegex>().Date;
        }

        public override void Filter(string filteredText)
        {
            var hasDayMonthYear = dayMonthYear.IsMatch(filteredText);
            var hasExplicitDate = exactDate.IsMatch(filteredText);

            if (hasDayMonthYear | hasExplicitDate)
            {
                // When working with this Filter REMEMBER that the filter will //
                // be called multiple times since the filtering above this will split the //
                // string into parts based on the occurance of 'AND' or 'OR' combination operators //
                var dateFilter = this.FilterRule as IFilterDateRule;

                if (hasExplicitDate)
                {
                    dateFilter.DateType = DateTypeOperator.FullDate;
                    dateFilter.DateOperands = DateTime.Parse(exactDate.Match(filteredText).Value.Replace("'", string.Empty));
                }
                else if (hasDayMonthYear)
                {
                    var count = dayMonthYear.Matches(filteredText).Count;
                    var operandsFilter = new OperandsPipe();
                    var dateCollection = filteredText.Split(new string[] { "&&&", "|||" }, StringSplitOptions.None);
                    int dayOperand = DateTime.MinValue.Day;
                    int monthOperand = DateTime.MinValue.Month;
                    int yearOperand = DateTime.MinValue.Year;

                    switch (count)
                    {
                        case 1:
                            dateFilter.DateType = DateTypeOperator.YearOnly;

                            yearOperand = operandsFilter.GetNumericOperandsNumber(dateCollection[0].Trim());
                            break;
                        case 2:
                            dateFilter.DateType = DateTypeOperator.MonthYear;

                            monthOperand = operandsFilter.GetNumericOperandsNumber(dateCollection[0].Trim());
                            yearOperand = operandsFilter.GetNumericOperandsNumber(dateCollection[1].Trim());
                            break;
                        default:
                            dateFilter.DateType = DateTypeOperator.FullDate;

                            dayOperand = operandsFilter.GetNumericOperandsNumber(dateCollection[0].Trim());
                            monthOperand = operandsFilter.GetNumericOperandsNumber(dateCollection[1].Trim());
                            yearOperand = operandsFilter.GetNumericOperandsNumber(dateCollection[2].Trim());

                            break;
                    }

                    dateFilter.DateOperands = new DateTime(yearOperand, monthOperand, dayOperand, 0, 0, 0); //, DateTimeKind.Utc);
                }
            }

            while (HasNextPipe)
                Next<AFilterPipe>().Filter(filteredText, FilterRule);
        }

        #endregion
    }
}
