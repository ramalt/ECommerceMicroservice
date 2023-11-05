using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CourseApp.Shared.Dtos
{
    public class Response<T>
    {
        public T Data { get; private set; }
        [JsonIgnore] //for only internal control logic ops.
        public int StatusCode { get; private set; }
        [JsonIgnore]
        public bool IsSuccesfull { get; private set; }
        public List<string>? Errors { get; private set; }

        public static Response<T> Success(T data, int statusCode) => new Response<T> { Data = data, StatusCode = statusCode, IsSuccesfull = true };
        public static Response<T> Success(int statusCode) => new Response<T> { Data = default(T), StatusCode = statusCode, IsSuccesfull = true };
        public static Response<T> Fail(int statusCode, List<string> errors) => new Response<T> { StatusCode = statusCode, Errors = errors, IsSuccesfull = false };
        public static Response<T> Fail(int statusCode, string error) => new Response<T> { StatusCode = statusCode, Errors = new List<string>() { error }, IsSuccesfull = false };

    }
}