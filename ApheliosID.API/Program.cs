using ApheliosID.Core.Interfaces;
using ApheliosID.Core.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configurar Swagger
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

    // Agregar JWT a Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// ✅ Configurar JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "your-super-secret-key-min-32-chars!")
        )
    };
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

// ✅ Registrar IdentityService
builder.Services.AddSingleton<IdentityService>(provider =>
{
    var logger = provider.GetRequiredService<ILogger<IdentityService>>();
    logger.LogInformation("🆔 Initializing Identity Service...");
    
    var cryptoService = provider.GetRequiredService<CryptoService>();
    var blockchainService = provider.GetRequiredService<BlockchainService>();
    
    var service = new IdentityService(cryptoService, blockchainService);
    
    logger.LogInformation("✅ Identity Service initialized");
    
    return service;
});

// ✅ Registrar AuthService
builder.Services.AddSingleton<AuthService>(provider =>
{
    var logger = provider.GetRequiredService<ILogger<AuthService>>();
    logger.LogInformation("🔐 Initializing Auth Service...");
    
    var cryptoService = provider.GetRequiredService<CryptoService>();
    var identityService = provider.GetRequiredService<IdentityService>();
    
    var service = new AuthService(cryptoService, identityService);
    
    logger.LogInformation("✅ Auth Service initialized");
    
    return service;
});

// ✅ Registrar CredentialService
builder.Services.AddSingleton<CredentialService>(provider =>
{
    var logger = provider.GetRequiredService<ILogger<CredentialService>>();
    logger.LogInformation("🎓 Initializing Credential Service...");
    
    var cryptoService = provider.GetRequiredService<CryptoService>();
    var identityService = provider.GetRequiredService<IdentityService>();
    var blockchainService = provider.GetRequiredService<BlockchainService>();
    
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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();