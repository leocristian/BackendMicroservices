using ConsultaService.Models;
using Npgsql;
using System.Data;
using Dapper;
using Microsoft.VisualBasic;

namespace ConsultaService.Services {
    public class PacientesService {

        private readonly string NOME_TABELA = "pacientes";

        private readonly NpgsqlConnection conn;

        public PacientesService(NpgsqlConnection Conn) {
            conn = Conn;
        }
    
        public async Task<IEnumerable<Paciente>> GetAll() {
            IEnumerable<Paciente> pacientes;
            
            string _sql = $"select id, idEnfermeiro, nomeCompleto, telefone, email, to_char(dataNascimento, 'yyyy-MM-dd'), to_char(dataInicioGravidez, 'yyyy-MM-dd'), cpf, bairro, endereco, numeroSus from {NOME_TABELA}";

            try {           
                
                await conn.CloseAsync();
                await conn.OpenAsync();
                pacientes = await conn.QueryAsync<Paciente>(_sql);
                await conn.CloseAsync();
                

            } catch(Exception e) {
                throw new Exception(e.Message);
            }

            return pacientes;
        }

        public async Task Insert(Paciente paciente) { 

            string _sql = $"insert into {NOME_TABELA} (idenfermeiro, nomecompleto, telefone, email, datanascimento, datainiciogravidez, cpf, bairro, endereco, numerosus) " +
                          "values (@IdEnfermeiro, @NomeCompleto, @Telefone, @Email, to_date(@DataNascimento, 'yyyy-MM-dd'), to_date(@DataInicioGravidez, 'yyyy-MM-dd'), @Cpf, @Bairro, @Endereco, @NumeroSus)";
            try {
                
                var DataNascimento     = paciente.DataNascimento.ToString("yyyy-MM-dd");
                var DataInicioGravidez = paciente.DataInicioGravidez.ToString("yyyy-MM-dd");

                var parametros = new {
                    paciente.IdEnfermeiro,
                    paciente.NomeCompleto,
                    paciente.Telefone,
                    paciente.Email,
                    DataNascimento,
                    DataInicioGravidez,
                    paciente.Cpf,
                    paciente.Bairro,
                    paciente.Endereco,
                    paciente.NumeroSus
                };
        

                if (await conn.ExecuteAsync(_sql, parametros) > 0) {
                    Console.WriteLine("Paciente Inserido com sucesso!");
                    await conn.CloseAsync();
                }
                
            } catch(NpgsqlException e) {
                throw new NpgsqlException(e.Message);
            }
        }

        public async Task<Paciente?> FindById(int id) {

            Paciente? paciente;

            string _sql = @$"select * from {NOME_TABELA} where id = @id";
            
            await conn.CloseAsync();
            await conn.OpenAsync();
            paciente = await conn.QuerySingleOrDefaultAsync<Paciente?>(_sql, new { id });
            await conn.CloseAsync();
            
            return paciente;
        }

        public async Task<Paciente?> FindByCpf(string cpf) {

            Paciente? paciente;

            string _sql = $"select * from {NOME_TABELA} where cpf = @cpf ";

            await conn.CloseAsync();        
            await conn.OpenAsync();
            paciente = await conn.QuerySingleOrDefaultAsync<Paciente>(_sql, new { cpf });
            await conn.CloseAsync();
                        
            return paciente;
        }

        public async Task Update(Paciente paciente) { 

            string _sql = $"update {NOME_TABELA} set "+
                           "nomecompleto=@NomeCompleto, "+
                           "telefone=@Telefone, "+
                           "email=@Email, "+
                           "datanascimento=@DataNascimento, "+
                           "datainiciogravidez=@DataInicioGravidez, "+
                           "cpf=@Cpf, "+
                           "endereco=@Endereco, "+
                           "numerosus=@NumeroSus " +
                           "where id=@id";

            try {
                
                var parametros = new {
                    paciente.NomeCompleto,
                    paciente.Telefone,
                    paciente.Email,
                    paciente.DataNascimento,
                    paciente.DataInicioGravidez,
                    paciente.Cpf,
                    paciente.Bairro,
                    paciente.Endereco,
                    paciente.NumeroSus,
                    paciente.Id
                };

                if (await conn.ExecuteAsync(_sql, parametros) > 0) {
                    Console.WriteLine("Paciente Atualizado com Sucesso!");
                    await conn.CloseAsync();
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
                    await conn.CloseAsync();
                }
                
            } catch(NpgsqlException e) {
                throw new NpgsqlException(e.Message);
            }            
        }

        public async Task DeleteConsultasFromPaciente(int idPaciente) {
            
            string _sql = "delete from consultas where idpaciente=@idPaciente";

            try {
            
                if (await conn.ExecuteAsync(_sql, new { idPaciente }) > 0) {
                    Console.WriteLine($"Deletou consultas do paciente {idPaciente}");
                    await conn.CloseAsync();
                }

            } catch(NpgsqlException e) {
                throw new NpgsqlException(e.Message);
            }
        }
	}
}
