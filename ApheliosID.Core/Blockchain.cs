using ApheliosID.Core.Models;

namespace ApheliosID.Core
{
    public class Blockchain
    {
        private readonly List<Block> Chain;
        private List<Transaction> PendingTransactions { get; set; }
        private int TransactionsPerBlock { get; set; }

        public Blockchain(int transactionsPerBlock = 5)
        {
            Chain = new List<Block>();
            PendingTransactions = new List<Transaction>();
            TransactionsPerBlock = transactionsPerBlock;
            Chain.Add(CreateGenesisBlock());
        }

        private Block CreateGenesisBlock()
        {
            var genesisTx = new Transaction(
                "SYSTEM",
                "GENESIS",
                new { message = "Genesis Block - ApheliosID" }
            );

            return new Block(0, new List<Transaction> { genesisTx }, "0");
        }

        public Block GetLatestBlock()
        {
            return Chain[Chain.Count - 1];
        }

        public Transaction AddTransaction(Transaction transaction)
        {
            if (string.IsNullOrEmpty(transaction.getFrom()))
                throw new ArgumentException("Transaction must include From address");

            if (string.IsNullOrEmpty(transaction.getTo()))
                throw new ArgumentException("Transaction must include To address");

            PendingTransactions.Add(transaction);

            Console.WriteLine($"Transaction added. Pending: {PendingTransactions.Count}/{TransactionsPerBlock}");

            if (PendingTransactions.Count >= TransactionsPerBlock)
            {
                CreateBlock();
            }

            return transaction;
        }

        public Block CreateBlock()
        {
            if (PendingTransactions.Count == 0)
                throw new InvalidOperationException("No pending transactions to create a block");

            var latestBlock = GetLatestBlock();

            var block = new Block(
                Chain.Count,
                new List<Transaction>(PendingTransactions),
                latestBlock.getHash()
            );

            Chain.Add(block);
            PendingTransactions.Clear();

            Console.WriteLine($"Block #{block.getIndex()} created with {block.getTransactions().Count} transactions");

            return block;
        }

        public Block? ForceCreateBlock()
        {
            if (PendingTransactions.Count == 0)
            {
                Console.WriteLine("No pending transactions to create block");
                return null;
            }

            return CreateBlock();
        }

        public bool IsChainValid()
        {
            for (int i = 1; i < Chain.Count; i++)
            {
                Block currentBlock = Chain[i];
                Block previousBlock = Chain[i - 1];

                if (!currentBlock.IsValid())
                {
                    Console.WriteLine($"Block #{i} has invalid hash");
                    return false;
                }

                if (currentBlock.getPreviousHash() != previousBlock.getHash())
                {
                    Console.WriteLine($"Block #{i} has invalid previous hash");
                    return false;
                }

                if (!currentBlock.HasValidTransactions())
                {
                    Console.WriteLine($"Block #{i} has invalid transactions");
                    return false;
                }
            }

            return true;
        }

        public Block? GetBlockByIndex(int index)
        {
            return index >= 0 && index < Chain.Count ? Chain[index] : null;
        }

        public IReadOnlyList<Block> GetChain() => Chain.AsReadOnly();

        public object GetStats()
        {
            return new
            {
                TotalBlocks = Chain.Count,
                PendingTransactions = PendingTransactions.Count,
                TransactionsPerBlock = TransactionsPerBlock,
                IsValid = IsChainValid(),
                LatestBlockHash = GetLatestBlock().getHash()
            };
        }
    }
}