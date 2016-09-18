using System.Text;

namespace Lamb.model
{
    public class OperationResult
    {
        protected const string SUCCESS_STRING = "Result: Success.";
        protected const string FAIL_STRING = "Result: FAIL!";

        public bool Success { get; set; }
        public string Message { get; set; }

        public virtual string Format()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Success ? SUCCESS_STRING : FAIL_STRING);
            sb.AppendLine(Message);
            sb.AppendLine();
            return sb.ToString();
        }

        public static OperationResult Pass()
        {
            return new OperationResult
            {
                Success = true
            };
        }

        public static OperationResult Fail(string msg)
        {
            return new OperationResult
            {
                Success = false,
                Message = msg
            };
        }
    }
}