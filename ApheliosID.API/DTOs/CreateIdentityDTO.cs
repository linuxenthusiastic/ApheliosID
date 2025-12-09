using System.ComponentModel.DataAnnotations;

namespace ApheliosID.API.DTOs
{
    /// <summary>
    /// DTO para registrar una identidad (V SEGURA)
    /// El cliente ya generó las claves localmente
    /// </summary>
    public class CreateIdentityDto
    {
        [Required(ErrorMessage = "DID is required")]
        public string Did { get; set; } = string.Empty;
        [Required(ErrorMessage = "Public key is required")]
        public string PublicKey { get; set; } = string.Empty;
        public Dictionary<string, object> Metadata { get; set; } = new();
    }
}