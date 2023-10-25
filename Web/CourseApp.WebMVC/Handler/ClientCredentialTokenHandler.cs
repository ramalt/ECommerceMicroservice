using System.Net;
using System.Net.Http.Headers;
using CourseApp.WebMVC.Exceptions;
using CourseApp.WebMVC.Services.Interfaces;

namespace CourseApp.WebMVC.Handler;

public class ClientCredentialTokenHandler : DelegatingHandler
{
    private readonly IClientCredentialTokenService _tokenService;

    public ClientCredentialTokenHandler(IClientCredentialTokenService tokenService)
    {
        _tokenService = tokenService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _tokenService.GetToken());
        var response = await base.SendAsync(request, cancellationToken);

        if(response.StatusCode == HttpStatusCode.Unauthorized)
            throw new UnauthorizeException();

        return response;
    }
}
