using EnfermeiroService.Models;
using Npgsql;
using Dapper;
using EnfermeiroService.Lib;

namespace EnfermeiroService.Services {
    public class UsuarioService {
        
        private readonly string NOME_TABELA = "usuarios";
        private readonly NpgsqlConnection conn;
        private readonly string semente;
        private Generics generics;

        public UsuarioService(NpgsqlConnection Conn, IConfiguration config) {
            conn = Conn;
            semente = config.GetConnectionString("Semente")!;
            generics = new Generics();
        }

        public async Task<IEnumerable<Usuario>> GetAll() {
            IEnumerable<Usuario> usuarios;
            
            string _sql = $"select id, cpf, telefone, nomecompleto, registro, login, '' as senha, grupo from {NOME_TABELA}";

            try {
             
                await conn.OpenAsync();
                usuarios = await conn.QueryAsync<Usuario>(_sql);
                await conn.CloseAsync();

            } catch(NpgsqlException e) {
                await conn.CloseAsync();
                throw new NpgsqlException(e.Message);
            }

            return usuarios;
        }

        public async Task<Usuario?> ReadByLoginInfo(string username, string senha) {
            Usuario? usuario;
            string _sql = $"select id, nomecompleto, cpf, telefone, registro, login, grupo "+
                          $"from {NOME_TABELA} where login=@username and senha = @senhaMd5";

            await conn.OpenAsync();

            var senhaMd5 = generics.Md5(semente+senha+semente);

            usuario = await conn.QuerySingleOrDefaultAsync<Usuario>(_sql, new { username, senhaMd5 });
            await conn.CloseAsync();

            return usuario;
        }

        public async Task<Boolean> UserExists(string username) {
            Usuario? usuario;
            string _sql = $"select id, nomecompleto, cpf, telefone, registro, login, '' as senha, grupo "+
                          $"from {NOME_TABELA} where login=@username";

            await conn.OpenAsync();
            usuario = await conn.QuerySingleOrDefaultAsync<Usuario?>(_sql, new { username });
            await conn.CloseAsync();

            return (usuario is not null);
        }

        public async Task SignUp(Usuario usuario) {
            
            
            string _sql = $"insert into {NOME_TABELA} (cpf, telefone, nomecompleto, registro, login, senha, grupo) " +
                           "values (@Cpf, @Telefone, @NomeCompleto, @Registro, @Login, @Senha, @Grupo)";
                                                     
            try {
                
                usuario.Senha = generics.Md5(semente + usuario.Senha + semente)!;

                var parametros = new {
                    usuario.Cpf,
                    usuario.Telefone,
                    usuario.NomeCompleto,
                    usuario.Registro,
                    usuario.Login,
                    usuario.Senha,
                    usuario.Grupo
                };

                if (await conn.ExecuteAsync(_sql, parametros) > 0) {
                    Console.WriteLine("Usuário inserido com sucesso!");
                    await conn.CloseAsync();
                }

            } catch (NpgsqlException e) {
                throw new NpgsqlException(e.Message);
            }
        }

        public async Task<Usuario?> FindById(int id) {
            Usuario? usuario;

            string _sql = $"select id, nomecompleto, cpf, telefone, registro, login, grupo from {NOME_TABELA} where id = @id";
            
            await conn.OpenAsync();
            usuario = await conn.QuerySingleOrDefaultAsync<Usuario>(_sql, new { id });
            await conn.CloseAsync();
            
            return usuario;
        }

        public async Task Delete(int id) {
            string _sql = $"delete from {NOME_TABELA} where id=@id";

            try {
                if (await conn.ExecuteAsync(_sql, new { id }) > 0) {
                    Console.WriteLine("Deletou Usuário!");
                }

                Console.WriteLine("Deletou Usuário!");
                
            } catch(NpgsqlException e) {
                throw new NpgsqlException(e.Message);
            }     
        }
        
        public async Task Update(Usuario usuario) {
            string _sql = $"update {NOME_TABELA} set nomecompleto=@NomeCompleto, cpf=@Cpf, telefone=@Telefone, registro=@Registro, login=@Login, grupo=@Grupo " +
                          "where id=@Id";

            try {
                
                var parametros = new {
                    usuario.Cpf,
                    usuario.Telefone,
                    usuario.NomeCompleto,
                    usuario.Registro,
                    usuario.Login,
                    usuario.Grupo,
                    usuario.Id
                };

                if (await conn.ExecuteAsync(_sql, parametros) > 0) {
                    Console.WriteLine("Atualizou Usuário!");
                }
                
                
            } catch(NpgsqlException e) {
                throw new NpgsqlException(e.Message);
            } 
        }
    }
}