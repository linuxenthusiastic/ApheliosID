using System;
using System.Collections.Generic;

namespace ApheliosID.Core
{
    class Blockchain
    {
        public List<Transaction> PendingTransactions { get; private set; }
        public List<Block> Chain { get; private set; }
        public int Difficulty { get; private set; }
        
        public Blockchain(int difficulty = 0)
        {
            Difficulty = difficulty;
            Chain = new List<Block>();
            PendingTransactions = new List<Transaction>();
            CreateGenesisBlock();
        }
        
        private void CreateGenesisBlock()
        {
            Block genesis = new Block(
                index: 0,
                timestamp: DateTime.Now,
                data: "Bloque Genesis - ApheliosID",
                previousHash: "0"
            );
            
            Chain.Add(genesis);
        }
        
        public Block GetLatestBlock()
        {
            return Chain[Chain.Count - 1];
        }
        
        public void AddBlock(string data)
        {
            Block previousBlock = GetLatestBlock();
            
            Block newBlock = new Block(
                index: previousBlock.Index + 1,
                timestamp: DateTime.Now,
                data: data,
                previousHash: previousBlock.Hash
            );
            
            Chain.Add(newBlock);
        }
        
        public bool IsChainValid()
        {
            for (int i = 1; i < Chain.Count; i++)
            {
                Block currentBlock = Chain[i];
                Block previousBlock = Chain[i - 1];
                
                if (currentBlock.Hash != currentBlock.CalculateHash())
                {
                    return false;
                }
                
                if (currentBlock.PreviousHash != previousBlock.Hash)
                {
                    return false;
                }
            }
            return true;
        }
    }
}