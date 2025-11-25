using ApheliosID.Core.Models;

namespace ApheliosID.Core.Managers
{
    /// <summary>
    /// Gestor de la cadena de bloques
    /// Responsabilidad: Almacenar y proporcionar acceso a los bloques
    /// </summary>
    public class BlockchainManager
    {
        private readonly List<Block> _chain;

        public BlockchainManager()
        {
            _chain = new List<Block>();
        }

        public void AddBlock(Block block)
        {
            if (block == null)
                throw new ArgumentNullException(nameof(block));
            
            _chain.Add(block);
        }

        public Block GetLatestBlock()
        {
            if (_chain.Count == 0)
                throw new InvalidOperationException("Chain is empty");
            
            return _chain[_chain.Count - 1];
        }

        public Block? GetBlockByIndex(int index)
        {
            return index >= 0 && index < _chain.Count ? _chain[index] : null;
        }

        public IReadOnlyList<Block> GetChain()
        {
            return _chain.AsReadOnly();
        }

        public int GetBlockCount()
        {
            return _chain.Count;
        }
    }
}