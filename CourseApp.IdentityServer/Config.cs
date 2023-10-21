// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace CourseApp.IdentityServer
{
    public static class Config
    {

        public static IEnumerable<IdentityResource> IdentityResources =>
           new IdentityResource[]
           {
                new IdentityResources.OpenId(), //*required sub 
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
                new IdentityResource(){
                    Name = "roles",
                    DisplayName = "Roles",
                    Description = "User Roles",
                    UserClaims = new []{"role"}},

           };

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource("resource_catalog"){Scopes = {"catalog_fullpermission"}},
                new ApiResource("resource_photo_stock"){Scopes = {"photostock_fullpermission"}},
                new ApiResource("resource_payment_catalog"){Scopes = {"payment_fullpermission"}},
                new ApiResource("resource_basket"){Scopes = {"basket_fullpermission"}},
                new ApiResource("resource_discount"){Scopes = {"Discount_fullpermission", "Discount_read","Discount_write"}},
                new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
            };

        // api Scopes and permissions
        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("catalog_fullpermission", "Catalog Service full erişim"),
                new ApiScope("photostock_fullpermission", "PhotoStock Service full erişim"),
                new ApiScope("payment_fullpermission", "Payment Service full erişim"),
                new ApiScope("basket_fullpermission", "Basket Service full erişim"),
                new ApiScope("Discount_fullpermission", "Discount Service full erişim"),
                new ApiScope("Discount_read", "Discount Service okuma erişimi"),
                new ApiScope("Discount_write", "Discount Service yazma erişimi"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName),
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // m2m client credentials flow client
                new Client
                {
                    ClientName = "ASP.NET Core 6 MVC App",
                    ClientId = "MVCWebApp",
                    ClientSecrets = {new Secret("secret".Sha512())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes ={"catalog_fullpermission","photostock_fullpermission",IdentityServerConstants.LocalApi.ScopeName}
                },

                new Client
                {
                    ClientName = "ASP.NET Core 6 MVC App",
                    ClientId = "MVCWebAppUser",
                    AllowOfflineAccess = true, //refresh token activation
                    ClientSecrets = {new Secret("secret".Sha512())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,// clientId, ClientSecret, UserNamei Pass
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,//refresh token access
                        IdentityServerConstants.LocalApi.ScopeName,
                        "payment_fullpermission",
                        "basket_fullpermission",
                        "Discount_fullpermission",
                        "roles"},

                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    RefreshTokenUsage = TokenUsage.ReUse, //reusable refresh token
                    AccessTokenLifetime = 1*60*60, // 1 hour
                    AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60) - DateTime.Now).TotalSeconds, // 60 days
                    }
            };
    }
}