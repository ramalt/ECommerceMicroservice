using CourseApp.WebMVC.Services.Interfaces;
using CourseApp.WebMVC.Settings;
using IdentityModel.AspNetCore.AccessTokenManagement;
using IdentityModel.Client;
using Microsoft.Extensions.Options;

namespace CourseApp.WebMVC.Services;

public class ClientCredentialTokenService : IClientCredentialTokenService
{

    private readonly ServiceApiSettings _apiSettings;
    private readonly ClientSettings _clientSettings;
    private readonly IClientAccessTokenCache _clientTokenCache;
    private readonly HttpClient _httpClient;

    public ClientCredentialTokenService(IOptions<ServiceApiSettings> apiSettings, IOptions<ClientSettings> clientSettings, IClientAccessTokenCache clientTokenCachhe, HttpClient httpClient)
    {
        _apiSettings = apiSettings.Value;
        _clientSettings = clientSettings.Value;
        _clientTokenCache = clientTokenCachhe;
        _httpClient = httpClient;
    }

    public async Task<string> GetToken()
    {
        ClientAccessTokenParameters? tokenParameters = null; //?
        var currentToken  = await _clientTokenCache.GetAsync("WebClientToken", tokenParameters);

        if(currentToken is not null)
            return currentToken.AccessToken;
        
        var discovery = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest(){
            Address = _apiSettings.IdentityBaseUri,
            Policy = new DiscoveryPolicy(){RequireHttps = false}
        });

        if(discovery.IsError)
            throw discovery.Exception ?? new Exception("Somethings went wrong :/");

        var clientCredentialTokenRequest = new ClientCredentialsTokenRequest
        {
            ClientId = _clientSettings.WebClient.ClientId,
            ClientSecret = _clientSettings.WebClient.ClientSecret,
            Address = discovery.TokenEndpoint
        };

        var newToken = await _httpClient.RequestClientCredentialsTokenAsync(clientCredentialTokenRequest);

        if(newToken.IsError)
            throw newToken.Exception;

        await _clientTokenCache.SetAsync("WebClientToken", newToken.AccessToken, newToken.ExpiresIn, tokenParameters);

        return newToken.AccessToken;
    }
}
