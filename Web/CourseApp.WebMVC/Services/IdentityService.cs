using System.Globalization;
using System.Security.Claims;
using System.Text.Json;
using CourseApp.Shared.Dtos;
using CourseApp.WebMVC.Models;
using CourseApp.WebMVC.Services.Interfaces;
using CourseApp.WebMVC.Settings;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace CourseApp.WebMVC.Services;

public class IdentityService : IIdentityService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly ClientSettings _clientSettings;
    private readonly ServiceApiSettings _serviceSettings;

    public IdentityService(IHttpContextAccessor contextAccessor, HttpClient client, IOptions<ClientSettings> clientSettings, IOptions<ServiceApiSettings> serviceSettings)
    {
        _contextAccessor = contextAccessor;
        _httpClient = client;
        _clientSettings = clientSettings.Value;
        _serviceSettings = serviceSettings.Value;
    }

    public async Task<TokenResponse> GetAccessTokenByRefreshToken()
    {
        var discovery = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest(){
            Address = _serviceSettings.IdentityBaseUri,
            Policy = new DiscoveryPolicy(){RequireHttps = false}
        });

        if(discovery.IsError)
            throw discovery.Exception ?? new Exception("Somethings went wrong :/");

        var refreshToken = await _contextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

        RefreshTokenRequest refreshTokenRequest = new(){
            ClientId = _clientSettings.WebUserClientSettings.ClientId,
            ClientSecret = _clientSettings.WebUserClientSettings.ClientSecret,
            RefreshToken = refreshToken,
            Address = discovery.TokenEndpoint
        };

        var token = await _httpClient.RequestRefreshTokenAsync(refreshTokenRequest);

        if(token.IsError)
            return null;

        
        var authTokens = new List<AuthenticationToken>()
        {
            new AuthenticationToken{Name = OpenIdConnectParameterNames.AccessToken, Value = token.AccessToken},
            new AuthenticationToken{Name = OpenIdConnectParameterNames.RefreshToken, Value = token.RefreshToken},
            new AuthenticationToken{Name = OpenIdConnectParameterNames.ExpiresIn, Value = DateTime.Now.AddSeconds(token.ExpiresIn).ToString("O", CultureInfo.InvariantCulture)},
        };

        var authResult = await _contextAccessor.HttpContext.AuthenticateAsync();
        var props = authResult.Properties;
        props.StoreTokens(authTokens);

        await _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, authResult.Principal, props);

        return token;

    }

    public async Task RevokeRefreshToken()
    {
        var discovery = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest(){
            Address = _serviceSettings.IdentityBaseUri,
            Policy = new DiscoveryPolicy(){RequireHttps = false}
        });

        if(discovery.IsError)
            throw discovery.Exception ?? new Exception("Somethings went wrong :/");

        var refreshToken = await _contextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

        TokenRevocationRequest tokenRevocationRequest = new(){
            ClientId = _clientSettings.WebUserClientSettings.ClientId,
            ClientSecret = _clientSettings.WebUserClientSettings.ClientSecret,
            Address = discovery.TokenEndpoint,
            Token = refreshToken,
            TokenTypeHint = "refresh_token"
        };

        await _httpClient.RevokeTokenAsync(tokenRevocationRequest);
    }

    public async Task<Response<bool>> SingIn(SigninInput signinInput)
    {
        var discovery = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest(){
            Address = _serviceSettings.IdentityBaseUri,
            Policy = new DiscoveryPolicy(){RequireHttps = false}
        });

        if(discovery.IsError)
            throw discovery.Exception ?? new Exception("Somethings went wrong :/");


        var passwordTokenRequest = new PasswordTokenRequest(){
            ClientId = _clientSettings.WebUserClientSettings.ClientId,
            ClientSecret = _clientSettings.WebUserClientSettings.ClientSecret,
            UserName = signinInput.Email,    
            Password = signinInput.Password,
            Address = discovery.TokenEndpoint
        };

        var token = await _httpClient.RequestPasswordTokenAsync(passwordTokenRequest);

        if(token.IsError)
        {
            var responseContext = await token.HttpResponse.Content.ReadAsStringAsync();
            var errorDto = JsonSerializer.Deserialize<ErrorDto>(responseContext, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});

        return Shared.Dtos.Response<bool>.Fail(400, errorDto.Errors);
        }

        var userInfoRequest = new UserInfoRequest(){
            Token = token.AccessToken,
            Address = discovery.UserInfoEndpoint,
        };

        var userInfo = await _httpClient.GetUserInfoAsync(userInfoRequest);

        if(userInfo.IsError)
            throw userInfo.Exception ?? new Exception("Somethings went wrong with userInfo:/");



        ClaimsIdentity claimsIdentity = new(userInfo.Claims, CookieAuthenticationDefaults.AuthenticationScheme, "name", "role");

        ClaimsPrincipal claimsPrincipal = new(claimsIdentity);

        var authhenticationProps = new AuthenticationProperties();
        authhenticationProps.StoreTokens(new List<AuthenticationToken>()
        {
            new AuthenticationToken{Name = OpenIdConnectParameterNames.AccessToken, Value = token.AccessToken},
            new AuthenticationToken{Name = OpenIdConnectParameterNames.RefreshToken, Value = token.RefreshToken},
            new AuthenticationToken{Name = OpenIdConnectParameterNames.ExpiresIn, Value = DateTime.Now.AddSeconds(token.ExpiresIn).ToString("O", CultureInfo.InvariantCulture)},
        });


        authhenticationProps.IsPersistent = signinInput.RememberMe;

        await _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authhenticationProps);


        return Shared.Dtos.Response<bool>.Success(200);
            


    }
}
