using Npgsql;
using dotenv.net;

namespace ConsultaService.Connection {
    public class PgConnection {

        public readonly NpgsqlDataSource dataSource;

        public PgConnection() {

            string CONNECTION_STRING; 
            IDictionary<string, string> variaveis = DotEnv.Read();
          
            CONNECTION_STRING = variaveis["CONNECTION_STRING_POSTGRESQL"];
        
            dataSource = NpgsqlDataSource.Create(CONNECTION_STRING);
        }

        public async Task<NpgsqlConnection> Open() {
            return await dataSource.OpenConnectionAsync();
        }
    }
}
