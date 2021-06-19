using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace akowe_CRUD.Errors
{
    public class ApiError
    {
        public ApiError(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad request",
                401 => "UnAuthorized",
                403 => "Forbidden",
                404 => "Resource not found",
                500 => "Server Error, do not panic we just got notified",
                _ => null
            };
        }
    }

    public class ApiException : ApiError
    {
        public ApiException(int statusCode, string message = null, string details = null) : base(statusCode, message)
        {
            Details = details;

        }
        public string Details { get; set; }
    }
    public class ApiValidationErrorError : ApiError
    {
        public ApiValidationErrorError() : base(400)
        {
        }
        public IEnumerable<string> Errors { get; set; }
    }
}
