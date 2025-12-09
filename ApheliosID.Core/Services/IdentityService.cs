using ApheliosID.Core.Models;

namespace ApheliosID.Core.Services;

public class IdentityService 
{
    private readonly CryptoService _cryptoService;
    private readonly Dictionary<string, Identity> _identities;

    public IdentityService(CryptoService cryptoService)
        {
            _cryptoService = cryptoService ?? throw new ArgumentNullException(nameof(cryptoService));
            _identities = new Dictionary<string, Identity>();
        }

public Identity CreateIdentity(
    string did,
    string publicKey,
    Dictionary<string, object>? metadata = null)
{
    string expectedDid = _cryptoService.GenerateDid(publicKey);
    if (did != expectedDid)
        throw new ArgumentException($"DID does not match public key. Expected: {expectedDid}");

    if (_identities.ContainsKey(did))
        throw new InvalidOperationException($"Identity with DID {did} already exists");

    var identity = new Identity(
        did,
        publicKey,
        metadata
    );

    _identities[did] = identity;

    Console.WriteLine($"Identity registered: {did}");

    return identity;
}

    public Identity CreateIdentityWithKeys(Dictionary<string, object>? metadata = null)
        {
            var (publicKey, privateKey) = _cryptoService.GenerateKeyPair();
            return CreateIdentity(publicKey, privateKey, metadata);
        }

    public Identity? GetIdentity(string did)
        {
            return _identities.ContainsKey(did) ? _identities[did] : null;
        }

    public bool IdentityExists(string did)
        {
            return _identities.ContainsKey(did);
        }


    public void DeactivateIdentity(string did)
        {
            var identity = GetIdentity(did);
            if (identity == null)
                throw new InvalidOperationException($"Identity {did} not found");

            identity.Deactivate();
            Console.WriteLine($"Identity {did} deactivated");
        }

    public List<Identity> GetAllIdentities()
        {
            return _identities.Values.ToList();
        }

    public List<Identity> GetActiveIdentities()
        {
            return _identities.Values
                .Where(i => i.getIsActive())
                .ToList();
        }
}