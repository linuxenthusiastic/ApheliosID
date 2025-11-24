using System.ComponentModel.DataAnnotations;

namespace ApheliosID.API.DTOs
{
    public class CreateTransactionDto
    {
        [Required(ErrorMessage = "From address is required")]
        [MinLength(3, ErrorMessage = "From address must be at least 3 characters")]
        public string From { get; set; } = string.Empty;

        [Required(ErrorMessage = "To address is required")]
        [MinLength(3, ErrorMessage = "To address must be at least 3 characters")]
        public string To { get; set; } = string.Empty;

        [Required(ErrorMessage = "Data is required")]
        public object Data { get; set; } = new object();
    }
}