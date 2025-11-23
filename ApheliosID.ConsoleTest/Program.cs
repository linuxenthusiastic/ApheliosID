using ApheliosID.Core;
using ApheliosID.Core.Models;
using System.Text.Json;

Console.WriteLine("Testing ApheliosID Blockchain\n");
Console.WriteLine("═══════════════════════════════════════\n");

var aphelios = new Blockchain(transactionsPerBlock: 3);

Console.WriteLine("Initial Stats:");
Console.WriteLine(JsonSerializer.Serialize(aphelios.GetStats(), new JsonSerializerOptions { WriteIndented = true }));
Console.WriteLine("\n═══════════════════════════════════════\n");

Console.WriteLine("Adding transactions...\n");

aphelios.AddTransaction(new Transaction(
    "did:aphelios:alice",
    "did:aphelios:bob",
    new { action = "register", role = "user" }
));

aphelios.AddTransaction(new Transaction(
    "did:aphelios:bob",
    "did:aphelios:charlie",
    new { action = "credential", type = "developer" }
));

aphelios.AddTransaction(new Transaction(
    "did:aphelios:charlie",
    "did:aphelios:alice",
    new { action = "verify", status = "approved" }
));

Console.WriteLine("\n═══════════════════════════════════════\n");

aphelios.AddTransaction(new Transaction(
    "did:aphelios:alice",
    "did:aphelios:dave",
    new { action = "login", timestamp = DateTime.UtcNow }
));

aphelios.AddTransaction(new Transaction(
    "did:aphelios:dave",
    "did:aphelios:bob",
    new { action = "update", field = "email" }
));

Console.WriteLine("\nForcing block creation...");
aphelios.ForceCreateBlock();

Console.WriteLine("\n═══════════════════════════════════════\n");

Console.WriteLine("Final Stats:");
Console.WriteLine(JsonSerializer.Serialize(aphelios.GetStats(), new JsonSerializerOptions { WriteIndented = true }));

Console.WriteLine("\n═══════════════════════════════════════\n");

Console.WriteLine("Validating blockchain...");
bool isValid = aphelios.IsChainValid();
Console.WriteLine($"Chain is {(isValid ? "✅ VALID" : "❌ INVALID")}");

Console.WriteLine("\n═══════════════════════════════════════\n");

Console.WriteLine("All Blocks:\n");
foreach (var block in aphelios.GetChain())
{
    Console.WriteLine($"Block #{block.getIndex()}");
    Console.WriteLine($"  Hash: {block.getHash()[..16]}...");
    Console.WriteLine($"  Previous: {block.getPreviousHash()[..16]}...");
    Console.WriteLine($"  Transactions: {block.getTransactions().Count}");
    Console.WriteLine($"  Timestamp: {block.getTimestamp():yyyy-MM-dd HH:mm:ss}");
    Console.WriteLine();
}

Console.WriteLine("═══════════════════════════════════════");
Console.WriteLine("Test completed successfully!");