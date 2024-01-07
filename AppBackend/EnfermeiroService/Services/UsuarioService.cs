using EnfermeiroService.Models;
using EnfermeiroService.Connection;
using Npgsql;

namespace EnfermeiroService.Services {
    public class UsuarioService {
        
        private readonly string NOME_TABELA = "enfermeiros";
        private readonly PgConnection connection;
        private readonly string semente;

        public UsuarioService(PgConnection pgConnection, IConfiguration config) {
            connection = pgConnection;
            semente = config["DataBase:Semente"]!;
        }

        public async Task<List<Usuario>> GetAll() {
            List<Usuario> usuarios = [];
            
            string _sql = $"select id, nome_completo, cpf, telefone, coren, nome_login, grupo from {NOME_TABELA}";

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
                        "",
                        result.GetFieldValue<int>(6)
                    ));
                }

            } catch(NpgsqlException e) {
                throw new NpgsqlException(e.Message);
            }

            return usuarios;
        }

        public async Task<Usuario?> ReadByLoginInfo(string username, string senha) {
            Usuario? usuario;
            string _sql = $"select id, nome_completo, cpf, telefone, coren, nome_login, grupo "+
                          $"from enfermeiros where nome_login='{username}' and senha = crypt('{senha+semente}', senha)";

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

        public async Task<Usuario?> ReadByLogin(string username) {
            Usuario? usuario;
            string _sql = $"select id, nome_completo, cpf, telefone, coren, nome_login, grupo "+
                          $"from enfermeiros where nome_login='{username}'";

            Console.WriteLine(_sql);
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
                Console.WriteLine("Inseriu Novo Usuário!");

            } catch (NpgsqlException e) {
                throw new NpgsqlException(e.Message);
            }
        }

        public async Task<Usuario?> FindById(int id) {
            Usuario? usuario;

            string _sql = $"select id, nome_completo, cpf, telefone, coren, nome_login, grupo from {NOME_TABELA} where id = {id}";

            await using NpgsqlCommand command   = connection.dataSource.CreateCommand(_sql);
            await using NpgsqlDataReader result = await command.ExecuteReaderAsync();

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
            
            return usuario;
        }

        public async Task Delete(int id) {
            string _sql = $"delete from {NOME_TABELA} where id=@p1";

            try {
                await using NpgsqlCommand command = new(_sql, await connection.Open()) {
                    Parameters = {
                        new("p1", id)
                    }
                };

                await command.ExecuteNonQueryAsync();

                Console.WriteLine("Deletou Usuário!");
                
            } catch(NpgsqlException e) {
                throw new NpgsqlException(e.Message);
            }     
        }
        
        public async Task Update(Usuario usuario) {
            string _sql = $"update {NOME_TABELA} set nome_completo=@p1, cpf=@p2, telefone=@p3, coren=@p4, nome_login=@p5, grupo=@p6 " +
                          "where id=@p7";

            try {
                await using NpgsqlCommand command = new(_sql, await connection.Open()) {
                    Parameters = {
                        new("p1", usuario.NomeCompleto ),
                        new("p2", usuario.Cpf          ),
                        new("p3", usuario.Telefone     ),
                        new("p4", usuario.Coren        ),
                        new("p5", usuario.Login        ),
                        new("p6", usuario.Grupo        ),
                        new("p7", usuario.Id           )
                    }
                };

                await command.ExecuteNonQueryAsync();

                Console.WriteLine("Atualizou Usuário!");
                
            } catch(NpgsqlException e) {
                throw new NpgsqlException(e.Message);
            } 
        }
    }
}