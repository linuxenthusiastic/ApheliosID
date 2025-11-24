using ApheliosID.Core;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ApheliosID Blockchain API",
        Version = "v1",
        Description = "API REST para interactuar con la blockchain ApheliosID",
        Contact = new OpenApiContact
        {
            Name = "ApheliosID Team"
        }
    });
});

builder.Services.AddSingleton<Blockchain>(provider =>
{
    var logger = provider.GetRequiredService<ILogger<Blockchain>>();
    logger.LogInformation("Initializing ApheliosID Blockchain...");
    
    var blockchain = new Blockchain(transactionsPerBlock: 5);
    
    logger.LogInformation("Blockchain initialized with {Count} blocks", blockchain.GetChain().Count);
    
    return blockchain;
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