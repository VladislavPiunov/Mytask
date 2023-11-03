using Newtonsoft.Json;

namespace Mytask.IntegrationTests.Helpers.JwtAuthorisation.Models;

/// <summary>
/// Конфигурация Openid, см https://openid.net/specs/openid-connect-discovery-1_0-21.html
/// </summary>
internal class OpenidConfiguration
{
    /// <summary>
    /// URL using the https scheme with no query or fragment component that the OP asserts as its Issuer Identifier.
    /// </summary>
    [JsonProperty("issuer")]
    public string Issuer { get; set; } = null!;
      
    /// <summary>
    /// URL of the OP's OAuth 2.0 Authorization Endpoint
    /// </summary>
    [JsonProperty("authorization_endpoint")]
    public string AuthorizationEndpoint { get; set; }  = null!;
        
    /// <summary>
    /// URL of the OP's OAuth 2.0 Token Endpoint
    /// </summary>
    [JsonProperty("token_endpoint")]
    public string? TokenEndpoint { get; set; }
    
    /// <summary>
    /// URL of the OP's UserInfo Endpoint
    /// </summary>
    [JsonProperty("userinfo_endpoint")]
    public string? UserinfoEndpoint { get; set; }
    
    /// <summary>
    /// URL of the OP's JSON Web Key Set [JWK] document. This contains the signing key(s) the RP uses to validate signatures from the OP. 
    /// </summary>
    [JsonProperty("jwks_uri")]
    public string JwksUri { get; set; } = null!;
        
    /// <summary>
    /// https://connect2id.com/products/server/docs/api/check-session
    /// </summary>
    [JsonProperty("check_session_iframe")]
    public string CheckSessionIframe { get; set; }
        
    /// <summary>
    /// JSON array containing a list of the OAuth 2.0 Grant Type values that this OP supports.
    /// </summary>
    [JsonProperty("grant_types_supported")]
    public List<string>? GrantTypesSupported { get; set; }

    /// <summary>
    /// JSON array containing a list of the OAuth 2.0 response_type values that this OP supports.
    /// </summary>
    [JsonProperty("response_types_supported")]
    public List<string> ResponseTypesSupported { get; set; } = null!;
        
    /// <summary>
    /// JSON array containing a list of the Subject Identifier types that this OP supports. Valid types include pairwise and public.
    /// </summary>
    [JsonProperty("subject_types_supported")]
    public List<string> SubjectTypesSupported { get; set; } = null!;
        
    /// <summary>
    /// JSON array containing a list of the JWS signing algorithms (alg values) supported by the OP for the ID Token to encode the Claims in a JWT [JWT]. The algorithm RS256 MUST be included.
    /// </summary>
    [JsonProperty("id_token_signing_alg_values_supported")]
    public List<string> IdTokenSigningAlgValuesSupported { get; set; } = null!;
        
    /// <summary>
    /// JSON array containing a list of the JWE encryption algorithms (alg values) supported by the OP for the ID Token to encode the Claims in a JWT
    /// </summary>
    [JsonProperty("id_token_encryption_alg_values_supported")]
    public List<string>? IdTokenEncryptionAlgValuesSupported { get; set; }
        
    /// <summary>
    /// JSON array containing a list of the JWE encryption algorithms (enc values) supported by the OP for the ID Token to encode the Claims in a JWT 
    /// </summary>
    [JsonProperty("id_token_encryption_enc_values_supported")]
    public List<string>? IdTokenEncryptionEncValuesSupported { get; set; }
        
    /// <summary>
    /// JSON array containing a list of the JWS signing algorithms (alg values) supported by the UserInfo Endpoint to encode the Claims in a JWT. The value none MAY be included.
    /// </summary>
    [JsonProperty("userinfo_signing_alg_values_supported")]
    public List<string>? UserinfoSigningAlgValuesSupported { get; set; }
        
