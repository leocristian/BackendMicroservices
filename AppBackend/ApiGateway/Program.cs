using ApiGateway.Generics;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ApiGateway.Services;
using Microsoft.OpenApi.Models;
using System.ComponentModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<Constants>();

builder.Services.AddSingleton(new HttpClient() { BaseAddress = new Uri(builder.Configuration.GetConnectionString("UrlAgendamentosService")!) }); 

// string key = builder.Configuration["Settings:JwtKey"] ?? throw new Exception("Settings:JwtKey não configurada!");

string key = builder.Configuration.GetConnectionString("JwtKey") ?? throw new Exception("JwtKey não configurada!");

byte[] encodedKey = Encoding.ASCII.GetBytes(key);

builder.Services.AddSingleton(new TokenService(encodedKey));

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme             = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o => {
    o.TokenValidationParameters  = new TokenValidationParameters {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(encodedKey), 
        ValidateAudience = false,
        ValidateIssuer   = false,
        ValidateLifetime = true
    };
});

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("Enfermeiros", policy => policy.RequireClaim("Grupo", "0"))
    .AddPolicy("Medicos",     policy => policy.RequireClaim("Grupo", "1"))
    .AddPolicy("Usuarios",    policy => policy.RequireClaim("Grupo", "0", "1"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();