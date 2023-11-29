using ConsultaService.Models;
using ConsultaService.Connection;
using Npgsql;

namespace ConsultaService.Services {
    public class PacientesService {

        private string NOME_TABELA = "pacientes";

        private PgConnection connection;

        public PacientesService() {
            connection = new PgConnection();
        }

        public async Task<List<Paciente>> GetAll() {
            List<Paciente> pacientes = new List<Paciente>();
            string _sql = $"select * from {NOME_TABELA}";

            try {
                await using var command = connection.dataSource.CreateCommand(_sql);
                await using var result = await command.ExecuteReaderAsync();

                while (await result.ReadAsync()) {
                    pacientes.Add(new Paciente(
                        result.GetFieldValue<int>(0),
                        result.GetFieldValue<String>(1),
                        result.GetFieldValue<String>(2),
                        result.GetFieldValue<String>(3),
                        result.GetFieldValue<DateOnly>(4),
                        result.GetFieldValue<String>(5),
                        result.GetFieldValue<String>(6),
                        result.GetFieldValue<String>(7)
                    ));
                }

            } catch(Exception e) {
                Console.WriteLine(e.Message);
            }

            return pacientes;
        }

        public async Task Insert(Paciente paciente) { 

            string _sql = $"insert into {NOME_TABELA}(nome_completo, email, telefone, data_nascimento, cpf, endereco, numero_sus) " +
                          "values ($1, $2, $3, $4, $5, $6, $7)";

            try {
                await using var command = new NpgsqlCommand(_sql, await connection.dataSource.OpenConnectionAsync()) {
                    Parameters = {
                        new() { Value = paciente.NomeCompleto },
                        new() { Value = paciente.Email },
                        new() { Value = paciente.Telefone },
                        new() { Value = paciente.DataNascimento },
                        new() { Value = paciente.Cpf },
                        new() { Value = paciente.Endereco },
                        new() { Value = paciente.NumeroSus },
                    }
                };

                await command.ExecuteNonQueryAsync();
                
            } catch(Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        public async Task<Paciente?> FindById(int id) {

            Paciente? paciente;

            string _sql = $"select * from {NOME_TABELA} where id = {id}";

            await using var command = connection.dataSource.CreateCommand(_sql);
            await using var result = await command.ExecuteReaderAsync();

            if (await result.ReadAsync()) {
                paciente = new Paciente(
                    result.GetFieldValue<int>(0),
                    result.GetFieldValue<String>(1),
                    result.GetFieldValue<String>(2),
                    result.GetFieldValue<String>(3),
                    result.GetFieldValue<DateOnly>(4),
                    result.GetFieldValue<String>(5),
                    result.GetFieldValue<String>(6),
                    result.GetFieldValue<String>(7)
                );
            } else {
                paciente = null;
            }
            
            return paciente;
        }

        public async Task<Paciente?> FindByCpf(string cpf) {

            Paciente? paciente;

            string _sql = $"select * from {NOME_TABELA} where cpf = '{cpf}' ";

            await using var command = connection.dataSource.CreateCommand(_sql);
            await using var result = await command.ExecuteReaderAsync();

            if (await result.ReadAsync()) {
                paciente = new Paciente(
                    result.GetFieldValue<int>(0),
                    result.GetFieldValue<String>(1),
                    result.GetFieldValue<String>(2),
                    result.GetFieldValue<String>(3),
                    result.GetFieldValue<DateOnly>(4),
                    result.GetFieldValue<String>(5),
                    result.GetFieldValue<String>(6),
                    result.GetFieldValue<String>(7)
                );
            } else {
                paciente = null;
            }
            
            return paciente;
        }

    }
}
