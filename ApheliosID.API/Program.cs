using ApheliosID.Core.Interfaces;
using ApheliosID.Core.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ApheliosID - Blockchain API",
        Version = "v1",
        Description = "API REST para blockchain con identidades descentralizadas",
        Contact = new OpenApiContact
        {
            Name = "ApheliosID Team"
        }
    });
});

builder.Services.AddSingleton<IBlockchainService, BlockchainService>(provider =>
{
    var logger = provider.GetRequiredService<ILogger<BlockchainService>>();
    logger.LogInformation("🔗 Initializing Blockchain Service...");
    
    var service = new BlockchainService(transactionsPerBlock: 5);
    
    logger.LogInformation("✅ Blockchain initialized with {Count} blocks", service.GetChain().Count);
    
    return service;
});

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