using CourseApp.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CourseApp.Shared
{

    public class CustomBaseController : ControllerBase
    {
        public IActionResult CreateActionResult<T>(Response<T> response)
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}