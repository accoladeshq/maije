// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace Maije.SampleIdentity
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.Profile(),
                new IdentityResources.OpenId()
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
                new ApiResource("api1", "My API #1")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "spa",
                    ClientName = "SPA Client",
                    ClientUri = "http://identityserver.io",

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,

                    RedirectUris =
                    {
                        "http://localhost:5000/swagger/oauth2-redirect.html"
                    },

                    PostLogoutRedirectUris = { "http://localhost:5000/index.html" },
                    AllowedCorsOrigins = { "http://localhost:5000" },

                    AllowedScopes = { "api1" }
                }
            };
        }
    }
}