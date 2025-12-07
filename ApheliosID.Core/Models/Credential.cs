using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApheliosID.Core.Models
{
    /// <summary>
    /// Representa una credencial verificable
    /// </summary>
    public class Credential
    {
        private string Id { get; }
        private string Issuer { get; }
        private string Subject { get; }
        private string Type { get; }
        private Dictionary<string, object> Claims { get; }
        private DateTime IssuedAt { get; }
        private DateTime? ExpiresAt { get; }
        private string Signature { get; }
        private bool IsRevoked { get; set; }
        private DateTime? RevokedAt { get; set; }

        public Credential(
            string id,
            string issuer,
            string subject,
            string type,
            Dictionary<string, object> claims,
            string signature,
            DateTime? expiresAt = null)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
            Subject = subject ?? throw new ArgumentNullException(nameof(subject));
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Claims = claims ?? throw new ArgumentNullException(nameof(claims));
            Signature = signature ?? throw new ArgumentNullException(nameof(signature));
            IssuedAt = DateTime.UtcNow;
            ExpiresAt = expiresAt;
            IsRevoked = false;
            RevokedAt = null;
        }

        public Credential(
            string id,
            string issuer,
            string subject,
            string type,
            Dictionary<string, object> claims,
            DateTime issuedAt,
            DateTime? expiresAt,
            string signature,
            bool isRevoked,
            DateTime? revokedAt)
        {
            Id = id;
            Issuer = issuer;
            Subject = subject;
            Type = type;
            Claims = claims;
            IssuedAt = issuedAt;
            ExpiresAt = expiresAt;
            Signature = signature;
            IsRevoked = isRevoked;
            RevokedAt = revokedAt;
        }

        public string GetSignatureData()
        {
            return $"{Id}{Issuer}{Subject}{Type}{JsonSerializer.Serialize(Claims)}{IssuedAt:O}";
        }

        public void Revoke()
        {
            IsRevoked = true;
            RevokedAt = DateTime.UtcNow;
        }

        public bool IsExpired()
        {
            if (ExpiresAt == null)
                return false;

            return DateTime.UtcNow > ExpiresAt.Value;
        }

        public bool IsActive()
        {
            return !IsRevoked && !IsExpired();
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(new
            {
                Id,
                Issuer,
                Subject,
                Type,
                Claims,
                IssuedAt,
                ExpiresAt,
                Signature = Signature[..Math.Min(32, Signature.Length)] + "...",
                IsRevoked,
                RevokedAt,
                IsActive = IsActive()
            }, new JsonSerializerOptions { WriteIndented = true });
        }

        // Getters públicos
        public string getId() => Id;
        public string getIssuer() => Issuer;
        public string getSubject() => Subject;
        public string getType() => Type;
        public Dictionary<string, object> getClaims() => new Dictionary<string, object>(Claims);
        public DateTime getIssuedAt() => IssuedAt;
        public DateTime? getExpiresAt() => ExpiresAt;
        public string getSignature() => Signature;
        public bool getIsRevoked() => IsRevoked;
        public DateTime? getRevokedAt() => RevokedAt;
    }
}