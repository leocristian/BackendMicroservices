
using ConsultaService.Models;
using ConsultaService.Connection;
using Npgsql;

namespace ConsultaService.Services {
    public class AgendamentosService {

        private string NOME_TABELA = "agendamentos";

        private PgConnection connection;

        public AgendamentosService() {
            connection = new PgConnection();
        }

        public async Task<List<Agendamento>> GetAllFromPaciente(int idPaciente) {

            List<Agendamento> agendamentos = new List<Agendamento>();

            string _sql = "";

            _sql = $"select * from {NOME_TABELA} where idpaciente={idPaciente}";

            for (int i=0;i<10;i++) {
               agendamentos.Add(new Agendamento(1,1,1,"1234", DateTime.Now, "1234", "1234"));
            }

            await Task.Delay(500);

            return agendamentos;
        }

        public async Task<List<Agendamento>> GetById(int idPaciente, int idAgendamento) {

            List<Agendamento> agendamentos = new List<Agendamento>();

            string _sql = "";

            _sql = $"select * from {NOME_TABELA}";

            for (int i=0;i<10;i++) {
               agendamentos.Add(new Agendamento(1,1,1,"1234", DateTime.Now, "1234", "1234"));
            }

            await Task.Delay(500);

            return agendamentos;
        }

        public async Task Insert(Agendamento agendamento) {

            string _sql = $"insert into {NOME_TABELA}(id_paciente, id_enfermeiro, descricao, data_hora, local, observacoes) " +
                          "values ($1, $2, $3, $4, $5, $6)";

            try {
                await using var command = new NpgsqlCommand(_sql, await connection.dataSource.OpenConnectionAsync()) {
                    Parameters = {
                        new() { Value = agendamento.IdPaciente },
                        new() { Value = agendamento.IdEnfermeiro },
                        new() { Value = agendamento.Descricao },
                        new() { Value = agendamento.DataHora },
                        new() { Value = agendamento.Local },
                        new() { Value = agendamento.Observacoes }
                    }
                };

                await command.ExecuteNonQueryAsync();
                
            } catch(NpgsqlException e) {
                throw new NpgsqlException(e.Message);
            }
        }
    }
}
