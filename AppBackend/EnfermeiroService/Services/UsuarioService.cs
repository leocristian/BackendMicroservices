using EnfermeiroService.Models;
using EnfermeiroService.Connection;
using Npgsql;
using dotenv.net;

namespace EnfermeiroService.Services {
    public class UsuarioService {
        
        private readonly string NOME_TABELA = "enfermeiros";
        private readonly PgConnection connection;
        private readonly string semente;

        public UsuarioService(PgConnection pgConnection, IConfiguration config) {
            connection = pgConnection;
            semente = config["DataBase:Semente"];
        }

        public async Task<List<Usuario>> GetAll() {
            List<Usuario> usuarios = new();
            
            string _sql = $"select * from {NOME_TABELA}";

            try {
             
                await using NpgsqlCommand command   = connection.dataSource.CreateCommand(_sql);
                await using NpgsqlDataReader result = await command.ExecuteReaderAsync();

                while (await result.ReadAsync()) {
                    usuarios.Add(new Usuario(
                        result.GetFieldValue<int>(0),
                        result.GetFieldValue<string>(1),
                        result.GetFieldValue<string>(2),
                        result.GetFieldValue<string>(3),
                        result.GetFieldValue<string>(4),
                        result.GetFieldValue<string>(5),
                        result.GetFieldValue<string>(6),
                        result.GetFieldValue<int>(7)
                    ));
                }

            } catch(NpgsqlException e) {
                throw new NpgsqlException(e.Message);
            }

            return usuarios;
        }

        public async Task<Usuario?> ReadByLogin(string username, string senha) {
            Usuario? usuario;
            string _sql = $"select * from enfermeiros where nome_login='{username}' and senha = crypt('{senha+semente}', senha)";

            await using NpgsqlCommand command   = connection.dataSource.CreateCommand(_sql);
            await using NpgsqlDataReader result = await command.ExecuteReaderAsync();

            try {
                if (await result.ReadAsync()) {
                    usuario = new Usuario(
                        result.GetFieldValue<int>(0),
                        result.GetFieldValue<string>(1),
                        result.GetFieldValue<string>(2),
                        result.GetFieldValue<string>(3),
                        result.GetFieldValue<string>(4),
                        result.GetFieldValue<string>(5),
                        "",
                        result.GetFieldValue<int>(6)
                    );
                } else {
                    usuario = null;
                }
            } catch (NpgsqlException e) {
                throw new NpgsqlException(e.Message);
            }
            return usuario;
        }

        public async Task SignUp(Usuario usuario) {
            
            
            string _sql = $"insert into {NOME_TABELA} (cpf, telefone, nome_completo, coren, nome_login, senha, grupo) " +
                           "values ($1, $2, $3, $4, $5, crypt($6, gen_salt('md5')), $7)";
                                                     
            try {
                await using NpgsqlCommand command = new(_sql, await connection.Open()) {
                    Parameters = {
                        new() { Value = usuario.Cpf },
                        new() { Value = usuario.Telefone },
                        new() { Value = usuario.NomeCompleto },
                        new() { Value = usuario.Coren },
                        new() { Value = usuario.Login },
                        new() { Value = usuario.Senha+semente },
                        new() { Value = usuario.Grupo }
                    }
                };

                await command.ExecuteNonQueryAsync();

            } catch (NpgsqlException e) {
                throw new NpgsqlException(e.Message);
            }
        }
    }
}