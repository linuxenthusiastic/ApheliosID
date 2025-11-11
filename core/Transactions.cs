using System;

namespace ApheliosID.Core;

class Transaction
{
    public string From {get;}
    public string To {get;}
    public string Data {get;}
    public DateTime Timestamp {get;}
    public string Signature {get;}
    public string Hash{get;}
    

    public Transaction(string from , TransactionType type, string data)
    {
            if (string.IsNullOrWhiteSpace(from))
                throw new ArgumentException("El campo 'From' no puede estar vacío", nameof(from));
            
            if (string.IsNullOrWhiteSpace(data))
                throw new ArgumentException("El campo 'Data' no puede estar vacío", nameof(data));

            From = from;
            Type = type;
            Data = data;
            Timestamp = DateTime.UtcNow;
            Hash = CalculateHash();
    }

    public Transaction(string from, TransactionType type, string data, 
                          DateTime timestamp, string signature, string hash)
    {
            From = from;
            Type = type;
            Data = data;
            Timestamp = timestamp;
            Signature = signature;
            Hash = hash;
    }

    public string CalculateHash()
    {
            string rawData = $"{From}{Type}{Data}{Timestamp:O}";
            
            byte[] bytes = Encoding.UTF8.GetBytes(rawData);
            
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
    }

    public bool ValidHash()
    {
        return Hash == CalculateHash();
    }
}