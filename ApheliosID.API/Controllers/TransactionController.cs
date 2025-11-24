using Microsoft.AspNetCore.Mvc;
using ApheliosID.Core;
using ApheliosID.Core.Models;
using ApheliosID.API.DTOs;

namespace ApheliosID.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly Blockchain _blockchain;
        private readonly ILogger<TransactionsController> _logger;

        public TransactionsController(Blockchain blockchain, ILogger<TransactionsController> logger)
        {
            _blockchain = blockchain;
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
                _blockchain.AddTransaction(transaction);

                var response = new TransactionResponseDto
                {
                    From = transaction.getFrom(),
                    To = transaction.getTo(),
                    Data = transaction.getData(),
                    Timestamp = transaction.getTimestamp(),
                    Hash = transaction.getHash()
                };

                return CreatedAtAction(
                    nameof(GetTransactionByHash),
                    new { hash = transaction.getHash() },
                    response
                );
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning("Invalid transaction: {Message}", ex.Message);
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("pending")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<object> GetPendingTransactions()
        {
            _logger.LogInformation("Getting pending transactions");

            var stats = _blockchain.GetStats();
            
            return Ok(new
            {
                pendingCount = ((dynamic)stats).PendingTransactions,
                message = "Use stats endpoint for detailed information",
                statsEndpoint = "/api/blockchain/stats"
            });
        }

        [HttpGet("{hash}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TransactionResponseDto> GetTransactionByHash(string hash)
        {
            _logger.LogInformation("Getting transaction by hash: {Hash}", hash);

            foreach (var block in _blockchain.GetChain())
            {
                foreach (var tx in block.getTransactions())
                {
                    if (tx.getHash() == hash)
                    {
                        var response = new TransactionResponseDto
                        {
                            From = tx.getFrom(),
                            To = tx.getTo(),
                            Data = tx.getData(),
                            Timestamp = tx.getTimestamp(),
                            Hash = tx.getHash()
                        };
                        return Ok(response);
                    }
                }
            }

            return NotFound(new { message = $"Transaction with hash {hash} not found" });
        }

        [HttpPost("batch")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<object> CreateBatchTransactions([FromBody] List<CreateTransactionDto> dtos)
        {
            _logger.LogInformation("Creating {Count} transactions in batch", dtos.Count);

            if (dtos.Count == 0)
            {
                return BadRequest(new { message = "Transaction list cannot be empty" });
            }

            var createdTransactions = new List<TransactionResponseDto>();
            var errors = new List<string>();

            foreach (var dto in dtos)
            {
                try
                {
                    var transaction = new Transaction(dto.From, dto.To, dto.Data);
                    _blockchain.AddTransaction(transaction);

                    createdTransactions.Add(new TransactionResponseDto
                    {
                        From = transaction.getFrom(),
                        To = transaction.getTo(),
                        Data = transaction.getData(),
                        Timestamp = transaction.getTimestamp(),
                        Hash = transaction.getHash()
                    });
                }
                catch (ArgumentException ex)
                {
                    errors.Add($"Transaction from {dto.From} to {dto.To}: {ex.Message}");
                }
            }

            return CreatedAtAction(
                nameof(GetPendingTransactions),
                null,
                new
                {
                    created = createdTransactions.Count,
                    failed = errors.Count,
                    transactions = createdTransactions,
                    errors
                }
            );
        }
    }
}