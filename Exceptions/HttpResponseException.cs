using System.Net;
namespace quizon.Exceptions
{
    public class HttpResponseException : Exception
    {
        public HttpStatusCode Status { get; set; } = HttpStatusCode.InternalServerError;
        public object Value { get; set; }
        public string Description { get; set; }
        public HttpResponseException(HttpStatusCode code, string description = "", object Value = null)
        {
            Status = code;
            Description = description;
            this.Value = Value;
        }
    }
}
