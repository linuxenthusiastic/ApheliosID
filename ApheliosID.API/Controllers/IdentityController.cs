using Microsoft.AspNetCore.Mvc;
using ApheliosID.Core.Services;
using ApheliosID.API.DTOs;

namespace ApheliosID.API.Controllers
{
    /// <summary>
    /// Controlador para operaciones de identidades descentralizadas
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IdentityService _identityService;
        private readonly CryptoService _cryptoService;
        private readonly ILogger<IdentityController> _logger;

        public IdentityController(
            IdentityService identityService,
            CryptoService cryptoService,
            ILogger<IdentityController> logger)
        {
            _identityService = identityService;
            _cryptoService = cryptoService;
            _logger = logger;
        }

        [HttpPost("create-with-keys")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<object> CreateIdentityWithKeys([FromBody] CreateIdentityWithKeysDto dto)
        {
            _logger.LogInformation("Creating new identity with generated keys");

            try
            {
                var (publicKey, privateKey) = _cryptoService.GenerateKeyPair();
                
                var did = _cryptoService.GenerateDid(publicKey);
                
                var identity = _identityService.CreateIdentity(did, publicKey, dto.Metadata);
                
                var response = new
                {
                    did = identity.getDid(),
                    publicKey = identity.getPublicKey(),
                    privateKey = privateKey, //solo se envia al cliente
                    createdAt = identity.getCreatedAt(),
                    isActive = identity.getIsActive(),
                    metadata = identity.getMetadata(),
                    warning = "⚠️ SAVE YOUR PRIVATE KEY - It will never be shown again!"
                };

                _logger.LogInformation("✅ Identity created: {DID}", did);

                return CreatedAtAction(nameof(GetIdentity), new { did = identity.getDid() }, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating identity with keys");
                return StatusCode(500, new { message = "Error creating identity", error = ex.Message });
            }
        }
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IdentityResponseDto> RegisterIdentity([FromBody] CreateIdentityDto dto)
        {
            _logger.LogInformation("Registering identity {DID}", dto.Did);

            try
            {
                var identity = _identityService.CreateIdentity(dto.Did, dto.PublicKey, dto.Metadata);

                var response = new IdentityResponseDto
                {
                    Did = identity.getDid(),
                    PublicKey = identity.getPublicKey(),
                    PrivateKey = null,  // NUNCA se retorna en este endpoint
                    CreatedAt = identity.getCreatedAt(),
                    IsActive = identity.getIsActive(),
                    Metadata = identity.getMetadata()
                };

                _logger.LogInformation("✅ Identity registered: {DID}", dto.Did);

                return CreatedAtAction(nameof(GetIdentity), new { did = identity.getDid() }, response);
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

        [HttpPost("generate-keys")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<object> GenerateKeys()
        {
            _logger.LogInformation("Generating RSA key pair");

            var (publicKey, privateKey) = _cryptoService.GenerateKeyPair();
            var did = _cryptoService.GenerateDid(publicKey);

            return Ok(new
            {
                did,
                publicKey,
                privateKey,
                message = "Keys generated successfully. Use POST /api/identity/register to create the identity."
            });
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