using ApheliosID.Core.Models;

namespace ApheliosID.Core.Services
{
    /// <summary>
    /// Servicio para gestionar credenciales verificables
    /// </summary>
    public class CredentialService
    {
        private readonly CryptoService _cryptoService;
        private readonly IdentityService _identityService;
        private readonly BlockchainService _blockchainService;
        private readonly Dictionary<string, Credential> _credentials;

        public CredentialService(
            CryptoService cryptoService,
            IdentityService identityService,
            BlockchainService blockchainService)
        {
            _cryptoService = cryptoService ?? throw new ArgumentNullException(nameof(cryptoService));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _blockchainService = blockchainService ?? throw new ArgumentNullException(nameof(blockchainService));
            _credentials = new Dictionary<string, Credential>();
        }

        public Credential IssueCredential(
            string issuerDid,
            string issuerPrivateKey,
            string subjectDid,
            string type,
            Dictionary<string, object> claims,
            DateTime? expiresAt = null)
        {
            var issuer = _identityService.GetIdentity(issuerDid);
            if (issuer == null)
                throw new InvalidOperationException($"Issuer identity {issuerDid} not found");

            var subject = _identityService.GetIdentity(subjectDid);
            if (subject == null)
                throw new InvalidOperationException($"Subject identity {subjectDid} not found");

            string credentialId = $"cred-{Guid.NewGuid().ToString()[..16]}";

            var tempCredential = new Credential(
                credentialId,
                issuerDid,
                subjectDid,
                type,
                claims,
                "",
                expiresAt
            );

            string signatureData = tempCredential.GetSignatureData();
            string signature = _cryptoService.Sign(signatureData, issuerPrivateKey);

            var credential = new Credential(
                credentialId,
                issuerDid,
                subjectDid,
                type,
                claims,
                signature,
                expiresAt
            );

            _credentials[credentialId] = credential;

            var transaction = new Transaction(
                issuerDid,
                subjectDid,
                new
                {
                    type = "credential-issued",
                    credentialId,
                    credentialType = type,
                    claims,
                    signature,
                    issuedAt = credential.getIssuedAt(),
                    expiresAt = credential.getExpiresAt()
                }
            );

            _blockchainService.AddTransaction(transaction);

            Console.WriteLine($"✅ Credential {credentialId} issued by {issuerDid} to {subjectDid}");

            return credential;
        }

        public bool VerifyCredential(string credentialId)
        {
            var credential = GetCredential(credentialId);
            if (credential == null)
            {
                Console.WriteLine($"❌ Credential {credentialId} not found");
                return false;
            }

            if (credential.getIsRevoked())
            {
                Console.WriteLine($"❌ Credential {credentialId} is revoked");
                return false;
            }

            if (credential.IsExpired())
            {
                Console.WriteLine($"❌ Credential {credentialId} is expired");
                return false;
            }
            var issuer = _identityService.GetIdentity(credential.getIssuer());
            if (issuer == null)
            {
                Console.WriteLine($"❌ Issuer {credential.getIssuer()} not found");
                return false;
            }

            string signatureData = credential.GetSignatureData();
            bool isValidSignature = _cryptoService.Verify(
                signatureData,
                credential.getSignature(),
                issuer.getPublicKey()
            );

            if (!isValidSignature)
            {
                Console.WriteLine($"❌ Credential {credentialId} has invalid signature");
                return false;
            }

            Console.WriteLine($"✅ Credential {credentialId} is VALID");
            return true;
        }

        public void RevokeCredential(string credentialId, string issuerDid, string issuerPrivateKey)
        {
            var credential = GetCredential(credentialId);
            if (credential == null)
                throw new InvalidOperationException($"Credential {credentialId} not found");

            if (credential.getIssuer() != issuerDid)
                throw new UnauthorizedAccessException("Only the issuer can revoke a credential");

            var testData = "revocation-test";
            var testSignature = _cryptoService.Sign(testData, issuerPrivateKey);
            var issuer = _identityService.GetIdentity(issuerDid);
            
            if (!_cryptoService.Verify(testData, testSignature, issuer.getPublicKey()))
                throw new UnauthorizedAccessException("Invalid issuer private key");

            credential.Revoke();

            var transaction = new Transaction(
                issuerDid,
                credential.getSubject(),
                new
                {
                    type = "credential-revoked",
                    credentialId,
                    revokedAt = credential.getRevokedAt()
                }
            );

            _blockchainService.AddTransaction(transaction);

            Console.WriteLine($"🚫 Credential {credentialId} revoked by {issuerDid}");
        }

        public Credential? GetCredential(string credentialId)
        {
            return _credentials.ContainsKey(credentialId) ? _credentials[credentialId] : null;
        }

        public List<Credential> GetCredentialsBySubject(string subjectDid)
        {
            return _credentials.Values
                .Where(c => c.getSubject() == subjectDid)
                .ToList();
        }
        public List<Credential> GetCredentialsByIssuer(string issuerDid)
        {
            return _credentials.Values
                .Where(c => c.getIssuer() == issuerDid)
                .ToList();
        }

        public List<Credential> GetAllCredentials()
        {
            return _credentials.Values.ToList();
        }
    }
}