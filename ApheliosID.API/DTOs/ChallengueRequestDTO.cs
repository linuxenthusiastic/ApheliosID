using System.ComponentModel.DataAnnotations;

namespace ApheliosID.API.DTOs;

public class ChallengeRequestDto
{
    [Required]
    public string Did { get; set; } = string.Empty;
}

public class ChallengeResponseDto
{
    public string Challenge { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public string Message { get; set; } = string.Empty;
}

public class VerifyRequestDto
{
    [Required]
    public string Did { get; set; } = string.Empty;

    [Required]
    public string Challenge { get; set; } = string.Empty;

    [Required]
    public string Signature { get; set; } = string.Empty;
}

public class TokenResponseDto
{
    public string Token { get; set; } = string.Empty;
    public string TokenType { get; set; } = "Bearer";
    public int ExpiresIn { get; set; }
    public string Did { get; set; } = string.Empty;
}