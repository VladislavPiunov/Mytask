using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Mytask.API.Dto;
using Mytask.API.Extensions;
using Mytask.API.Extensions.Auth.Options;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Mytask.API.Controllers
{
    [ApiController]
    [Route("api/keycloak")]
    public class KeycloakController : Controller
    {
        private IHttpClientFactory _httpClientFactory;

        private KeycloakOptions _keycloakOptions;

        public KeycloakController(IHttpClientFactory httpClientFactory, IOptionsSnapshot<KeycloakOptions> keycloakOptions)
        {
            _httpClientFactory = httpClientFactory;
            _keycloakOptions = keycloakOptions?.Value ?? throw new ArgumentNullException(nameof(keycloakOptions));
        }


        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetUsersAsync()
        {
            var client = _httpClientFactory.CreateClient();

            var data = new[]
            {
                new KeyValuePair<string, string>("client_id", _keycloakOptions.ClientId),
                new KeyValuePair<string, string>("client_secret", _keycloakOptions.ClientSecret),
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("scope", "openid"),
                new KeyValuePair<string, string>("username", _keycloakOptions.Username),
                new KeyValuePair<string, string>("password", _keycloakOptions.Password)
            };

            using var keycloakResponse = await client.PostAsync($"{_keycloakOptions.Host}:{_keycloakOptions.Port}/realms/{_keycloakOptions.Realm}/protocol/openid-connect/token", new FormUrlEncodedContent(data));

            var accessToken = keycloakResponse.Content.ReadAs<TokenDto>()!.AccessToken;

            var msg = new HttpRequestMessage(HttpMethod.Get, $"{_keycloakOptions.Host}:{_keycloakOptions.Port}/admin/realms/{_keycloakOptions.Realm}/users");
            msg.Headers.Authorization = AuthenticationHeaderValue.Parse("Bearer " + accessToken);

            using var response = await client.SendAsync(msg);

            var result = response.Content.ReadAs<List<UserDto>>();
            return Ok(result);
        }
    }
}
