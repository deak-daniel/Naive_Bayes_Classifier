using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bayes
{
    /// <summary>
    /// Static class for list extension methods
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Gives back the values for testing
        /// </summary>
        /// <param name="list">Preferably the test list.</param>
        /// <returns>The values in a correct format</returns>
        public static string ValueToString(this NaiveBayes.NaiveBayesData list)
        {
            string res = "";
            for (int i = 0; i < list.Count; i++)
            {
                res += $"{list[i].Value},";
            }
            return res.Trim(',');
        }
    }
}
