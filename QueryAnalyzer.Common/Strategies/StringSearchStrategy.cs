using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueryAnalyzer.Interfaces;

namespace QueryAnalyzer.Common.Strategies
{
    public class StringSearchStrategy : IStringSearchStrategy
    {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Methods

        public StringSearchStrategy()
        {
            
        }

        public bool HasWithin(string searchWitin, string searchFor)
        {
            char[] wordToSearchFor = searchFor.ToCharArray();
            
            char[] pattern  = new char[searchFor.Length];
            Array.Copy(wordToSearchFor, pattern, searchFor.Length);
            var table = buildTable(wordToSearchFor);

            var searchForLocation = search(wordToSearchFor, pattern, table);

            return searchForLocation > -1;
        }

        private int[] buildTable(char[] wordPattern)
        {
            int[] result = new int[wordPattern.Length];
            int position = 2;
            int currentIndex = 0;
            result[0] = -1;
            result[1] = 0;

            while (position < wordPattern.Length)
            {
                if (wordPattern[position - 1] == wordPattern[currentIndex])
                {
                    ++currentIndex;
                    result[position] = currentIndex;
                    ++position;
                }
                else if (currentIndex > 0)
                {
                    currentIndex = result[currentIndex];
                }
                else
                {
                    result[position] = 0;
                    ++position;
                }
            }
            return result;
        }

        private int search(char[] searchFor, char[] searchForPattern, int[] patternTable)
        {
            // look for this.W inside of S
            int m = 0;
            int i = 0;
            while (m + i < searchFor.Length)
            {
                if (searchForPattern[i] == searchFor[m + i])
                {
                    if (i == searchForPattern.Length - 1)
                        return m;
                    ++i;
                }
                else
                {
                    m = m + i - patternTable[i];
                    if (patternTable[i] > -1)
                        i = patternTable[i];
                    else
                        i = 0;
                }
            }
            return -1;  // not found
        }

        #endregion
    }
}
