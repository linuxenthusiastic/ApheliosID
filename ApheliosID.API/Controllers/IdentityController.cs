using Microsoft.AspNetCore.Mvc;
using ApheliosID.Core.Services;
using ApheliosID.API.DTOs;

namespace ApheliosID.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IdentityService _identityService;
        private readonly ILogger<IdentityController> _logger;

        public IdentityController(IdentityService identityService, ILogger<IdentityController> logger)
        {
            _identityService = identityService;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IdentityResponseDto> RegisterIdentity([FromBody] CreateIdentityDto dto)
        {
            _logger.LogInformation("Registering identity {DID}", dto.Did);

            try
            {
                var identity = _identityService.RegisterIdentity(
                    dto.Did,
                    dto.PublicKey,
                    dto.Metadata
                );

                var response = new IdentityResponseDto
                {
                    Did = identity.getDid(),
                    PublicKey = identity.getPublicKey(),
                    PrivateKey = null,
                    CreatedAt = identity.getCreatedAt(),
                    IsActive = identity.getIsActive(),
                    Metadata = identity.getMetadata()
                };

                return CreatedAtAction(
                    nameof(GetIdentity),
                    new { did = identity.getDid() },
                    response
                );
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning("Invalid identity data: {Message}", ex.Message);
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning("Identity already exists: {Message}", ex.Message);
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{did}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IdentityResponseDto> GetIdentity(string did)
        {
            _logger.LogInformation("Getting identity {DID}", did);

            var identity = _identityService.GetIdentity(did);
            if (identity == null)
            {
                return NotFound(new { message = $"Identity {did} not found" });
            }

            var response = new IdentityResponseDto
            {
                Did = identity.getDid(),
                PublicKey = identity.getPublicKey(),
                PrivateKey = null,
                CreatedAt = identity.getCreatedAt(),
                IsActive = identity.getIsActive(),
                Metadata = identity.getMetadata()
            };

            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<IdentityResponseDto>> GetAllIdentities()
        {
            _logger.LogInformation("Getting all identities");

            var identities = _identityService.GetAllIdentities();

            var response = identities.Select(i => new IdentityResponseDto
            {
                Did = i.getDid(),
                PublicKey = i.getPublicKey(),
                PrivateKey = null,
                CreatedAt = i.getCreatedAt(),
                IsActive = i.getIsActive(),
                Metadata = i.getMetadata()
            }).ToList();

            return Ok(response);
        }

        [HttpPost("{did}/deactivate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<object> DeactivateIdentity(string did)
        {
            _logger.LogInformation("Deactivating identity {DID}", did);

            try
            {
                _identityService.DeactivateIdentity(did);
                return Ok(new { message = $"Identity {did} deactivated successfully" });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost("{did}/activate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<object> ActivateIdentity(string did)
        {
            _logger.LogInformation("Activating identity {DID}", did);

            try
            {
                _identityService.ActivateIdentity(did);
                return Ok(new { message = $"Identity {did} activated successfully" });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}