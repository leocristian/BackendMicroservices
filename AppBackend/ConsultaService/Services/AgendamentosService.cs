
using ConsultaService.Models;
using ConsultaService.Connection;
using Npgsql;
using Microsoft.Extensions.DependencyInjection;

namespace ConsultaService.Services {
    public class AgendamentosService {

        private readonly string NOME_TABELA = "agendamentos";

        private readonly PgConnection connection;

        public AgendamentosService(PgConnection pgConnection) {
            connection = pgConnection;
        }

        public async Task<List<Agendamento>> GetAllFromPaciente(int idPaciente) {

            List<Agendamento> agendamentos = new ();

            string _sql = "";

            _sql = $"select id, id_paciente, id_enfermeiro, descricao, data_consulta, hora_consulta, id_local, observacoes "+
                   $"from {NOME_TABELA} where id_paciente={idPaciente}";

            try {
                
                await using var command = connection.dataSource.CreateCommand(_sql);
                await using var result  = await command.ExecuteReaderAsync();  

                while (await result.ReadAsync()) {
                    agendamentos.Add(new Agendamento(
                        result.GetFieldValue<int>(0),
                        result.GetFieldValue<int>(1),
                        result.GetFieldValue<int>(2),
                        result.GetFieldValue<string>(3),
                        result.GetFieldValue<DateOnly>(4),
                        result.GetFieldValue<TimeOnly>(5),
                        result.GetFieldValue<int>(6),
                        result.GetFieldValue<string>(7)                       
                    ));
                }

            } catch (NpgsqlException e) {
                throw new NpgsqlException(e.Message);
            }

            return agendamentos;
        }

        public async Task<Agendamento?> GetById(int idPaciente, int idAgendamento) {

            Agendamento? agendamento;

            string _sql = "";

            _sql = $"select id, id_paciente, id_enfermeiro, descricao, data_consulta, hora_consulta, id_local, observacoes "+
                   $"from {NOME_TABELA} where id={idAgendamento} and id_paciente={idPaciente}";

            try {
                
                await using NpgsqlCommand command = connection.dataSource.CreateCommand(_sql);
                await using NpgsqlDataReader result  = await command.ExecuteReaderAsync();

                if (await result.ReadAsync()) {
                    agendamento = new Agendamento(
                        result.GetFieldValue<int>(0),
                        result.GetFieldValue<int>(1),
                        result.GetFieldValue<int>(2),
                        result.GetFieldValue<string>(3),
                        result.GetFieldValue<DateOnly>(4),
                        result.GetFieldValue<TimeOnly>(5),
                        result.GetFieldValue<int>(6),
                        result.GetFieldValue<string>(7)

                    );
                } else {
                    agendamento = null;
                }                
            } catch(NpgsqlException e) {
                throw new NpgsqlException(e.Message);
            }

            return agendamento;
        }

        public async Task Insert(Agendamento agendamento) {

            string _sql = $"insert into {NOME_TABELA}(id_paciente, id_enfermeiro, descricao, data_consulta, hora_consulta, id_local, observacoes) " +
                          "values ($1, $2, $3, $4, $5, $6, $7)";

            try {
                await using NpgsqlCommand command = new NpgsqlCommand(_sql, await connection.Open()) {
                    Parameters = {
                        new() { Value = agendamento.IdPaciente   },
                        new() { Value = agendamento.IdEnfermeiro },
                        new() { Value = agendamento.Descricao    },
                        new() { Value = agendamento.Data         },
                        new() { Value = agendamento.Hora         },
                        new() { Value = agendamento.IdLocal      },
                        new() { Value = agendamento.Observacoes  }
                    }
                };

                await command.ExecuteNonQueryAsync();
                
            } catch(NpgsqlException e) {
                throw new NpgsqlException(e.Message);
            }
        }

        public async Task Update(Agendamento agendamento) {
            string _sql = $"update {NOME_TABELA} set descricao=$1, data_hora=$2, id_local=$3, observacoes=$4 " +
                          "where id_paciente=$5 and id=$6";

            try {
                await using NpgsqlCommand command = new(_sql, await connection.Open()) {
                    Parameters = {
                        new() { Value = agendamento.Descricao   },
                        new() { Value = agendamento.Data        },
                        new() { Value = agendamento.Hora        },
                        new() { Value = agendamento.IdLocal     },
                        new() { Value = agendamento.Observacoes },
                        new() { Value = agendamento.IdPaciente  },
                        new() { Value = agendamento.Id          }
                    }
                };

                Console.WriteLine(_sql);

                await command.ExecuteNonQueryAsync();
                
            } catch(NpgsqlException e) {
                throw new NpgsqlException(e.Message);
            } 
        }

        public async Task Delete(int idPaciente, int idAgendamento) {
            string _sql = $"delete from {NOME_TABELA} where id=$1 and id_paciente=$2";

            try {
                await using NpgsqlCommand command = new(_sql, await connection.Open()) {
                    Parameters = {
                        new() { Value = idAgendamento },
                        new() { Value = idPaciente    }
                    }
                };

                await command.ExecuteNonQueryAsync();
                
            } catch(NpgsqlException e) {
                throw new NpgsqlException(e.Message);
            } 
        }
    }
}
