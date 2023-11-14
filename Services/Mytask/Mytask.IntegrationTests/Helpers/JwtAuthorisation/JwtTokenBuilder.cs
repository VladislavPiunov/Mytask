using System.Security.Claims;
using System.Security.Cryptography;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using EncryptionException = Jose.EncryptionException;

namespace Mytask.IntegrationTests.Helpers.JwtAuthorisation;

/// <summary>
/// Билдер для создания jwt
/// </summary>
internal class JwtTokenBuilder
{
    /// <summary>
    /// Список клеймов, которые будут добавлены в токен
    /// </summary>
    private readonly List<Claim> _claims = new();

    /// <summary>
    /// Приватный rsa ключ - используется для подписи токена
    /// </summary>
    private static string PrivateKey => 
        """
        -----BEGIN RSA PRIVATE KEY-----
        MIIEogIBAAKCAQEAjWrUiboTNuiejJM9jY0K/oCiuhoiZje18VQH7KlPWnihA7S8SvpVclr5nxuT75Rg2iBpR6RmfDfubkuMDOXlT86PRcgMAkDAFfoZ8OGlhKGLGgVSf+Tduv8fJ7lN28OgEzEFgE8G+B84FwOeoUV7VxMk39MxAsN6ajXM+IU+gLohj4c5UKrWHjor3tdqrYSjIZmK7+tcCR+/0U9GXPu+mHYKEi9B5WmyO+EoQ31OUjghFwCXJYMsvSiLIoDbX/P9I6NUkZkJRPYcL96ixLpRHKC5hGL61tEOFBlvFu1mDvkX6swU5EfD1sxlME7Ytnzw98S0yPzYcaYXI/WGWb2QsQIDAQABAoIBAF6JUwm7FYs4WH07FQPijL3z+lSUkfhpN7zbYuzHhl/Bkknq8ZDh5msq/AJsKioXs+M9lYOqGETkEwUyha49pV0Dhe2tPLHo3UAT0HGiNscCQv4jHrKWqc+PKyGgE7ddAE60D6xlqBAItrNT3SCMVVaxWo4yHWpuiRAlZR+h21WrmbKeem4ZfiUUPlzCSrkEOAiz9GMrFt58oFPyg2cnX2B8DcZRMhe3nYCaEdojJFH5S+1sGvYE5aTyqXnQvRlNUV+tup5RIOMe411j4xG6goRDF7w9KuKsk4LgslnCs4Awyxlby8jmLuvNWaXA/q6VgR3x4kofdeMtmZN4wezcZK0CgYEAw80DgGoX0SRIPc48OEKD/YdRZ/Kwq5KYl/HrC0CxzlPkaD0vzU0WAE+b+/dDFYMskNtu3la8PwwZJtuJthQqF4eDNIxAALuEz13756Fn3XinX5ZkPLa0j8YJur8z/HTJe9pDvsLpqHYOWp3/Ig3Z+thOMuD+giiCVdxXGSlYFH8CgYEAuOVtV5tOtEb8hu5R4+Hp5JIAfd53qepHzFKUwF/Tp97UOH42sIkxzX2B6UqpVcdx/yhHxd9g75vw0rHlbeqGWbjDR4nqcTTE1rDCNeDbci5YX+9Dfna8OWd1++KYa3kHi+C3tQPsak8ckMAMgGUHWcoEQJ0FEwUb/Z92/OakAs8CgYBUYHrL0exlki8Xg1JsJC3hCXlJREpiBZCAmh3iAYUeFwTs7sE0xa1fgO8FS+66zIZd/lHuuo3w1XPZTO4xassgzKL7+Bx0tFptSmEN1n598EqgZJzZlRqGgp8avN7YQjO5jbt372Ll18ojvsZ9lF6FPMWmI1NKH87a1VMrYqe0XQKBgA0zXB4gGXtnggoEI9aYP4GxJtXVt0drUZr13mbpsIvQregmorLx6JtaNZc5XGOibLIh5xXqf9o7kPMJ/m5diyAGv/Jwl0tj0BXf4s3D8wbw5iBbTb9OrNuQVm0YXXd22aIT9im3UP66DTkMbRgRnne7o5gVXdJg0AHIi888jEMjAoGAStEkWhJONuHawMFSqJM+dcMVDSyx6wRDjleeZTzMqXBbDH0I1AT6v+zMsCeAsirA8LTa0kfvMT+E6HqRO9daaH/hhw1Qtxwdgh6EGus3oMz4Yk9Kvl/yIQWLtonEWMO76ZCFe4x40ZCodLp/CZuvH+ybhDRYHv60L63tP6XYf+M=
        -----END RSA PRIVATE KEY-----
        """;
    
    /// <summary>
    /// Создать токен
    /// </summary>
    public string CreateToken()
    { 
        RSAParameters rsaParams;
        using (var pk = new StringReader(PrivateKey))
        {
            var pemReader = new PemReader(pk);
            var keyPair = pemReader.ReadObject() as AsymmetricCipherKeyPair;
            if (keyPair == null)
            {
                throw new EncryptionException("Could not read RSA private key");
            } 
            var privateRsaParams = keyPair.Private as RsaPrivateCrtKeyParameters;
            rsaParams = DotNetUtilities.ToRSAParameters(privateRsaParams);
        }

        using var rsa = new RSACryptoServiceProvider();
        rsa.ImportParameters(rsaParams);
            
        var payload = _claims.ToDictionary(k => k.Type, v => (object)v.Value);
        return Jose.JWT.Encode(payload, rsa, Jose.JwsAlgorithm.RS256);
    }
    
    /// <summary>
    /// Добавить стандартные клеймы
    /// </summary>
    public JwtTokenBuilder WithDefaultClaims()
    {
        _claims.Add(new Claim("exp", "1883314464"));
        _claims.Add(new Claim("iat", "1883314404"));
        _claims.Add(new Claim("jti", "1a35839e-bb3e-4010-a9ca-7fd96f73037c"));
        _claims.Add(new Claim("aud", "account"));
        _claims.Add(new Claim("typ", "Bearer"));
        _claims.Add(new Claim("azp", "localhost"));
        _claims.Add(new Claim("session_state", "657f0fdc-d21e-4d4e-ada4-0eb91acf9d9c"));
        _claims.Add(new Claim("acr", "1"));
        _claims.Add(new Claim("scope", "profile email"));
        _claims.Add(new Claim("sid", "657f0fdc-d21e-4d4e-ada4-0eb91acf9d9c"));
        return this;
    }
    
    /// <summary>
    /// Добавить клейм iss
    /// </summary>
    public JwtTokenBuilder WithIssuer(string issuer)
    {
        _claims.Add(new Claim("iss", $"http://localhost:8484/auth/realms/{issuer}"));
        return this;
    }
    
    /// <summary>
    /// Добавить клеймы
    /// </summary>
    public JwtTokenBuilder WithClaims(Dictionary<string, string> withClaims)
    {
        foreach (var claim in withClaims)
        {
            _claims.Add(new Claim(claim.Key, claim.Value));
        }

        return this;
    }
}