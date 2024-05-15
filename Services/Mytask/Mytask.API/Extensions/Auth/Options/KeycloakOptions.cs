namespace Mytask.API.Extensions.Auth.Options
{
    public class KeycloakOptions
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public string Realm { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
