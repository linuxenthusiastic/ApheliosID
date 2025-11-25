using Microsoft.AspNetCore.Mvc;
using ApheliosID.Core.Interfaces;
using ApheliosID.Core.Models;
using ApheliosID.API.DTOs;

namespace ApheliosID.API.Controllers
{
    /// <summary>
    /// Controlador para operaciones de blockchain
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class BlockchainController : ControllerBase
    {
        private readonly IBlockchainService _blockchainService;
        private readonly ILogger<BlockchainController> _logger;

        public BlockchainController(IBlockchainService blockchainService, ILogger<BlockchainController> logger)
        {
            _blockchainService = blockchainService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<BlockResponseDto>> GetBlockchain()
        {
            _logger.LogInformation("Getting entire blockchain");

            var blocks = _blockchainService.GetChain().Select(block => new BlockResponseDto
            {
                Index = block.getIndex(),
                Timestamp = block.getTimestamp(),
                Transactions = block.getTransactions().Select(tx => new TransactionResponseDto
                {
                    From = tx.getFrom(),
                    To = tx.getTo(),
                    Data = tx.getData(),
                    Timestamp = tx.getTimestamp(),
                    Hash = tx.getHash()
                }).ToList(),
                PreviousHash = block.getPreviousHash(),
                Hash = block.getHash(),
                TransactionCount = block.getTransactions().Count
            }).ToList();

            return Ok(blocks);
        }

        [HttpGet("stats")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<object> GetStats()
        {
            _logger.LogInformation("Getting blockchain stats");
            return Ok(_blockchainService.GetStats());
        }

        [HttpGet("block/{index}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<BlockResponseDto> GetBlockByIndex(int index)
        {
            _logger.LogInformation("Getting block by index: {Index}", index);

            var block = _blockchainService.GetBlockByIndex(index);

            if (block == null)
            {
                return NotFound(new { message = $"Block with index {index} not found" });
            }

            var blockDto = new BlockResponseDto
            {
                Index = block.getIndex(),
                Timestamp = block.getTimestamp(),
                Transactions = block.getTransactions().Select(tx => new TransactionResponseDto
                {
                    From = tx.getFrom(),
                    To = tx.getTo(),
                    Data = tx.getData(),
                    Timestamp = tx.getTimestamp(),
                    Hash = tx.getHash()
                }).ToList(),
                PreviousHash = block.getPreviousHash(),
                Hash = block.getHash(),
                TransactionCount = block.getTransactions().Count
            };

            return Ok(blockDto);
        }

        [HttpGet("latest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<BlockResponseDto> GetLatestBlock()
        {
            _logger.LogInformation("Getting latest block");

            var block = _blockchainService.GetLatestBlock();

            var blockDto = new BlockResponseDto
            {
                Index = block.getIndex(),
                Timestamp = block.getTimestamp(),
                Transactions = block.getTransactions().Select(tx => new TransactionResponseDto
                {
                    From = tx.getFrom(),
                    To = tx.getTo(),
                    Data = tx.getData(),
                    Timestamp = tx.getTimestamp(),
                    Hash = tx.getHash()
                }).ToList(),
                PreviousHash = block.getPreviousHash(),
                Hash = block.getHash(),
                TransactionCount = block.getTransactions().Count
            };

            return Ok(blockDto);
        }

        [HttpGet("validate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<object> ValidateChain()
        {
            _logger.LogInformation("Validating blockchain");

            bool isValid = _blockchainService.IsChainValid();

            return Ok(new
            {
                isValid,
                message = isValid ? "✅ Blockchain is valid" : "❌ Blockchain is invalid",
                totalBlocks = _blockchainService.GetChain().Count,
                timestamp = DateTime.UtcNow
            });
        }

        [HttpPost("force-block")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<BlockResponseDto> ForceCreateBlock()
        {
            _logger.LogInformation("Forcing block creation");

            var block = _blockchainService.ForceCreateBlock();

            if (block == null)
            {
                return BadRequest(new { message = "No pending transactions to create a block" });
            }

            var blockDto = new BlockResponseDto
            {
                Index = block.getIndex(),
                Timestamp = block.getTimestamp(),
                Transactions = block.getTransactions().Select(tx => new TransactionResponseDto
                {
                    From = tx.getFrom(),
                    To = tx.getTo(),
                    Data = tx.getData(),
                    Timestamp = tx.getTimestamp(),
                    Hash = tx.getHash()
                }).ToList(),
                PreviousHash = block.getPreviousHash(),
                Hash = block.getHash(),
                TransactionCount = block.getTransactions().Count
            };

            return CreatedAtAction(
                nameof(GetBlockByIndex),
                new { index = block.getIndex() },
                blockDto
            );
        }
    }
}