using ApheliosID.Core.Models;

namespace ApheliosID.Core.Interfaces
{
    /// <summary>
    /// Interfaz para el servicio de blockchain
    /// Define el contrato para operaciones de blockchain
    /// </summary>
    public interface IBlockchainService
    {
        Transaction AddTransaction(Transaction transaction);
        Block? ForceCreateBlock();
        bool IsChainValid();
        Block? GetBlockByIndex(int index);
        Block GetLatestBlock();
        IReadOnlyList<Block> GetChain();
        object GetStats();
    }
}