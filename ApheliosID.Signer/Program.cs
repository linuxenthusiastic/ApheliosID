using System;
using System.Security.Cryptography;
using System.Text;

namespace ApheliosID.Signer;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("╔════════════════════════════════════════════════╗");
        Console.WriteLine("║      APHELIOSID - CHALLENGE SIGNER TOOL        ║");
        Console.WriteLine("╚════════════════════════════════════════════════╝");
        Console.WriteLine();

        // Solicitar challenge
        Console.Write("Enter challenge: ");
        string? challenge = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(challenge))
        {
            Console.WriteLine("Challenge cannot be empty");
            return;
        }

        // Solicitar private key
        Console.WriteLine();
        Console.WriteLine("Enter your private key (Base64 format):");
        Console.WriteLine("   (Paste ONLY the Base64 content, no BEGIN/END lines)");
        Console.WriteLine();
        
        string? privateKeyBase64 = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(privateKeyBase64))
        {
            Console.WriteLine("❌ Private key cannot be empty");
            return;
        }

        try
        {
            Console.WriteLine();
            Console.WriteLine("Signing challenge...");
            
            // Intentar firmar con múltiples métodos
            string signature = SignChallenge(challenge, privateKeyBase64);

            Console.WriteLine();
            Console.WriteLine("SUCCESS!");
            Console.WriteLine();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║                   SIGNATURE                     ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine(signature);
            Console.WriteLine();
            Console.WriteLine("Copy this signature and use it in /api/auth/verify");
            Console.WriteLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine();
            Console.WriteLine($"❌ ERROR: {ex.Message}");
            Console.WriteLine();
            Console.WriteLine("Debug info:");
            Console.WriteLine($"  Challenge length: {challenge.Length}");
            Console.WriteLine($"  Key length: {privateKeyBase64.Length}");
            Console.WriteLine($"  Exception type: {ex.GetType().Name}");
        }
    }

static string SignChallenge(string challenge, string privateKeyBase64)
{
    // Limpiar el Base64
    string cleanKey = privateKeyBase64
        .Replace("\n", "")
        .Replace("\r", "")
        .Replace(" ", "")
        .Replace("\t", "")
        .Trim();

    Console.WriteLine($"  Cleaned key length: {cleanKey.Length} chars");

    byte[] privateKeyBytes = Convert.FromBase64String(cleanKey);
    Console.WriteLine($"  Key bytes: {privateKeyBytes.Length} bytes");

    using var rsa = RSA.Create();
    
    try
    {
        rsa.ImportRSAPrivateKey(privateKeyBytes, out _);
        Console.WriteLine($"  Imported as RSA Private Key");
    }
    catch (Exception ex)
    {
        try
        {
            rsa.ImportPkcs8PrivateKey(privateKeyBytes, out _);
            Console.WriteLine($"  Imported as PKCS8");
        }
        catch
        {
            throw new Exception(
                $"Could not import key in any format. Original error: {ex.Message}"
            );
        }
    }

    byte[] challengeBytes = Encoding.UTF8.GetBytes(challenge);
    byte[] signatureBytes = rsa.SignData(
        challengeBytes,
        HashAlgorithmName.SHA256,
        RSASignaturePadding.Pkcs1
    );
    
    Console.WriteLine($"Signed with SHA256 + PKCS1");

    return Convert.ToBase64String(signatureBytes);
}
}