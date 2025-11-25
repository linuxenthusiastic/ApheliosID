using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace ApheliosID.Core.Models
{
    /// <summary>
    /// Representa un bloque inmutable en la blockchain
    /// </summary>
    public class Block
    {
        private readonly List<Transaction> _transactions;
        private int Index { get; }
        private DateTime Timestamp { get; }
        private string PreviousHash { get; }
        private string Hash { get; }
        public Block(int index, List<Transaction> transactions, string previousHash = "")
        {
            Index = index;
            Timestamp = DateTime.UtcNow;
            _transactions = new List<Transaction>(transactions ?? new List<Transaction>());
            PreviousHash = previousHash;
            Hash = CalculateHash();
        }

        public string CalculateHash()
        {
            string rawData = $"{Index}{PreviousHash}{Timestamp:O}{JsonSerializer.Serialize(_transactions)}";
            HashCalculator hashCalculator = new HashCalculator(rawData);
            return hashCalculator.CalculateSHA256();
        }

        public bool HasValidTransactions()
        {
            foreach (var transaction in _transactions)
            {
                if (!transaction.IsValid())
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsValid()
        {
            return Hash == CalculateHash() && HasValidTransactions();
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(new
            {
                Index,
                Timestamp,
                Transactions = _transactions,
                PreviousHash,
                Hash
            }, new JsonSerializerOptions { WriteIndented = true });
        }

        public int getIndex() => Index;
        public DateTime getTimestamp() => Timestamp;
        public List<Transaction> getTransactions() => new List<Transaction>(_transactions);
        public string getPreviousHash() => PreviousHash;
        public string getHash() => Hash;
    }
}