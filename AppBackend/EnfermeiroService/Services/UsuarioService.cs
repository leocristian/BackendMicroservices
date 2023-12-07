using EnfermeiroService.Models;
using EnfermeiroService.Connection;
using Npgsql;

namespace EnfermeiroService.Services {
    public class UsuarioService {
        
        private readonly string NOME_TABELA = "usuarios";
        private readonly PgConnection connection;

        public UsuarioService() {
            connection = new();
        }
        public async Task SignUp(Usuario usuario) {
            string _sql = $"insert into {NOME_TABELA} (cpf, telefone, nome_completo, coren, data_nascimento, login, senha) " +
                           "values ($1, $2, $3, $4, $5, %6, $7)";

            try {
                await using NpgsqlCommand command = new(_sql, await connection.Open()) {
                    Parameters = {
                        new() { Value = usuario.Cpf },
                        new() { Value = usuario.Telefone },
                        new() { Value = usuario.NomeCompleto },
                        new() { Value = usuario.Coren },
                        new() { Value = usuario.DataNascimento },
                        new() { Value = usuario.Login },
                        new() { Value = usuario.Senha },
                    }
                };
            } catch (NpgsqlException e) {
                throw new NpgsqlException(e.Message);
            }
        }
    }
}