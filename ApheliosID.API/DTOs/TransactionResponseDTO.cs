namespace ApheliosID.API.DTOs
{
    public class TransactionResponseDto
    {
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public object Data { get; set; } = new object();
        public DateTime Timestamp { get; set; }
        public string Hash { get; set; } = string.Empty;
    }
}