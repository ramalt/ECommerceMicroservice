using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CourseApp.Shared.Dtos
{
    public class ResponseDto<T>
    {
        public T Data { get; private set; }
        [JsonIgnore] //for only internal control logic ops.
        public int StatusCode { get; private set; }
        [JsonIgnore]
        public bool IsSuccesfull { get; private set; }
        public List<string> Errors { get; private set; }

        public static ResponseDto<T> Success(T data, int statusCode) => new ResponseDto<T> { Data = data, StatusCode = statusCode, IsSuccesfull = true };
        public static ResponseDto<T> Success(int statusCode) => new ResponseDto<T> { Data = default(T), StatusCode = statusCode, IsSuccesfull = true };
        public static ResponseDto<T> Fail(int statusCode, List<string> errors) => new ResponseDto<T> { StatusCode = statusCode, Errors = errors, IsSuccesfull = true };
        public static ResponseDto<T> Fail(int statusCode, string error) => new ResponseDto<T> { StatusCode = statusCode, Errors = new List<string>() { error }, IsSuccesfull = true };

    }
}