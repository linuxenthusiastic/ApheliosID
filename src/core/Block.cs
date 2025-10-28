using System;

namespace ApheliosID.Core;

class Block
{
    public int Number {get;}
    public string Hash {get; private set;}
    public string PreviousHash {get;}
    public DateTime Timestamp {get;}
    public string Data {get;}

    public Block(int number , string data , string previousHash)
    {
        Number = number;
        Data = data;
        PreviousHash = previousHash;
        Timestamp = DateTime.UtcNow;
        Hash = CalculateHash();
    }

    public string CalculateHash()
    {
        
    }

    public bool isValid()
    {
        return Hash == CalculateHash();
    }
}