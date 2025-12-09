using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApheliosID.Core.Services;
using ApheliosID.API.DTOs;

namespace ApheliosID.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthController> _logger;

    public AuthController(
        AuthService authService,
        IConfiguration configuration,
        ILogger<AuthController> logger)
    {
        _authService = authService;
        _configuration = configuration;
        _logger = logger;
    }

    /// <summary>
    /// Solicita un challenge para auth
    /// </summary>
    [HttpPost("challenge")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ChallengeResponseDto> RequestChallenge([FromBody] ChallengeRequestDto dto)
    {
        _logger.LogInformation("Challenge requested for {DID}", dto.Did);

        try
        {
            var challenge = _authService.GenerateChallenge(dto.Did);

            return Ok(new ChallengeResponseDto
            {
                Challenge = challenge.getChallenge(),
                ExpiresAt = challenge.getExpiresAt(),
                Message = "Sign this challenge with your private key and send it to /api/auth/verify"
            });
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPost("verify")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<TokenResponseDto> VerifyChallenge([FromBody] VerifyRequestDto dto)
    {
        _logger.LogInformation("Verifying challenge for {DID}", dto.Did);

        bool isValid = _authService.VerifyChallenge(dto.Did, dto.Challenge, dto.Signature);

        if (!isValid)
        {
            return Unauthorized(new { message = "Invalid signature or expired challenge" });
        }

        // Generar JWT
        var token = GenerateJwtToken(dto.Did);
        var expiresIn = 3600;

        return Ok(new TokenResponseDto
        {
            Token = token,
            TokenType = "Bearer",
            ExpiresIn = expiresIn,
            Did = dto.Did
        });
    }

    private string GenerateJwtToken(string did)
    {
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "your-super-secret-key-min-32-chars!")
        );
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, did),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("did", did)
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"] ?? "ApheliosID",
            audience: _configuration["Jwt:Audience"] ?? "ApheliosID",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}