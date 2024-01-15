using ConsultaService.Models;
using ConsultaService.Connection;
using Npgsql;
using System.Data;
using Dapper;
using Microsoft.VisualBasic;

namespace ConsultaService.Services {
    public class PacientesService {

        private readonly string NOME_TABELA = "pacientes";

        private readonly PgConnection connection;

        private readonly NpgsqlConnection conn;

        public PacientesService(PgConnection pgConnection, NpgsqlConnection Conn) {
            connection = pgConnection;
            conn = Conn;
        }
    
        public async Task<IEnumerable<Paciente>> GetAll() {
            IEnumerable<Paciente> pacientes;
            
            string _sql = $"select id, nomecompleto, email,telefone, datanascimento , cpf, endereco, numerosus from {NOME_TABELA}";

            try {   
        
                await conn.OpenAsync();
                pacientes = await conn.QueryAsync<Paciente>(_sql);
                await conn.CloseAsync();
                

            } catch(Exception e) {
                throw new Exception(e.Message);
            }

            return pacientes;
        }

        public async Task Insert(Paciente paciente) { 

            string _sql = $"insert into {NOME_TABELA}(nomecompleto, email,telefone, datanascimento , cpf, endereco, numerosus) " +
                          "values (@NomeCompleto, @Email, @Telefone, @DataNascimento, @Cpf, @Endereco, @NumeroSus)";
            try {

                var parametros = new {
                    paciente.NomeCompleto,
                    paciente.Email,
                    paciente.Telefone,
                    paciente.DataNascimento,
                    paciente.Cpf,
                    paciente.Endereco,
                    paciente.NumeroSus
                };

                if (await conn.ExecuteAsync(_sql, parametros) > 0) {
                    Console.WriteLine("Paciente Inserido com sucesso!");
                }
                
            } catch(NpgsqlException e) {
                throw new NpgsqlException(e.Message);
            }
        }

        public async Task<Paciente?> FindById(int id) {

            Paciente? paciente;

            string _sql = @$"select * from {NOME_TABELA} where id = @id";
            
            await conn.OpenAsync();
            paciente = await conn.QuerySingleOrDefaultAsync<Paciente?>(_sql, new { id });
            await conn.CloseAsync();
            
            return paciente;
        }

        public async Task<Paciente?> FindByCpf(string cpf) {

            Paciente? paciente;

            string _sql = $"select * from {NOME_TABELA} where cpf = @cpf ";

            await conn.OpenAsync();
            paciente = await conn.QuerySingleOrDefaultAsync<Paciente>(_sql, new { cpf });
            await conn.CloseAsync();
                        
            return paciente;
        }

        public async Task Update(Paciente paciente) { 

            string _sql = $"update {NOME_TABELA} set nomecompleto=@NomeCompleto, email=@Email, telefone=@Telefone, datanascimento=@DataNascimento, cpf=@Cpf, endereco=@Endereco, numerosus=@NumeroSus " +
                          "where id=@id";

            try {
                
                var parametros = new {
                    paciente.NomeCompleto,
                    paciente.Email,
                    paciente.Telefone,
                    paciente.DataNascimento,
                    paciente.Cpf,
                    paciente.Endereco,
                    paciente.NumeroSus
                };

                if (await conn.ExecuteAsync(_sql, parametros) > 0) {
                    Console.WriteLine("Paciente Atualizado com Sucesso!");
                }
                
            } catch(NpgsqlException e) {
                throw new NpgsqlException(e.Message);
            }
        }

        public async Task Delete(int id) {
            string _sql = $"delete from {NOME_TABELA} where id=@id";

            try {
                
                if (await conn.ExecuteAsync(_sql, new { id }) > 0) {
                    Console.WriteLine("Deletou Paciente!");
                }
                
            } catch(NpgsqlException e) {
                throw new NpgsqlException(e.Message);
            }            
        }

        public async Task DeleteAgendamentosFromPaciente(int idPaciente) {
            
            string _sql = "delete from agendamentos where id_paciente=@idPaciente";

            try {
            
                if (await conn.ExecuteAsync(_sql, new { idPaciente }) > 0) {
                    Console.WriteLine($"Deletou agendamentos do paciente {idPaciente}");
                }

            } catch(NpgsqlException e) {
                throw new NpgsqlException(e.Message);
            }
        }
	}
}
