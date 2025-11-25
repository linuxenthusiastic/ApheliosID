using Microsoft.AspNetCore.Mvc;
using ApheliosID.Core.Interfaces;
using ApheliosID.Core.Models;
using ApheliosID.API.DTOs;

namespace ApheliosID.API.Controllers
{
    /// <summary>
    /// Controlador para operaciones de transactions
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly IBlockchainService _blockchainService;
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(IBlockchainService blockchainService, ILogger<TransactionController> logger)
        {
            _blockchainService = blockchainService;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TransactionResponseDto> CreateTransaction([FromBody] CreateTransactionDto dto)
        {
            _logger.LogInformation("Creating transaction from {From} to {To}", dto.From, dto.To);

            try
            {
                var transaction = new Transaction(dto.From, dto.To, dto.Data);
                _blockchainService.AddTransaction(transaction);

                var response = new TransactionResponseDto
                {
                    From = transaction.getFrom(),
                    To = transaction.getTo(),
                    Data = transaction.getData(),
                    Timestamp = transaction.getTimestamp(),
                    Hash = transaction.getHash()
                };

                return CreatedAtAction(
                    nameof(CreateTransaction),
                    response
                );
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning("Invalid transaction: {Message}", ex.Message);
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}