    /// <summary>
    /// JSON array containing a list of the JWS signing algorithms (alg values) supported by the OP for Request Objects
    /// </summary>
    [JsonProperty("request_object_signing_alg_values_supported")]
    public List<string>? RequestObjectSigningAlgValuesSupported { get; set; }
        
    /// <summary>
    /// JSON array containing a list of the OAuth 2.0 response_mode values that this OP supports
    /// </summary>
    [JsonProperty("response_modes_supported")]
    public List<string>? ResponseModesSupported { get; set; }
      
    /// <summary>
    /// URL of the OP's Dynamic Client Registration Endpoint 
    /// </summary>
    [JsonProperty("registration_endpoint")]
    public string RegistrationEndpoint { get; set; }
        
    /// <summary>
    ///  JSON array containing a list of Client Authentication methods supported by this Token Endpoint.
    /// </summary>
    [JsonProperty("token_endpoint_auth_methods_supported")]
    public List<string>? TokenEndpointAuthMethodsSupported { get; set; }
        
    /// <summary>
    /// JSON array containing a list of the JWS signing algorithms (alg values) supported by the Token Endpoint for the signature on the JWT used to authenticate the Client at the Token Endpoint for the private_key_jwt and client_secret_jwt authentication methods. Servers SHOULD support RS256.
    /// </summary>
    [JsonProperty("token_endpoint_auth_signing_alg_values_supported")]
    public List<string> TokenEndpointAuthSigningAlgValuesSupported { get; set; }
      
    /// <summary>
    /// JSON array containing a list of the Claim Names of the Claims that the OpenID Provider MAY be able to supply values for.
    /// </summary>
    [JsonProperty("claims_supported")]
    public List<string> ClaimsSupported { get; set; }
        
    /// <summary>
    /// JSON array containing a list of the Claim Types that the OpenID Provider supports - normal, aggregated, and distributed.
    /// </summary>
    [JsonProperty("claim_types_supported")]
    public List<string>? ClaimTypesSupported { get; set; }
        
    /// <summary>
    ///  Boolean value specifying whether the OP supports use of the claims parameter, with true indicating support. If omitted, the default value is false.
    /// </summary>
    [JsonProperty("claims_parameter_supported")]
    public bool ClaimsParameterSupported { get; set; }
        
    /// <summary>
    /// JSON array containing a list of the OAuth 2.0 [RFC6749] scope values that this server supports. The server MUST support the openid scope value.
    /// </summary>
    [JsonProperty("scopes_supported")]
    public List<string>? ScopesSupported { get; set; }
        
    /// <summary>
    /// Boolean value specifying whether the OP supports use of the request parameter, with true indicating support. If omitted, the default value is false.
    /// </summary>
    [JsonProperty("request_parameter_supported")]
    public bool RequestParameterSupported { get; set; }
        
    /// <summary>
    /// Boolean value specifying whether the OP supports use of the request_uri parameter, with true indicating support. If omitted, the default value is true.
    /// </summary>
    [JsonProperty("request_uri_parameter_supported")]
    public bool RequestUriParameterSupported { get; set; }
        
    /// <summary>
    /// JSON array containing a list of Proof Key for CodeExchange (PKCE) [RFC7636] code challenge methods supported by this authorization server.
    /// </summary>
    [JsonProperty("code_challenge_methods_supported")]
    public List<string>? CodeChallengeMethodsSupported { get; set; }
        
    /// <summary>
    /// Boolean value indicating server support for mutual-TLS client certificate-bound access tokens. If omitted, the default value is false.
    /// </summary>
    [JsonProperty("tls_client_certificate_bound_access_tokens")]
    public bool TlsClientCertificateBoundAccessTokens { get; set; }
        
    /// <summary>
    /// https://www.oauth.com/oauth2-servers/token-introspection-endpoint/
    /// </summary>
    [JsonProperty("introspection_endpoint")]
    public string IntrospectionEndpoint { get; set; }
}