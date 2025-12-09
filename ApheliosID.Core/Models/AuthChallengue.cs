namespace ApheliosID.Core.Models;

/// <summary>
/// Representa un desafio de auth
/// </summary>
public class AuthChallenge
{
    private string Did { get; }
    private string Challenge { get; }
    private DateTime CreatedAt { get; }
    private DateTime ExpiresAt { get; }
    private bool IsUsed { get; set; }

    public AuthChallenge(string did, string challenge, int validMinutes = 5)
    {
        Did = did;
        Challenge = challenge;
        CreatedAt = DateTime.UtcNow;
        ExpiresAt = CreatedAt.AddMinutes(validMinutes);
        IsUsed = false;
    }

    public bool IsExpired() => DateTime.UtcNow > ExpiresAt;
    
    public void MarkAsUsed() => IsUsed = true;

    // Getters
    public string getDid() => Did;
    public string getChallenge() => Challenge;
    public DateTime getCreatedAt() => CreatedAt;
    public DateTime getExpiresAt() => ExpiresAt;
    public bool getIsUsed() => IsUsed;
}