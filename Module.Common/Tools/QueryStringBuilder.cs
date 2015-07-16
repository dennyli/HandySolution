using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.Module.Common.Tools
{
    /// <summary>
    /// Class thanks to Karl Shifflett
    /// http://karlshifflett.wordpress.com/
    /// </summary>
    public class QueryStringBuilder
    {

        private QueryStringBuilder()
        {
        }

        /// <summary>
        /// Constructs the specified Uri.
        /// </summary>
        /// <param name="protocol">The protocol.  Example:  http://</param>
        /// <param name="address">The address. Example www.bing.com or ApplicationMenuView</param>
        /// <param name="parms">The parms.
        /// <code>string[,] parms = { {"itemId", "1255"}, {"action", "edit"} }</code>
        /// </param>
        /// <returns></returns>
        public static String Construct(String protocol, String address, String[,] parms)
        {
            return String.Concat(protocol, address, ParseParameters(parms));
        }

        /// <summary>
        /// Constructs the specified Uri.
        /// </summary>
        /// <param name="address">The address. Example www.bing.com or ApplicationMenuView</param>
        /// <param name="parms">The parms.
        /// <code>string[,] parms = { {"itemId", "1255"}, {"action", "edit"} }</code>        
        /// </param>
        /// <returns></returns>
        public static String Construct(String address, String[,] parms)
        {
            return String.Concat(address, ParseParameters(parms));
        }

        static String ParseParameters(String[,] parms)
        {
            if (parms == null || parms.Length == 0)
            {
                return String.Empty;
            }

            var sb = new StringBuilder();
            String token = "?";

            for (Int32 i = 0; i < parms.Length - 1; i++)
            {
                sb.AppendFormat("{0}{1}={2}", token, parms[i, 0], parms[i, 1]);
                token = "&";
            }
            return sb.ToString();
        }
    }
}
