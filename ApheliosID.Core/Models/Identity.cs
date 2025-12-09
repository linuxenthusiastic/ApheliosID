using System.Text.Json.Serialization;

namespace ApheliosID.Core.Models
{
    /// <summary>
    /// Representa una identidad descentralizada (DID)
    /// </summary>
    public class Identity
    {
        private string Did { get; }
        private string PublicKey { get; }
        [JsonIgnore]
        private string? PrivateKey { get; }
        private DateTime CreatedAt { get; }
        private bool IsActive { get; set; }
        private Dictionary<string, object> Metadata { get; }

        public Identity(
            string did,
            string publicKey,
            string privateKey,
            Dictionary<string, object>? metadata = null)
        {
            Did = did ?? throw new ArgumentNullException(nameof(did));
            PublicKey = publicKey ?? throw new ArgumentNullException(nameof(publicKey));
            PrivateKey = privateKey;
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
            Metadata = metadata ?? new Dictionary<string, object>();
        }

        
        public Identity(
            string did,
            string publicKey,
            Dictionary<string, object>? metadata = null)
        {
            Did = did ?? throw new ArgumentNullException(nameof(did));
            PublicKey = publicKey ?? throw new ArgumentNullException(nameof(publicKey));
            PrivateKey = null;
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
            Metadata = metadata ?? new Dictionary<string, object>();
        }

        public Identity(
            string did,
            string publicKey,
            DateTime createdAt,
            bool isActive,
            Dictionary<string, object>? metadata = null)
        {
            Did = did;
            PublicKey = publicKey;
            PrivateKey = null;
            CreatedAt = createdAt;
            IsActive = isActive;
            Metadata = metadata ?? new Dictionary<string, object>();
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void Activate()
        {
            IsActive = true;
        }

        public void AddMetadata(string key, object value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Key cannot be null or empty");

            Metadata[key] = value;
        }

        public void RemoveMetadata(string key)
        {
            Metadata.Remove(key);
        }

        public string getDid() => Did;
        public string getPublicKey() => PublicKey;
        public string? getPrivateKey() => PrivateKey;
        public DateTime getCreatedAt() => CreatedAt;
        public bool getIsActive() => IsActive;
        public Dictionary<string, object> getMetadata() => new Dictionary<string, object>(Metadata);
    }
}