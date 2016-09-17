using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Amazon.Runtime;

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
        /// a less verbose shortcut for String.Equals, using StringComparison.InvariantCultureIgnoreCase,
        /// which is pretty much what we want to use everywhere (since our SQL db's are set up case-insensitive)
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
    }

}
