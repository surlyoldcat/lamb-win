using System.Net;

namespace Lamb.service
{
    public enum CredentialsType
    {
        FromProfile,
        UserEntered
    }

    public static class Constants
    {
        public static readonly HttpStatusCode[] SUCCESS_CODES =
        {
            HttpStatusCode.Accepted,
            HttpStatusCode.Created,
            HttpStatusCode.OK
        };

        public static readonly string NULL_STRING = "--none--";
    }
}