using System.Security.Cryptography;
using System.Text;

namespace ApheliosID.Core.Services;

/// <summary>
/// Servicio para operaciones criptográficas
/// Responsabilidad: RSA, firmas digitales, hashing, DIDs
/// </summary>
public class CryptoService 
{

    public (string publicKey, string privateKey) GenerateKeyPair()
    {
        using (var rsa = RSA.Create(2048))
        {
            var publicKeyBytes = rsa.ExportRSAPublicKey();
            
            var privateKeyBytes = rsa.ExportRSAPrivateKey();
            
            var publicKeyBase64 = Convert.ToBase64String(publicKeyBytes);
            var privateKeyBase64 = Convert.ToBase64String(privateKeyBytes);
            
            return (publicKeyBase64, privateKeyBase64);
        }
    }

    public string Sign(string message, string privateKeyBase64)
    {
        var messageBytes = Encoding.UTF8.GetBytes(message);
        
        var privateKeyBytes = Convert.FromBase64String(privateKeyBase64);
        
        using (var rsa = RSA.Create())
        {
            rsa.ImportRSAPrivateKey(privateKeyBytes, out _);
            
            var signatureBytes = rsa.SignData(
                messageBytes,
                HashAlgorithmName.SHA256,
                RSASignaturePadding.Pkcs1
            );
            
            return Convert.ToBase64String(signatureBytes);
        }
    }

    public bool Verify(string message, string signatureBase64, string publicKeyBase64)
    {
        try
        {
            var messageBytes = Encoding.UTF8.GetBytes(message);
            
            var signatureBytes = Convert.FromBase64String(signatureBase64);
            
            var publicKeyBytes = Convert.FromBase64String(publicKeyBase64);
            
            using (var rsa = RSA.Create())
            {
                rsa.ImportRSAPublicKey(publicKeyBytes, out _);
                
                return rsa.VerifyData(
                    messageBytes,
                    signatureBytes,
                    HashAlgorithmName.SHA256,
                    RSASignaturePadding.Pkcs1
                );
            }
        }
        catch
        {
            return false;
        }
    }

    public string GenerateDid(string publicKey)
    {
        var publicKeyBytes = Encoding.UTF8.GetBytes(publicKey);
        
        using (var sha256 = SHA256.Create())
        {
            var hashBytes = sha256.ComputeHash(publicKeyBytes);
            
            var hashBase64 = Convert.ToBase64String(hashBytes);
            
            var cleanHash = hashBase64
                .Replace("+", "")
                .Replace("/", "")
                .Replace("=", "");
            
            var didSuffix = cleanHash.Substring(0, Math.Min(16, cleanHash.Length));
            
            return $"did:aphelios:{didSuffix}";
        }
    }
}