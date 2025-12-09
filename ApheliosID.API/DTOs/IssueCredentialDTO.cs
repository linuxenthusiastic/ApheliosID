using System.ComponentModel.DataAnnotations;

namespace ApheliosID.API.DTOs
{
    /// <summary>
    /// DTO para emitir una credencial
    /// </summary>
    public class IssueCredentialDto
    {
        [Required(ErrorMessage = "Issuer DID is required")]
        public string IssuerDid { get; set; } = string.Empty;
        [Required(ErrorMessage = "Issuer private key is required")]
        public string IssuerPrivateKey { get; set; } = string.Empty;
        [Required(ErrorMessage = "Subject DID is required")]
        public string SubjectDid { get; set; } = string.Empty;
        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; } = string.Empty;
        [Required(ErrorMessage = "Claims are required")]
        public Dictionary<string, object> Claims { get; set; } = new();
        public DateTime? ExpiresAt { get; set; }
    }
}