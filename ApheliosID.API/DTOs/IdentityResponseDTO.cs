namespace ApheliosID.API.DTOs
{
    /// <summary>
    /// DTO de respuesta para una identidad
    /// </summary>
    public class IdentityResponseDto
    {
        public string Did { get; set; } = string.Empty;
        public string PublicKey { get; set; } = string.Empty;
        public string? PrivateKey { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public Dictionary<string, object> Metadata { get; set; } = new();
    }
}