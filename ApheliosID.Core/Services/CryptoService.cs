using System.Security.Cryptography;
using System.Text;

namespace ApheliosID.Core.Service;
    /// <summary>
    /// Servicio para operaciones criptográficas
    /// Responsabilidad: RSA, firmas digitales, hashing, DIDs
    /// </summary>
public class CryptoService 
{

    //generares pares de claves rsa 2048 bites
    public (string publicKey, string privateKey) GenerateKeyPair()
    {
        using(var rsa = RSA.Create(2048))
        {
            // Clave public como bytes
                var publicKeyBytes = rsa.ExportRSAPublicKey();
            // Clave privada como bytes
                var privateKeyBytes = rsa.ExportRSAPrivateKey();
            // Clave publica a base64
                var publicKeyBase64 = Convert.ToBase64String(publicKeyBytes);
            // Clave privada a base64
                var privateKeyBase64 = Convert.ToBase64String(privateKeyBytes);
                return (publicKeyBase64, privateKeyBase64)
        }
    }


public bool Verify(string message,string messaBase64,string publicKeyBase64)
{
    try
    {
        //mensaje a bytes
                var messageBytes = Encoding.UTF8.GetBytes(message);
        //firma a byes
                var signatureBytes = Convert.FromBase64String(signatureBase64);
        //publickey a bytes
                var publicKeyBytes = Convert.FromBase64String(publicKeyBase64);

                using (var rsa = RSA.Create())
                {
                    rsa.ImportRSAPublicKey(publicKeyBytes, out _);
                    
                    //rsa checker
                    return rsa.VerifyData(
                        messageBytes,
                        signatureBytes,
                        HashAlgorithmName.SHA256,
                        RSASignaturePadding.Pkcs1
                    );
                }

                catch
                {
                    return false;
                }
    }
}

public string GenerateDid(string publickey)
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