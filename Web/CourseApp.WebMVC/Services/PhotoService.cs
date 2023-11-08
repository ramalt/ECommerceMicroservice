using CourseApp.WebMVC.Models.PhotoService;
using CourseApp.WebMVC.Services.Interfaces;
using Newtonsoft.Json;

namespace CourseApp.WebMVC.Services;

public class PhotoService : IPhotoService
{
    private readonly HttpClient _client;

    public PhotoService(HttpClient client)
    {
        _client = client;
    }

    public Task<bool> DeleteImage(string url)
    {
        throw new NotImplementedException();
    }

    public async Task<PhotoViewModel> UploadImage(IFormFile image)
    {
        if (image == null || image.Length <= 0)
        {
            return null;
        }

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";

        using var ms = new MemoryStream();

        await image.CopyToAsync(ms);

        var multipartContent = new MultipartFormDataContent
        {
            { new ByteArrayContent(ms.ToArray()), "file", fileName }
        };

        var response = await _client.PostAsync("photo", multipartContent);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        string jsonResponse = await response.Content.ReadAsStringAsync();
        var responseObject = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
        var dataToken = responseObject["data"];

        PhotoViewModel viewModel = new();

        if (dataToken is not null)
        {
            viewModel.Url = dataToken["url"];
        }
        Console.WriteLine($"--------------_____>{viewModel.Url}");
        return viewModel;

    }
}
