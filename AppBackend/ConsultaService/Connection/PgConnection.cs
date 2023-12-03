using Npgsql;

namespace ConsultaService.Connection {
    public class PgConnection {

        private const string CONNECTION_STRING = "Server=127.0.0.1;Port=5435;Username=postgres;Password=admin;Database=ConsultaServiceDB";

        public readonly NpgsqlDataSource dataSource;

        public PgConnection() {
            dataSource = NpgsqlDataSource.Create(CONNECTION_STRING);
        }

        public async Task<NpgsqlConnection> Open() {
            return await dataSource.OpenConnectionAsync();
        }

    }
}
