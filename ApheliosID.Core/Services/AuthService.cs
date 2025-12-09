using ApheliosID.Core.Models;
using System.Security.Cryptography;
using System.Text;

namespace ApheliosID.Core.Services;

/// <summary>
/// Servicio para autenticación challenge-response
/// </summary>
public class AuthService
{
    private readonly CryptoService _cryptoService;
    private readonly IdentityService _identityService;
    private readonly Dictionary<string, AuthChallenge> _challenges;

    public AuthService(CryptoService cryptoService, IdentityService identityService)
    {
        _cryptoService = cryptoService;
        _identityService = identityService;
        _challenges = new Dictionary<string, AuthChallenge>();
    }

    public AuthChallenge GenerateChallenge(string did)
    {
        // Verificar que la identidad existe
        var identity = _identityService.GetIdentity(did);
        if (identity == null)
            throw new InvalidOperationException($"Identity {did} not found");

        // Generar challenge aleatorio
        var challengeBytes = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(challengeBytes);
        }
        var challengeString = Convert.ToBase64String(challengeBytes);

        // Crear y guardar challenge
        var authChallenge = new AuthChallenge(did, challengeString, validMinutes: 5);
        _challenges[challengeString] = authChallenge;

        Console.WriteLine($"Challenge generated for {did}");

        return authChallenge;
    }

    public bool VerifyChallenge(string did, string challenge, string signature)
    {
        // Verificar que el challenge existe
        if (!_challenges.ContainsKey(challenge))
        {
            Console.WriteLine($"Challenge not found");
            return false;
        }

        var authChallenge = _challenges[challenge];

        // Verificar que el challenge es para este DID
        if (authChallenge.getDid() != did)
        {
            Console.WriteLine($"Challenge DID mismatch");
            return false;
        }

        // Verificar que no expiró
        if (authChallenge.IsExpired())
        {
            Console.WriteLine($"Challenge expired");
            return false;
        }

        // Verificar que no fue usado
        if (authChallenge.getIsUsed())
        {
            Console.WriteLine($"Challenge already used");
            return false;
        }

        // Obtener identidad
        var identity = _identityService.GetIdentity(did);
        if (identity == null)
        {
            Console.WriteLine($"Identity not found");
            return false;
        }

        // Verificar firma
        bool isValidSignature = _cryptoService.Verify(
            challenge,
            signature,
            identity.getPublicKey()
        );

        if (!isValidSignature)
        {
            Console.WriteLine($"Invalid signature");
            return false;
        }

        authChallenge.MarkAsUsed();

        Console.WriteLine($"Challenge verified for {did}");

        return true;
    }

    public void CleanExpiredChallenges()
    {
        var expiredKeys = _challenges
            .Where(kvp => kvp.Value.IsExpired() || kvp.Value.getIsUsed())
            .Select(kvp => kvp.Key)
            .ToList();

        foreach (var key in expiredKeys)
        {
            _challenges.Remove(key);
        }

        Console.WriteLine($"Cleaned {expiredKeys.Count} expired/used challenges");
    }
}