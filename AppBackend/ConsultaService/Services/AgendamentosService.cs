
using ConsultaService.Models;
using ConsultaService.Connection;

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

            _sql = $"select * from {NOME_TABELA}";

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
    }
}
