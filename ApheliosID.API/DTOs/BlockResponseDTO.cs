namespace ApheliosID.API.DTOs
{
    public class BlockResponseDto
    {
        public int Index { get; set; }
        public DateTime Timestamp { get; set; }
        public List<TransactionResponseDto> Transactions { get; set; } = new();
        public string PreviousHash { get; set; } = string.Empty;
        public string Hash { get; set; } = string.Empty;
        public int TransactionCount { get; set; }
    }
}