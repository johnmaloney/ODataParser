using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryAnalyzer.Common
{
    public static class DefaultExtensions
    {
        /// <summary>
        /// To be used throughout the system to allow for future string comparison changes without
        /// having to walk all the source code and find string.Equals or string.Compare or ==
        /// </summary>
        /// <param name="comparor"></param>
        /// <param name="comparee"></param>
        /// <param name="comparisonOptions"></param>
        /// <returns></returns>
        public static bool IsEqualTo(this string comparor, string comparee, StringComparison comparisonOptions = StringComparison.InvariantCultureIgnoreCase)
        {
            return string.Equals(comparor, comparee, comparisonOptions);
        }

        /// <summary>
        /// To be used throughout the system to allow for future string groups comparison changes without
        /// having to walk all the source code and find string.Equals or string.Compare or ==
        /// </summary>
        /// <param name="comparor"></param>
        /// <param name="comparees"></param>
        /// <param name="comparisonOptions"></param>
        /// <returns>True if any, False if none</returns>
        public static bool IsEqualToAny(this string comparor, params string[] comparees)
        {
            // IF ANY are equal then the WHOLE return is TRUE //
            foreach (string comparee in comparees)
                if (comparor.IsEqualTo(comparee))
                    return true;
            return false;
        }

        /// <summary>
        /// To be used throughout the system to allow for future string comparison changes without
        /// having to walk all the source code and find string.Equals or string.Compare or ==
        /// </summary>
        /// <param name="comparor"></param>
        /// <param name="comparee"></param>
        /// <param name="comparisonOptions"></param>
        /// <returns></returns>
        public static bool IsNotEqualTo(this string comparor, string comparee, StringComparison comparisonOptions = StringComparison.InvariantCultureIgnoreCase)
        {
            // Return the opposite of the IsEqualTo result //
            return !comparor.IsEqualTo(comparee, comparisonOptions);
        }
    }
}
