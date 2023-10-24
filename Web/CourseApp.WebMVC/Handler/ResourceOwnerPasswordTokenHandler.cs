using CourseApp.WebMVC.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Net;
using System.Net.Http.Headers;

namespace CourseApp.WebMVC.Handler;

public class ResourceOwnerPasswordTokenHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IIdentityService _identityService;
    private ILogger<ResourceOwnerPasswordTokenHandler> _logger;

    public ResourceOwnerPasswordTokenHandler(ILogger<ResourceOwnerPasswordTokenHandler> logger, IIdentityService identityService, IHttpContextAccessor contextAccessor)
    {
        _logger = logger;
        _identityService = identityService;
        _contextAccessor = contextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var accessToken = await _contextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer",accessToken);

        var response = await base.SendAsync(request, cancellationToken);

        if(response.StatusCode == HttpStatusCode.Unauthorized)
        {
            var tokenResponse = await _identityService.GetAccessTokenByRefreshToken();
            if (tokenResponse is not null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer",tokenResponse.AccessToken);

                response = await base.SendAsync(request, cancellationToken);
                
            }
        }

        if(response.StatusCode == HttpStatusCode.Unauthorized)
        {
            throw new UnauthorizedAccessException();
        }

        return response;
    }


}
