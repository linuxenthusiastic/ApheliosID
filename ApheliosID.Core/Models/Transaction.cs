using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace ApheliosID.Core.Models
{
    public class Transaction
    {
        private string From { get;}
        private string To { get;}
        private object Data { get;}
        private DateTime Timestamp { get;}
        private string Hash { get;}

        public Transaction(string from, string to, object data)
        {
            From = from;
            To = to;
            Data = data;
            Timestamp = DateTime.UtcNow;
            Hash = CalculateHash();
        }

        public string CalculateHash()
        {
            string rawData = $"{From}{To}{JsonSerializer.Serialize(Data)}{Timestamp:O}";
            HashCalculator hashCalculator = new HashCalculator(rawData);
            return hashCalculator.CalculateSHA256();
        }

        public bool IsValid()
        {
            return Hash == CalculateHash();
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions 
            { 
                WriteIndented = true 
            });
        }

        public string getFrom()
        {
            return From;
        }

        public string getTo()
        {
            return To;
        }

        public object getData()
        {
            return Data;
        }

        public DateTime getTimestamp()
        {
            return Timestamp;
        }

        public string getHash()
        {
            return Hash;
        }
    }
}