using ApheliosID.Core.Services;
using ApheliosID.Core.Models;
using System.Text.Json;

Console.WriteLine("Testing ApheliosID Blockchain\n");
Console.WriteLine("═══════════════════════════════════════\n");

var blockchain = new BlockchainService(transactionsPerBlock: 3);

Console.WriteLine("Initial Stats:");
Console.WriteLine(JsonSerializer.Serialize(
    blockchain.GetStats(), 
    new JsonSerializerOptions { WriteIndented = true }
));

Console.WriteLine("\n═══════════════════════════════════════\n");
Console.WriteLine("Adding transactions...\n");

blockchain.AddTransaction(new Transaction(
    "did:aphelios:alice",
    "did:aphelios:bob",
    new { action = "register", role = "user" }
));

blockchain.AddTransaction(new Transaction(
    "did:aphelios:bob",
    "did:aphelios:charlie",
    new { action = "credential", type = "developer" }
));

blockchain.AddTransaction(new Transaction(
    "did:aphelios:charlie",
    "did:aphelios:alice",
    new { action = "verify", status = "approved" }
));

Console.WriteLine("\n═══════════════════════════════════════\n");
Console.WriteLine("Adding more transactions...\n");

blockchain.AddTransaction(new Transaction(
    "did:aphelios:alice",
    "did:aphelios:dave",
    new { action = "login", timestamp = DateTime.UtcNow }
));

blockchain.AddTransaction(new Transaction(
    "did:aphelios:dave",
    "did:aphelios:bob",
    new { action = "update", field = "email" }
));

Console.WriteLine("\nForcing block creation...");
var newBlock = blockchain.ForceCreateBlock();

if (newBlock != null)
{
    Console.WriteLine($"Block #{newBlock.getIndex()} created with {newBlock.getTransactions().Count} transactions");
}

Console.WriteLine("\n═══════════════════════════════════════\n");
Console.WriteLine("Final Stats:");
Console.WriteLine(JsonSerializer.Serialize(
    blockchain.GetStats(), 
    new JsonSerializerOptions { WriteIndented = true }
));

Console.WriteLine("\n═══════════════════════════════════════\n");
Console.WriteLine("Validating blockchain...");
bool isValid = blockchain.IsChainValid();
Console.WriteLine($"\nChain is {(isValid ? " VALID" : " INVALID")}");

Console.WriteLine("\n═══════════════════════════════════════\n");
Console.WriteLine("All Blocks:\n");

foreach (var block in blockchain.GetChain())
{
    Console.WriteLine($"Block #{block.getIndex()}");
    Console.WriteLine($"  Hash:         {block.getHash()}");
    Console.WriteLine($"  PreviousHash: {block.getPreviousHash()}");
    Console.WriteLine($"  Transactions: {block.getTransactions().Count}");
    Console.WriteLine($"  Timestamp:    {block.getTimestamp():yyyy-MM-dd HH:mm:ss}");
    Console.WriteLine();
}

Console.WriteLine("═══════════════════════════════════════");
Console.WriteLine("✅ Test completed successfully!");