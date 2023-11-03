using Mytask.IntegrationTests.Helpers.JwtAuthorisation.Models;

namespace Mytask.IntegrationTests.Helpers.JwtAuthorisation;

/// <summary>
/// Генератор ответов от KeyCloak
/// </summary>
internal static class KeyCloakResponseGenerator
{
    /// <summary>
    /// Эмулируем ответ от кейклоак на запрос конфигурации
    /// </summary>
    public static OpenidConfiguration GetOpenidConfiguration(string realm)
    {
        return new OpenidConfiguration
        {
            Issuer = $"http://localhost:4501/auth/realms/{realm}",
            AuthorizationEndpoint = $"http://localhost:4501/auth/realms/{realm}/protocol/openid-connect/auth",
            TokenEndpoint = $"http://localhost:4501/auth/realms/{realm}/protocol/openid-connect/token",
            UserinfoEndpoint = $"http://localhost:4501/auth/realms/{realm}/protocol/openid-connect/userinfo",
            JwksUri = $"http://localhost:4501/auth/realms/{realm}/protocol/openid-connect/certs",
            CheckSessionIframe = $"http://localhost:4501/auth/realms/{realm}/protocol/openid-connect/login-status-iframe.html",
            GrantTypesSupported = new List<string>
            {
                "authorization_code",
                "implicit",
                "refresh_token",
                "password",
                "client_credentials"
            },
            ResponseTypesSupported = new List<string>
            {
                "code",
                "none",
                "id_token",
                "token",
                "id_token token",
                "code id_token",
                "code token",
                "code id_token token"
            },
            SubjectTypesSupported = new List<string>
            {
                "public",
                "pairwise"
            },
            IdTokenSigningAlgValuesSupported = new List<string>
            {
                "RS256"
            },
            IdTokenEncryptionAlgValuesSupported = new List<string>
            {
                "RSA-OAEP",
                "RSA1_5"
            },
            IdTokenEncryptionEncValuesSupported = new List<string>
            {
                "A128GCM",
                "A128CBC-HS256"
            },
            UserinfoSigningAlgValuesSupported = new List<string>
            {
                "RS256"
            },
            RequestObjectSigningAlgValuesSupported = new List<string>
            {
                "RS256"
            },
            ResponseModesSupported = new List<string>
            {
                "query",
                "fragment",
                "form_post"
            },
            RegistrationEndpoint =
                $"http://localhost:4501/auth/realms/{realm}/clients-registrations/openid-connect",
            TokenEndpointAuthMethodsSupported = new List<string>
            {
                "private_key_jwt",
                "client_secret_basic",
                "client_secret_post",
                "tls_client_auth",
                "client_secret_jwt"
            },
            TokenEndpointAuthSigningAlgValuesSupported = new List<string>
            {
                "RS256"
            },
            ClaimsSupported = new List<string>
            {
                "aud",
                "sub",
                "iss",
                "auth_time",
                "name",
                "given_name",
                "family_name",
                "preferred_username",
                "clientId",
                "email",
                "acr"
            },
            ClaimTypesSupported = new List<string>
            {
                "normal"
            },
            ClaimsParameterSupported = false,
            ScopesSupported = new List<string>
            {
                "openid",
                "profile",
                "email",
                "address",
                "phone",
                "offline_access",
                "roles",
                "web-origins",
                "microprofile-jwt",
                "aud-fins",
                "aud-my-app"
            },
            RequestParameterSupported = true,
            RequestUriParameterSupported = true,
            CodeChallengeMethodsSupported = new List<string>
            {
                "plain",
                "S256"
            },
            TlsClientCertificateBoundAccessTokens = true,
            IntrospectionEndpoint = $"http://localhost:4501/auth/realms/{realm}/protocol/openid-connect/token/introspect"

        };
    }

