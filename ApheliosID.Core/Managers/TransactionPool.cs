using ApheliosID.Core.Models;

namespace ApheliosID.Core.Managers
{
    /// <summary>
    /// Gestor del pool de transacciones pendientes
    /// Responsabilidad: Gestionar transacciones que aún no están en un bloque
    /// </summary>
    public class TransactionPool
    {
        private readonly List<Transaction> _pendingTransactions;
        private readonly int _maxTransactionsPerBlock;

        public TransactionPool(int maxTransactionsPerBlock = 5)
        {
            _pendingTransactions = new List<Transaction>();
            _maxTransactionsPerBlock = maxTransactionsPerBlock;
        }
        
        public void AddTransaction(Transaction transaction)
        {
            if (transaction == null)
                throw new ArgumentNullException(nameof(transaction));
            
            if (string.IsNullOrEmpty(transaction.getFrom()))
                throw new ArgumentException("Transaction must include From address");
            
            if (string.IsNullOrEmpty(transaction.getTo()))
                throw new ArgumentException("Transaction must include To address");
            
            _pendingTransactions.Add(transaction);
        }

        public bool IsFull()
        {
            return _pendingTransactions.Count >= _maxTransactionsPerBlock;
        }

        public List<Transaction> GetPendingTransactions()
        {
            return new List<Transaction>(_pendingTransactions);
        }

        public int GetPendingCount()
        {
            return _pendingTransactions.Count;
        }


        public bool HasPendingTransactions()
        {
            return _pendingTransactions.Count > 0;
        }

        public void Clear()
        {
            _pendingTransactions.Clear();
        }

        public int GetMaxTransactionsPerBlock()
        {
            return _maxTransactionsPerBlock;
        }
    }
}