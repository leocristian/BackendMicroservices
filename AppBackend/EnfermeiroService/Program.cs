using EnfermeiroService.Connection;
using EnfermeiroService.Services;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options => {
    options.AddPolicy(
        name: MyAllowSpecificOrigins,
        policy => {
            policy.WithOrigins("http://ec2-54-90-88-53.compute-1.amazonaws.com");
        }
    );
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection")
?? throw new Exception("DefaultConnection não configurada!");

builder.Services.AddSingleton(new NpgsqlConnection(ConnectionString));

builder.Services.AddScoped<UsuarioService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
