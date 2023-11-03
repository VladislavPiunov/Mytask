using Newtonsoft.Json;

namespace Mytask.IntegrationTests.Helpers.JwtAuthorisation.Models;

/// <summary>
/// Подробнее смотри https://www.rfc-editor.org/rfc/rfc7517#page-10
/// </summary>
public class Certificates
{
    /// <summary>
    /// Array of JWK values
    /// </summary>
    [JsonProperty("keys")]
    public List<Key> Keys { get; set; }
}

/// <summary>
/// JWK value
/// </summary>
public class Key
{
    /// <summary>
    /// Key ID
    /// </summary>
    [JsonProperty("kid")]
    public string Kid { get; set; }

    /// <summary>
    /// Key Type
    /// </summary>
    [JsonProperty("kty")]
    public string Kty { get; set; }

    /// <summary>
    /// Algorithm
    /// </summary>
    [JsonProperty("alg")]
    public string Alg { get; set; }

    /// <summary>
    /// Public Key Use
    /// </summary>
    [JsonProperty("use")]
    public string Use { get; set; }

    /// <summary>
    /// RSA key values - modulus
    /// </summary>
    [JsonProperty("n")]
    public string N { get; set; }

    /// <summary>
    /// RSA key values - publicExponent
    /// </summary>
    [JsonProperty("e")]
    public string E { get; set; }

    /// <summary>
    /// X.509 Certificate Chain
    /// </summary>
    [JsonProperty("x5c")]
    public string[] X5C { get; set; }

    /// <summary>
    /// X.509 Certificate SHA-1 Thumbprint
    /// </summary>
    [JsonProperty("x5t")]
    public string X5T { get; set; }

    /// <summary>
    /// X.509 Certificate SHA-256 Thumbprint
    /// </summary>
    [JsonProperty("x5t#S256")]
    public string Hash { get; set; }
}