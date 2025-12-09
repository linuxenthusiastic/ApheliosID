using System.ComponentModel.DataAnnotations;

namespace ApheliosID.API.DTOs
{
    /// <summary>
    /// DTO para revocar una credencial
    /// </summary>
    public class RevokeCredentialDto
    {
        [Required(ErrorMessage = "Issuer DID is required")]
        public string IssuerDid { get; set; } = string.Empty;
        [Required(ErrorMessage = "Issuer private key is required")]
        public string IssuerPrivateKey { get; set; } = string.Empty;
    }
}