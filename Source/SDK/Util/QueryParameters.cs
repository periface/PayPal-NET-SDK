using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayPal.Util
{
    public class QueryParameters : Dictionary<string, string>
    {
        /// <summary>
        /// Converts the dictionary of query parameters to a URL-formatted string. Empty values are ommitted from the parameter list.
        /// </summary>
        /// <param name="includeLeadingQueryToken">If true, prepends a '?' character to the query string.</param>
        /// <returns>A URL-formatted string containing the query parameters</returns>
        public string ToUrlFormattedString(bool includeLeadingQueryToken = true)
        {
            return this.Aggregate
            (
                "",
                (parameters, item) =>
                    parameters + (string.IsNullOrEmpty(item.Value) ? "" : ((string.IsNullOrEmpty(parameters) ? (includeLeadingQueryToken ? "?" : "") : "&") + string.Format("{0}={1}", item.Key, HttpUtility.UrlEncode(item.Value))))
            );
        }
    }    
}