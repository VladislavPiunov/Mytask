using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Mytask.API.Extensions.Auth.Options;

namespace Mytask.API.Extensions.Auth
{
    public static class KeycloakAuthentication
    {
        public static IServiceCollection AddKeycloakAuthentication(this IServiceCollection services, ConfigurationManager configuration)
        {
            var keycloakOptions = configuration
                .GetSection("KeycloakOptions").Get<KeycloakOptions>()
                ?? throw new ArgumentNullException(nameof(KeycloakOptions));

            services.Configure<KeycloakOptions>(configuration.GetSection("KeycloakOptions"));

            services
                .AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
                {
                    opt.Authority = $"{keycloakOptions.Host}:{keycloakOptions.Port}/auth/realms/{keycloakOptions.Realm}/";
                    opt.Audience = keycloakOptions.ClientId;
                    opt.RequireHttpsMetadata = false; // if ssl
                    opt.SaveToken = true;
                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = $"{keycloakOptions.Host}:{keycloakOptions.Port}/auth/realms/{keycloakOptions.Realm}/"
                    };
                });

            return services;
        }
    }
}
