using Npgsql;
using Dapper;
using System.Data;

namespace ConsultaService.Connection {
    public class PgConnection {

        public readonly NpgsqlDataSource dataSource;

        public PgConnection(string ConnString) {
            dataSource = NpgsqlDataSource.Create(ConnString);
            
            Console.WriteLine("Conexao criada!");
        }

        public async Task<NpgsqlConnection> Open() {
            return await dataSource.OpenConnectionAsync();
        }
    }
}

