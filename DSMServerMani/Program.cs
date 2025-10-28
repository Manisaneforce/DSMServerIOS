using DSMServerMani.ApplicationDbContext;
using DSMServerMani.Repositories;
using DSMServerMani.Repositories.Implements;
using DSMServerMani.Services;
using DSMServerMani.Services.Implements;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ✅ Load configuration files safely
builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Secrets", "secrets.json"), optional: false, reloadOnChange: true)
    .AddJsonFile("senders.json", optional: true, reloadOnChange: true); // ✅ Now optional to avoid FileNotFound crash

builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<ICheckinRepository, CheckinRepository>();
builder.Services.AddScoped<ICheckinService, CheckinService>();


var jwtSecretKey = builder.Configuration["JWTSetting:SecretKey"]
    ?? throw new ArgumentNullException("JWTSetting:SecretKey is missing.");
var defaultConnection = builder.Configuration["ConnectionStrings:DefaultConnection"]
    ?? throw new InvalidOperationException("DefaultConnection not found in secrets.json");

// ✅ Register AppDbConnection
builder.Services.AddScoped<AppDbConnection>();

// ✅ Register DbContext using AppDbConnection
builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    var dbConnection = serviceProvider.GetRequiredService<AppDbConnection>();
    options.UseSqlServer(dbConnection.ConnectionString);
});

// ✅ Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ Enable Swagger for development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
