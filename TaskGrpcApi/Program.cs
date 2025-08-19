using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TaskGrpcApi.Services;
using Microsoft.Data.Sqlite;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// JWT config
var jwtKey = "super_secret_key_123!"; // In production, use secure storage
var keyBytes = Encoding.ASCII.GetBytes(jwtKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // For dev only
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

builder.Services.AddGrpc();

builder.Services.AddSingleton<IDbConnection>(sp =>
{
    var conn = new SqliteConnection("Data Source=tasks.db");
    conn.Open();

    // Initialize schema
    var cmd = conn.CreateCommand();
    cmd.CommandText =
    @"
    CREATE TABLE IF NOT EXISTS Tasks (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        Title TEXT NOT NULL,
        Description TEXT,
        IsCompleted INTEGER NOT NULL
    );
    ";
    cmd.ExecuteNonQuery();

    return conn;
});

builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<AuthService>();

var app = builder.Build();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapGrpcService<TaskService>().RequireAuthorization();
app.MapGrpcService<AuthService>();

app.MapGet("/", () => "Use a gRPC client to communicate with this server.");

app.Run();
