using ApheliosID.Core.Models;

namespace ApheliosID.Core.Managers
{
    /// <summary>
    /// Validador de bloques y cadenas
    /// Responsabilidad: Verificar la integridad de bloques y la cadena completa
    /// </summary>
    public class BlockValidator
    {

        public bool ValidateBlock(Block block)
        {
            if (block == null)
                return false;
            
            if (!block.IsValid())
                return false;
            
            if (!block.HasValidTransactions())
                return false;
            
            return true;
        }

        public bool ValidateBlockLink(Block currentBlock, Block previousBlock)
        {
            if (currentBlock == null || previousBlock == null)
                return false;
            
            if (currentBlock.getPreviousHash() != previousBlock.getHash())
            {
                Console.WriteLine($"Block #{currentBlock.getIndex()} has invalid previous hash");
                return false;
            }
            
            if (currentBlock.getIndex() != previousBlock.getIndex() + 1)
            {
                Console.WriteLine($"Block #{currentBlock.getIndex()} has invalid index sequence");
                return false;
            }
            
            return true;
        }

        public bool ValidateChain(IReadOnlyList<Block> chain)
        {
            if (chain == null || chain.Count == 0)
                return false;
            
            for (int i = 1; i < chain.Count; i++)
            {
                Block currentBlock = chain[i];
                Block previousBlock = chain[i - 1];
                
                if (!ValidateBlock(currentBlock))
                {
                    Console.WriteLine($"❌ Block #{i} is invalid");
                    return false;
                }
                
                if (!ValidateBlockLink(currentBlock, previousBlock))
                {
                    Console.WriteLine($"❌ Block #{i} has invalid link to previous block");
                    return false;
                }
            }
            
            return true;
        }

        public bool ValidateChainDetailed(IReadOnlyList<Block> chain, out List<string> validationErrors)
        {
            validationErrors = new List<string>();
            
            if (chain == null || chain.Count == 0)
            {
                validationErrors.Add("Chain is null or empty");
                return false;
            }
            
            bool isValid = true;
            
            for (int i = 1; i < chain.Count; i++)
            {
                Block currentBlock = chain[i];
                Block previousBlock = chain[i - 1];
                
                if (!currentBlock.IsValid())
                {
                    validationErrors.Add($"Block #{i} has invalid hash");
                    isValid = false;
                }
                
                if (!currentBlock.HasValidTransactions())
                {
                    validationErrors.Add($"Block #{i} has invalid transactions");
                    isValid = false;
                }
                
                if (currentBlock.getPreviousHash() != previousBlock.getHash())
                {
                    validationErrors.Add($"Block #{i} has invalid previous hash");
                    isValid = false;
                }
                
                if (currentBlock.getIndex() != previousBlock.getIndex() + 1)
                {
                    validationErrors.Add($"Block #{i} has invalid index");
                    isValid = false;
                }
            }
            
            return isValid;
        }
    }
}