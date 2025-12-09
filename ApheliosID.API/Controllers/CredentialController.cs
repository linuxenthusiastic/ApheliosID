using Microsoft.AspNetCore.Mvc;
using ApheliosID.Core.Services;
using ApheliosID.Core.Models;
using ApheliosID.API.DTOs;

namespace ApheliosID.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CredentialController : ControllerBase
    {
        private readonly CredentialService _credentialService;
        private readonly ILogger<CredentialController> _logger;

        public CredentialController(
            CredentialService credentialService,
            ILogger<CredentialController> logger)
        {
            _credentialService = credentialService;
            _logger = logger;
        }

        [HttpPost("issue")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CredentialResponseDto> IssueCredential([FromBody] IssueCredentialDto dto)
        {
            _logger.LogInformation("Issuing credential from {Issuer} to {Subject}", 
                dto.IssuerDid, dto.SubjectDid);

            try
            {
                var credential = _credentialService.IssueCredential(
                    dto.IssuerDid,
                    dto.IssuerPrivateKey,
                    dto.SubjectDid,
                    dto.Type,
                    dto.Claims,
                    dto.ExpiresAt
                );

                var response = new CredentialResponseDto
                {
                    Id = credential.getId(),
                    Issuer = credential.getIssuer(),
                    Subject = credential.getSubject(),
                    Type = credential.getType(),
                    Claims = credential.getClaims(),
                    IssuedAt = credential.getIssuedAt(),
                    ExpiresAt = credential.getExpiresAt(),
                    Signature = credential.getSignature(),
                    IsRevoked = credential.getIsRevoked(),
                    RevokedAt = credential.getRevokedAt(),
                    IsActive = credential.IsActive()
                };

                return CreatedAtAction(
                    nameof(GetCredential),
                    new { id = credential.getId() },
                    response
                );
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning("Failed to issue credential: {Message}", ex.Message);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error issuing credential");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpPost("verify/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<object> VerifyCredential(string id)
        {
            _logger.LogInformation("Verifying credential {CredentialId}", id);

            var credential = _credentialService.GetCredential(id);
            if (credential == null)
            {
                return NotFound(new { message = $"Credential {id} not found" });
            }

            bool isValid = _credentialService.VerifyCredential(id);

            var reasons = new List<string>();
            
            if (credential.getIsRevoked())
                reasons.Add("Credential is revoked");
            
            if (credential.IsExpired())
                reasons.Add("Credential is expired");
            
            if (!isValid && reasons.Count == 0)
                reasons.Add("Invalid signature");

            return Ok(new
            {
                credentialId = id,
                isValid,
                isActive = credential.IsActive(),
                isRevoked = credential.getIsRevoked(),
                isExpired = credential.IsExpired(),
                reasons = reasons.Count > 0 ? reasons : null,
                verifiedAt = DateTime.UtcNow
            });
        }

        [HttpPost("revoke/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<object> RevokeCredential(string id, [FromBody] RevokeCredentialDto dto)
        {
            _logger.LogInformation("Revoking credential {CredentialId} by {Issuer}", 
                id, dto.IssuerDid);

            try
            {
                _credentialService.RevokeCredential(id, dto.IssuerDid, dto.IssuerPrivateKey);

                return Ok(new
                {
                    message = "Credential revoked successfully",
                    credentialId = id,
                    revokedBy = dto.IssuerDid,
                    revokedAt = DateTime.UtcNow
                });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error revoking credential");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CredentialResponseDto> GetCredential(string id)
        {
            _logger.LogInformation("Getting credential {CredentialId}", id);

            var credential = _credentialService.GetCredential(id);
            if (credential == null)
            {
                return NotFound(new { message = $"Credential {id} not found" });
            }

            var response = new CredentialResponseDto
            {
                Id = credential.getId(),
                Issuer = credential.getIssuer(),
                Subject = credential.getSubject(),
                Type = credential.getType(),
                Claims = credential.getClaims(),
                IssuedAt = credential.getIssuedAt(),
                ExpiresAt = credential.getExpiresAt(),
                Signature = credential.getSignature(),
                IsRevoked = credential.getIsRevoked(),
                RevokedAt = credential.getRevokedAt(),
                IsActive = credential.IsActive()
            };

            return Ok(response);
        }

        [HttpGet("subject/{did}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<CredentialResponseDto>> GetCredentialsBySubject(string did)
        {
            _logger.LogInformation("Getting credentials for subject {DID}", did);

            var credentials = _credentialService.GetCredentialsBySubject(did);

            var response = credentials.Select(c => new CredentialResponseDto
            {
                Id = c.getId(),
                Issuer = c.getIssuer(),
                Subject = c.getSubject(),
                Type = c.getType(),
                Claims = c.getClaims(),
                IssuedAt = c.getIssuedAt(),
                ExpiresAt = c.getExpiresAt(),
                Signature = c.getSignature(),
                IsRevoked = c.getIsRevoked(),
                RevokedAt = c.getRevokedAt(),
                IsActive = c.IsActive()
            }).ToList();

            return Ok(response);
        }

        [HttpGet("issuer/{did}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<CredentialResponseDto>> GetCredentialsByIssuer(string did)
        {
            _logger.LogInformation("Getting credentials issued by {DID}", did);

            var credentials = _credentialService.GetCredentialsByIssuer(did);

            var response = credentials.Select(c => new CredentialResponseDto
            {
                Id = c.getId(),
                Issuer = c.getIssuer(),
                Subject = c.getSubject(),
                Type = c.getType(),
                Claims = c.getClaims(),
                IssuedAt = c.getIssuedAt(),
                ExpiresAt = c.getExpiresAt(),
                Signature = c.getSignature(),
                IsRevoked = c.getIsRevoked(),
                RevokedAt = c.getRevokedAt(),
                IsActive = c.IsActive()
            }).ToList();

            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<CredentialResponseDto>> GetAllCredentials()
        {
            _logger.LogInformation("Getting all credentials");

            var credentials = _credentialService.GetAllCredentials();

            var response = credentials.Select(c => new CredentialResponseDto
            {
                Id = c.getId(),
                Issuer = c.getIssuer(),
                Subject = c.getSubject(),
                Type = c.getType(),
                Claims = c.getClaims(),
                IssuedAt = c.getIssuedAt(),
                ExpiresAt = c.getExpiresAt(),
                Signature = c.getSignature(),
                IsRevoked = c.getIsRevoked(),
                RevokedAt = c.getRevokedAt(),
                IsActive = c.IsActive()
            }).ToList();

            return Ok(response);
        }
    }
}