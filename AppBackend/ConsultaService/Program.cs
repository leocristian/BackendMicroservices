using ConsultaService.Connection;
using ConsultaService.Services;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Ler string de conexão 
string ConnectionString = builder.Configuration["DataBase:ConnString"] 
?? throw new Exception("DataBase:ConnString não configurada!");

builder.Services.AddSingleton(new PgConnection(ConnectionString));

builder.Services.AddSingleton(new NpgsqlConnection(ConnectionString));

builder.Services.AddSingleton(ConnectionString);

builder.Services.AddScoped<AgendamentosService>();
builder.Services.AddScoped<PacientesService>();

// Métodos para registro de dependências
// AddSingleton - Cria uma única instância para toda a aplicação.
// AddScoped    - Cria uma nova insância a cada requisição, instância fica disponível somente dentro do escopo da solicitação
// AddTransient - Cria uma nova instância toda vez que o objeto é referenciado

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
