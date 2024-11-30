using System.Net;

namespace TaskManagement.Application.Exceptions
{
    public class CustomException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public CustomException(HttpStatusCode statusCode, string message, Exception innerException)
            : base(message, innerException)
        {
            this.StatusCode = statusCode;
        }
    }
}
