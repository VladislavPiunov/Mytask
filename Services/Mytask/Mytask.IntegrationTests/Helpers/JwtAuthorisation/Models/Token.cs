using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Mytask.IntegrationTests.Helpers.JwtAuthorisation.Models;

internal class Token
{
    /// <summary>
    /// The access token issued by the authorization server.
    /// </summary>
    [JsonProperty("access_token")]
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = null!;

    /// <summary>
    /// The lifetime in seconds of the access token.  For example, the value "3600" denotes that the access token will expire in one hour from the time the response was generated. If omitted, the authorization server SHOULD provide the expiration time via other means or document the default value.
    /// </summary>
    [JsonProperty("expires_in")]
    [JsonPropertyName("expires_in")]
    public long ExpiresIn { get; set; }

    /// <summary>
    /// The lifetime in seconds of the refresh token.
    /// </summary>
    [JsonProperty("refresh_expires_in")]
    [JsonPropertyName("refresh_expires_in")]
    public long RefreshExpiresIn { get; set; }

    /// <summary>
    /// The refresh token, which can be used to obtain new access tokens using the same authorization grant
    /// </summary>
    [JsonProperty("refresh_token")]
    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; }

    /// <summary>
    /// The type of the token issued. Value is case insensitive.
    /// </summary>
    [JsonProperty("token_type")]
    [JsonPropertyName("token_type")]
    public string TokenType { get; set; } = null!;

    /// <summary>
    /// Pushing a not-before policy ensures that any tokens issued before that time become invalid.
    /// </summary>
    [JsonProperty("not-before-policy")]
    [JsonPropertyName("not-before-policy")]
    public long NotBeforePolicy { get; set; }

    /// <summary>
    /// A session is attached to a user and the client used to log in. From the session, access token and refresh token are forged. The « session_state » field in those tokens is the id of the session. 
    /// </summary>
    [JsonProperty("session_state")]
    [JsonPropertyName("session_state")]
    public Guid SessionState { get; set; }

    /// <summary>
    ///  The scope of the access token
    /// </summary>
    [JsonProperty("scope")]
    [JsonPropertyName("scope")]
    public string Scope { get; set; }
}