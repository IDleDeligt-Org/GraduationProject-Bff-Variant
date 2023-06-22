using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Sipster.IDP
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
               
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new[]
        {
            // local API scope
            new ApiScope(IdentityServerConstants.LocalApi.ScopeName),
            new ApiScope("sipsterapi", "sipsterapi")
        };

        public static IEnumerable<Client> Clients =>
            new Client[]
                {
                    new Client()
                    {
                        ClientName = "Sipster",
                        ClientId = "sipsterclient",
                        AllowedGrantTypes = GrantTypes.Code,
                        RequirePkce = true,
                        AccessTokenLifetime = 30,
                        RedirectUris =
                        {
                            "https://localhost:44461/account/login/callback"
                        },
                        FrontChannelLogoutUri = "https://localhost:44461/",
                        PostLogoutRedirectUris = { "https://localhost:44461/" },
                        AllowOfflineAccess = true,
                        AlwaysIncludeUserClaimsInIdToken = true,
                        AllowedScopes =
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            IdentityServerConstants.StandardScopes.Email,
                            "sipsterapi",

                        },
                        ClientSecrets =
                        {
                            new Secret("secret".Sha256())
                        }
                    }
                };
    }
}