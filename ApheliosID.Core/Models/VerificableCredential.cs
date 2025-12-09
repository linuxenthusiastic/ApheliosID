namespace ApheliosID.Core.Models;

public abstract class VerifiableCredential
{
    public string Id { get; protected set; }
    public string Issuer { get; protected set; }
    public string Subject { get; protected set; }
    public DateTime IssuedAt { get; protected set; }
    public DateTime? ExpiresAt { get; protected set; }
    public bool IsRevoked { get; protected set; }
    public DateTime? RevokedAt { get; protected set; }

    protected VerifiableCredential(string issuer, string subject, DateTime? expiresAt = null)
    {
        Id = Guid.NewGuid().ToString();
        Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
        Subject = subject ?? throw new ArgumentNullException(nameof(subject));
        IssuedAt = DateTime.UtcNow;
        ExpiresAt = expiresAt;
        IsRevoked = false;
    }
    public abstract string GetCredentialType();
    public abstract bool ValidateSpecificClaims();

    public virtual bool IsValid()
    {
        if (IsRevoked) return false;
        if (ExpiresAt.HasValue && DateTime.UtcNow > ExpiresAt.Value) return false;
        
        return ValidateSpecificClaims();
    }

    public void Revoke()
    {
        IsRevoked = true;
        RevokedAt = DateTime.UtcNow;
    }

    public virtual Dictionary<string, object> GetCredentialInfo()
    {
        return new Dictionary<string, object>
        {
            { "id", Id },
            { "type", GetCredentialType() },
            { "issuer", Issuer },
            { "subject", Subject },
            { "issuedAt", IssuedAt },
            { "expiresAt", ExpiresAt ?? (object)"never" },
            { "isRevoked", IsRevoked },
            { "isValid", IsValid() }
        };
    }
}