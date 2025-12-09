namespace ApheliosID.API.DTOs
{
    /// <summary>
    /// DTO de respuesta para una credencial
    /// </summary>
    public class CredentialResponseDto
    {
        public string Id { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public Dictionary<string, object> Claims { get; set; } = new();
        public DateTime IssuedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public string Signature { get; set; } = string.Empty;
        public bool IsRevoked { get; set; }
        public DateTime? RevokedAt { get; set; }
        public bool IsActive { get; set; }
    }
}