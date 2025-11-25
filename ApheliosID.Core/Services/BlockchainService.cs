using ApheliosID.Core.Interfaces;
using ApheliosID.Core.Managers;
using ApheliosID.Core.Models;

namespace ApheliosID.Core.Services
{
    /// <summary>
    /// Servicio principal de blockchain
    /// Responsabilidad: Orquestar los diferentes managers y exponer APIs para operaciones de blockchain
    /// </summary>
    public class BlockchainService : IBlockchainService
    {
        private readonly BlockchainManager _chainManager;
        private readonly TransactionPool _transactionPool;
        private readonly BlockFactory _blockFactory;
        private readonly BlockValidator _blockValidator;

        public BlockchainService(int transactionsPerBlock = 5)
        {
            _chainManager = new BlockchainManager();
            _transactionPool = new TransactionPool(transactionsPerBlock);
            _blockFactory = new BlockFactory();
            _blockValidator = new BlockValidator();

            InitializeChain();
        }

        private void InitializeChain()
        {
            var genesisBlock = _blockFactory.CreateGenesisBlock();
            _chainManager.AddBlock(genesisBlock);
        }

        public Transaction AddTransaction(Transaction transaction)
        {
            if (transaction == null)
                throw new ArgumentNullException(nameof(transaction));

            _transactionPool.AddTransaction(transaction);

            Console.WriteLine($"Transaction added. Pending: {_transactionPool.GetPendingCount()}/{_transactionPool.GetMaxTransactionsPerBlock()}");

            if (_transactionPool.IsFull())
            {
                CreateBlock();
            }

            return transaction;
        }

        private Block CreateBlock()
        {
            if (!_transactionPool.HasPendingTransactions())
                throw new InvalidOperationException("No pending transactions to create a block");

            var pendingTransactions = _transactionPool.GetPendingTransactions();

            var latestBlock = _chainManager.GetLatestBlock();

            var newBlock = _blockFactory.CreateBlock(
                _chainManager.GetBlockCount(),
                pendingTransactions,
                latestBlock.getHash()
            );

            _chainManager.AddBlock(newBlock);

            _transactionPool.Clear();

            Console.WriteLine($"Block #{newBlock.getIndex()} created with {newBlock.getTransactions().Count} transactions");

            return newBlock;
        }

        public Block? ForceCreateBlock()
        {
            if (!_transactionPool.HasPendingTransactions())
            {
                Console.WriteLine("No pending transactions to create block");
                return null;
            }

            return CreateBlock();
        }

        public bool IsChainValid()
        {
            var chain = _chainManager.GetChain();
            return _blockValidator.ValidateChain(chain);
        }

        public bool IsChainValidDetailed(out List<string> errors)
        {
            var chain = _chainManager.GetChain();
            return _blockValidator.ValidateChainDetailed(chain, out errors);
        }

        public Block? GetBlockByIndex(int index)
        {
            return _chainManager.GetBlockByIndex(index);
        }

        public Block GetLatestBlock()
        {
            return _chainManager.GetLatestBlock();
        }

        public IReadOnlyList<Block> GetChain()
        {
            return _chainManager.GetChain();
        }

        public object GetStats()
        {
            var chain = _chainManager.GetChain();
            var latestBlock = _chainManager.GetLatestBlock();

            return new
            {
                TotalBlocks = _chainManager.GetBlockCount(),
                PendingTransactions = _transactionPool.GetPendingCount(),
                TransactionsPerBlock = _transactionPool.GetMaxTransactionsPerBlock(),
                IsValid = _blockValidator.ValidateChain(chain),
                LatestBlockHash = latestBlock.getHash()
            };
        }
    }
}
