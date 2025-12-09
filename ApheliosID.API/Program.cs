using ApheliosID.Core.Interfaces;
using ApheliosID.Core.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ApheliosID - Blockchain API",
        Version = "v1",
        Description = "API REST para blockchain con identidades descentralizadas y credenciales verificables",
        Contact = new OpenApiContact
        {
            Name = "ApheliosID Team"
        }
    });
});


// ✅ Registrar BlockchainService como INTERFAZ
builder.Services.AddSingleton<IBlockchainService, BlockchainService>(provider =>
{
    var logger = provider.GetRequiredService<ILogger<BlockchainService>>();
    logger.LogInformation("🔗 Initializing Blockchain Service...");
    
    var service = new BlockchainService(transactionsPerBlock: 5);
    
    logger.LogInformation("✅ Blockchain initialized with {Count} blocks", service.GetChain().Count);
    
    return service;
});

// ✅ Registrar la MISMA instancia como clase concreta
builder.Services.AddSingleton<BlockchainService>(provider =>
{
    return (BlockchainService)provider.GetRequiredService<IBlockchainService>();
});


// ✅ Registrar CryptoService
builder.Services.AddSingleton<CryptoService>();

// ✅ Registrar IdentityService (SOLO UNA VEZ)
builder.Services.AddSingleton<IdentityService>(provider =>
{
    var logger = provider.GetRequiredService<ILogger<IdentityService>>();
    logger.LogInformation("🆔 Initializing Identity Service...");
    
    var cryptoService = provider.GetRequiredService<CryptoService>();
    var blockchainService = provider.GetRequiredService<BlockchainService>();  // ← AGREGAR
    
    var service = new IdentityService(cryptoService, blockchainService);  // ← AGREGAR
    
    logger.LogInformation("✅ Identity Service initialized");
    
    return service;
});

// ✅ Registrar CredentialService
builder.Services.AddSingleton<CredentialService>(provider =>
{
    var logger = provider.GetRequiredService<ILogger<CredentialService>>();
    logger.LogInformation("🎓 Initializing Credential Service...");
    
    var cryptoService = provider.GetRequiredService<CryptoService>();
    var identityService = provider.GetRequiredService<IdentityService>();
    var blockchainService = provider.GetRequiredService<BlockchainService>();  // ← AHORA SÍ EXISTE
    
    var service = new CredentialService(cryptoService, identityService, blockchainService);
    
    logger.LogInformation("✅ Credential Service initialized");
    
    return service;
});

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configurar pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "ApheliosID API v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();