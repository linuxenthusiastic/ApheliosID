using System.Text.Json;

namespace ApheliosID.Core.Models
{
    /// <summary>
    /// Representa una transaction inmutable en la blockchain
    /// </summary>
    public class Transaction
    {
        private string From { get; }
        private string To { get; }
        private object Data { get; }
        private DateTime Timestamp { get; }
        private string Hash { get; }

        public Transaction(string from, string to, object data)
        {
            From = from ?? throw new ArgumentNullException(nameof(from));
            To = to ?? throw new ArgumentNullException(nameof(to));
            Data = data ?? throw new ArgumentNullException(nameof(data));
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

        public string getFrom() => From;
        public string getTo() => To;
        public object getData() => Data;
        public DateTime getTimestamp() => Timestamp;
        public string getHash() => Hash;
    }
}