    /// <summary>
    /// Эмулируем ответ от кейклоака на запрос сертификата для проверки токена
    /// </summary>
    public static Certificates GetCertificates()
    {
        return new Certificates()
        {
            Keys = new List<Key>()
            {
                new()
                {
                    Kid = "0-CDYkLdNn_158pWcWIH3H_sO5m-eu2uBXH0KZE1pZM",
                    Kty = "RSA",
                    Alg = "RS256",
                    Use = "sig",
                    N = "jWrUiboTNuiejJM9jY0K_oCiuhoiZje18VQH7KlPWnihA7S8SvpVclr5nxuT75Rg2iBpR6RmfDfubkuMDOXlT86PRcgMAkDAFfoZ8OGlhKGLGgVSf-Tduv8fJ7lN28OgEzEFgE8G-B84FwOeoUV7VxMk39MxAsN6ajXM-IU-gLohj4c5UKrWHjor3tdqrYSjIZmK7-tcCR-_0U9GXPu-mHYKEi9B5WmyO-EoQ31OUjghFwCXJYMsvSiLIoDbX_P9I6NUkZkJRPYcL96ixLpRHKC5hGL61tEOFBlvFu1mDvkX6swU5EfD1sxlME7Ytnzw98S0yPzYcaYXI_WGWb2QsQ",
                    E = "AQAB",
                    X5C = new[]
                    {
                        "MIICmzCCAYMCBgFhUT+/kjANBgkqhkiG9w0BAQsFADARMQ8wDQYDVQQDDAZCcm9rZXIwHhcNMTgwMjAxMTIwMTI3WhcNMjgwMjAxMTIwMzA3WjARMQ8wDQYDVQQDDAZCcm9rZXIwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQCNatSJuhM26J6Mkz2NjQr+gKK6GiJmN7XxVAfsqU9aeKEDtLxK+lVyWvmfG5PvlGDaIGlHpGZ8N+5uS4wM5eVPzo9FyAwCQMAV+hnw4aWEoYsaBVJ/5N26/x8nuU3bw6ATMQWATwb4HzgXA56hRXtXEyTf0zECw3pqNcz4hT6AuiGPhzlQqtYeOive12qthKMhmYrv61wJH7/RT0Zc+76YdgoSL0HlabI74ShDfU5SOCEXAJclgyy9KIsigNtf8/0jo1SRmQlE9hwv3qLEulEcoLmEYvrW0Q4UGW8W7WYO+RfqzBTkR8PWzGUwTti2fPD3xLTI/Nhxphcj9YZZvZCxAgMBAAEwDQYJKoZIhvcNAQELBQADggEBAEP7uuF7qtcxgyUPH1NlvsWIaOvDCmK2/xjnnr2UKzhiwXPvr+7QVNmH+oM+AeNUgXEy4B7C1mHQ4dYJd/QD7XGYE4nEadEnr/heo3Zj9RPX+ldq9ttFutUVTTW/7bESkaxEmoK018LZDlnTkiA8Q6ZPN1K/eqUJVfZ60aoNUnDG6UPFROWtmG8uB9fnos1SUBnroy+cEjtSTEnear1DpDk9DXwBeeCU91YYLzjOO/RmBLxgG2WuLkL4bWSLz/RDTRyVeCDpi7SoTzrg/NxoKtGvBUyxTAdyI6TBRo56I4O8lzD7pOUDjSN+khfM18JW9kUO6lsfecg5EJznf5GONG4="
                    },
                    X5T = "n5fx46hNQ83PWi9k8gp3ULhVU9c",
                    Hash = "Gk6c2paGOti48FHFNfG2VkhDhePaQ7gaAXQazyJ1ZFs"
                }
            }
        };
    }
        
    /// <summary>
    /// Эмулируем ответ от кейклоака на запрос токена
    /// </summary>
    public static Token GetToken(string realm, Dictionary<string, string> claims)
    {
        return new Token
        {
            AccessToken =  new JwtTokenBuilder().WithIssuer(realm).WithDefaultClaims().WithClaims(claims).CreateToken(),
            ExpiresIn = 100000,
            NotBeforePolicy = 1518534968,
            SessionState = Guid.NewGuid(),
            Scope = "email profile",
            RefreshToken =  new JwtTokenBuilder().WithIssuer(realm).CreateToken(),
            RefreshExpiresIn = 18000000,
            TokenType =  "Bearer"
        };
    }
}