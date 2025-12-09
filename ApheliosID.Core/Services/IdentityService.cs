using ApheliosID.Core.Models;

namespace ApheliosID.Core.Services;

public class IdentityService 
{
    private readonly CryptoService _cryptoService;
    private readonly Dictionary<string, Identity> _identities;
    private readonly BlockchainService? _blockchainService;  // ← AGREGAR
    
    public IdentityService(CryptoService cryptoService,
    BlockchainService? blockchainService = null)
    {
        _cryptoService = cryptoService ?? throw new ArgumentNullException(nameof(cryptoService));
        _identities = new Dictionary<string, Identity>();
        _blockchainService = blockchainService;
    }

    /// <summary>
    /// Registra una identidad (VER SEGURA - sin clave privada)
    /// El cliente ya generó las claves localmente
    /// </summary>
public Identity CreateIdentity(
    string did,
    string publicKey,
    Dictionary<string, object>? metadata = null)
{
    // Validar DID
    string expectedDid = _cryptoService.GenerateDid(publicKey);
    if (did != expectedDid)
        throw new ArgumentException($"DID does not match public key. Expected: {expectedDid}");

    // Verificar que no exista
    if (_identities.ContainsKey(did))
        throw new InvalidOperationException($"Identity with DID {did} already exists");

    // Crear identidad
    var identity = new Identity(did, publicKey, metadata);

    // Guardar en Dictionary
    _identities[did] = identity;

    //Registrar en blockchain
    if (_blockchainService != null)
    {
        var transaction = new Transaction(
            "did:aphelios:system",  // Sistema crea identidades
            did,
            new
            {
                type = "identity-created",
                did,
                publicKey,
                metadata,
                createdAt = identity.getCreatedAt()
            }
        );

        _blockchainService.AddTransaction(transaction);
        Console.WriteLine($"Identity registered in blockchain: {did}");
    }

    Console.WriteLine($"Identity created: {did}");

    return identity;
}

    /// <summary>
    /// Crea una identidad generando las claves automáticamente
    /// SOLO PARA HERRAMIENTAS CLI LOCALES
    /// </summary>
    public Identity CreateIdentityWithKeys(Dictionary<string, object>? metadata = null)
    {
        var (publicKey, privateKey) = _cryptoService.GenerateKeyPair();

        var did = _cryptoService.GenerateDid(publicKey);

        if (_identities.ContainsKey(did))
            throw new InvalidOperationException($"Identity with DID {did} already exists");

        var identityWithPrivateKey = new Identity(did, publicKey, privateKey, metadata);

        var storedIdentity = new Identity(did, publicKey, metadata);
        _identities[did] = storedIdentity;

        Console.WriteLine($"✅ Identity created with keys: {did}");

        return identityWithPrivateKey;
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

    public void ActivateIdentity(string did)
    {
        var identity = GetIdentity(did);
        if (identity == null)
            throw new InvalidOperationException($"Identity {did} not found");

        identity.Activate();
        Console.WriteLine($"Identity {did} activated");
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

    public int GetIdentityCount()
    {
        return _identities.Count;
    }
}