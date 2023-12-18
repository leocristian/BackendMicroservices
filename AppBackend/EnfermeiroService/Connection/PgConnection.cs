using Npgsql;

namespace EnfermeiroService.Connection {
    public class PgConnection {
        
        public readonly NpgsqlDataSource dataSource;

        public PgConnection(string ConnString) {
            dataSource = NpgsqlDataSource.Create(ConnString);
        } 

        public async Task<NpgsqlConnection> Open() {
            return await dataSource.OpenConnectionAsync();
        }
    }
}