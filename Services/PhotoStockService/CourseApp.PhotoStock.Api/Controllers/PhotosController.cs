using PhotoStockService.CourseApp.PhotoStock.Api.Dtos;
using CourseApp.Shared;
using CourseApp.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using SystemFile = System.IO.File;

namespace CourseApp.PhotoStock.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PhotosController : CustomBaseController
{
    [HttpPost]
    public async Task<IActionResult> SavePhoto(IFormFile file, CancellationToken cancellationToken)
    {
        if (file is not null && file.Length > 0)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", file.FileName); // ~/wwwroot/photos/example.jpg

            using Stream stream = new FileStream(path, FileMode.Create);

            await file.CopyToAsync(stream, cancellationToken);

            PhotoDto photoDto = new() { Url = file.FileName };
            return CreateActionResult(Response<PhotoDto>.Success(photoDto, 200));
        }
        return CreateActionResult(Response<PhotoDto>.Fail(200, "photo is empty"));
    }

    [HttpDelete]
    public IActionResult DeletePhoto(string url)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", url); // ~/wwwroot/photos/example.jpg

        if (!SystemFile.Exists(path))
        {
            return CreateActionResult(Response<PhotoDto>.Fail(200, "photo not found"));
        }
        SystemFile.Delete(path);

        return NoContent();
    }
}
