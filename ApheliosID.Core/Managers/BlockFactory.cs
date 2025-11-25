using ApheliosID.Core.Models;

namespace ApheliosID.Core.Managers
{
    /// <summary>
    /// Fabrica para crear bloques
    /// Responsabilidad: Crear bloques génesis y bloques normales
    /// </summary>
    public class BlockFactory
    {
        public Block CreateGenesisBlock()
        {
            var genesisTx = new Transaction(
                "SYSTEM",
                "GENESIS",
                new { message = "Genesis Block - ApheliosID" }
            );
            
            return new Block(0, new List<Transaction> { genesisTx }, "0");
        }

        public Block CreateBlock(int index, List<Transaction> transactions, string previousHash)
        {
            if (transactions == null || transactions.Count == 0)
                throw new ArgumentException("Block must contain at least one transaction");
            
            if (string.IsNullOrEmpty(previousHash))
                throw new ArgumentException("Previous hash is required");
            
            return new Block(index, transactions, previousHash);
        }
    }
}