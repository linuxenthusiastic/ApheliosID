using System;
using System.Security.Cryptography;
using System.Text;

namespace ApheliosID.Core;

class Block
{
    int Index {get;}
    string Data {get;}
    DateTime Timestamp {get;}
    string currentHash {get; private set;}
    string previousHash{get;}
    int Nonce{get; private set;};

    public Block(int number,DateTime timestamp, string currenthash , string previoushash, int nonce)
    {
        Index = index;
        Timestamp = DateTime.UtcNow;
        Data = data;
        PreviousHash = previousHash;
        Nonce = 0;
        Hash = CalculateHash();
    }

    private string CalculateHash()
    {
        string rawData = $"{Index}{Timestamp}{Data}{PreviousHash}{Nonce}";

        using(SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            StringBuilder builder = new StringBuilder();
            foreach(byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }

            return builder.ToString();
        }
    }
}