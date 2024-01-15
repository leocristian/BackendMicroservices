
using ConsultaService.Models;
using ConsultaService.Connection;
using Npgsql;
using Dapper;

namespace ConsultaService.Services {
    public class AgendamentosService {

        private readonly string NOME_TABELA = "agendamentos";

        private readonly PgConnection connection;

        private readonly NpgsqlConnection conn;

        public AgendamentosService(PgConnection pgConnection, NpgsqlConnection Conn) {
            connection = pgConnection;
            conn = Conn;
        }

        public async Task<IEnumerable<Agendamento>> GetAllFromPaciente(int idPaciente) {

            IEnumerable<Agendamento> agendamentos = [];

            string _sql = "select id, id_paciente, id_enfermeiro, descricao, data_consulta, hora_consulta, id_local, observacoes "+
                          $"from {NOME_TABELA} where id_paciente=@idPaciente";
            
            try {
                
                await conn.OpenAsync();
                agendamentos = await conn.QueryAsync<Agendamento>(_sql, new { idPaciente });
                await conn.CloseAsync();

            } catch (NpgsqlException e) {
                throw new NpgsqlException(e.Message);
            }

            return agendamentos;
        }

        public async Task<Agendamento?> GetById(int idPaciente, int idAgendamento) {

            Agendamento? agendamento;

            string _sql = "select id, id_paciente, id_enfermeiro, descricao, data_consulta, hora_consulta, id_local, observacoes "+
                          $"from {NOME_TABELA} where id=@idAgendamento and id_paciente=@idPaciente";

            try {

                await conn.OpenAsync();
                agendamento = await conn.QuerySingleOrDefaultAsync<Agendamento?>(_sql, new { idAgendamento, idPaciente });
                await conn.CloseAsync();
                
            } catch(NpgsqlException e) {
                throw new NpgsqlException(e.Message);
            }

            return agendamento;
        }

        public async Task Insert(Agendamento agendamento) {

            string _sql = $"insert into {NOME_TABELA}(id_paciente, id_enfermeiro, descricao, data_consulta, hora_consulta, id_local, observacoes) " +
                          "values (@IdPaciente, @IdEnfermeiro, @Descricao, @Data, @Hora, @IdLocao, @Observacoes)";

            try {
                
                var parametros = new {
                    agendamento.IdPaciente,
                    agendamento.IdEnfermeiro,
                    agendamento.Descricao,
                    agendamento.Data,
                    agendamento.Hora,
                    agendamento.IdLocal,
                    agendamento.Observacoes
                };

                if (await conn.ExecuteAsync(_sql, parametros) > 0) {
                    Console.WriteLine("Agendamento Inserido com sucesso!");
                }
                
            } catch(NpgsqlException e) {
                throw new NpgsqlException(e.Message);
            }
        }

        public async Task Update(Agendamento agendamento) {
            string _sql = $"update {NOME_TABELA} set descricao=@Descricao, data_consulta=@Data, hora_consulta=@Hora, id_local=@IdLocal, observacoes=@Observacoes " +
                          "where id_paciente=@IdPaciente and id=@Id";

            try {

                var parametros = new {
                    agendamento.IdPaciente,
                    agendamento.IdEnfermeiro,
                    agendamento.Descricao,
                    agendamento.Data,
                    agendamento.Hora,
                    agendamento.IdLocal,
                    agendamento.Observacoes
                };

                if (await conn.ExecuteAsync(_sql, parametros) > 0) {
                    Console.WriteLine("Agendamento Atualizado com Sucesso!");
                }
                
            } catch(NpgsqlException e) {
                throw new NpgsqlException(e.Message);
            } 
        }

        public async Task Delete(int idPaciente, int idAgendamento) {
            string _sql = $"delete from {NOME_TABELA} where id=@idAgendamento and id_paciente=@iPaciente";

            try {
                
                if (await conn.ExecuteAsync(_sql, new { idPaciente, idAgendamento }) > 0) {
                    Console.WriteLine("Agendamento deletado com sucesso!");
                }
        
            } catch(NpgsqlException e) {
                throw new NpgsqlException(e.Message);
            } 
        }
    }
}
