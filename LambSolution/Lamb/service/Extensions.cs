using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Amazon.Runtime;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Lamb.service
{
    public static class HttpStatusCodeExtensions
    {
        public static bool IsSuccessCode(this HttpStatusCode code)
        {
            return Constants.SUCCESS_CODES.Contains(code);
        }
    }

    public static class AmazonWebServiceResponseExtensions
    {
        public static void EnsureSuccessResponse(this AmazonWebServiceResponse r, string apiMethod)
        {
            if (!Constants.SUCCESS_CODES.Contains(r.HttpStatusCode))
            {
                StringBuilder msg = new StringBuilder();
                msg.AppendFormat("API call failed: {0}{1}", apiMethod, Environment.NewLine);
                msg.AppendFormat("Return code: {0}{1}", r.HttpStatusCode, Environment.NewLine);
                throw new ApplicationException(msg.ToString());
            }
        }
    }

    public static class StringExtensions
    {
        /// <summary>
        ///     a less verbose shortcut for String.Equals, using StringComparison.InvariantCultureIgnoreCase,
        ///     which is pretty much what we want to use everywhere (since our SQL db's are set up case-insensitive)
        /// </summary>
        /// <param name="s"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool CIEquals(this string s, string val)
        {
            return s.Equals(val, StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool CIContains(this IEnumerable<string> strings, string val)
        {
            return strings.Contains(val, StringComparer.InvariantCultureIgnoreCase);
        }

        public static bool CIStartsWith(this string s, string val)
        {
            return s.StartsWith(val, StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool IsValidJson(this string toBeValidated)
        {
            string s = toBeValidated.Trim();
            bool valid = false;
            if ((s.StartsWith("{") && s.EndsWith("}")) || (s.StartsWith("[") && s.EndsWith("]")))
            {
                try
                {
                    var x = JToken.Parse(s);
                    valid = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return valid;
        }

        public static string PrettyPrintJson(this string js)
        {
            string formatted;
            if (js.IsValidJson())
            {
                var parsed = JObject.Parse(js);
                formatted = parsed.ToString(Formatting.Indented);
            }
            else
            {
                formatted = "The returned data could not be parsed as JSON!" + Environment.NewLine;
            }
            return formatted;
        }
    }
}