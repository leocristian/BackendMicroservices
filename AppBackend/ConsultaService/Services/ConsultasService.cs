
using ConsultaService.Models;
using Npgsql;
using Dapper;

namespace ConsultaService.Services {
    public class ConsultasService {

        private readonly string NOME_TABELA = "consultas";

        private readonly NpgsqlConnection conn;

        public ConsultasService(NpgsqlConnection Conn) {
            conn = Conn;
        }

        public async Task<IEnumerable<Consulta>> GetAllFromPaciente(int idPaciente) {

            IEnumerable<Consulta> consultas = [];

            string _sql = "select id, idpaciente, idenfermeiro, descricao, dataconsulta, horaconsulta, idposto, observacoes, situacao "+
                          $"from {NOME_TABELA} where idpaciente=@idPaciente";
            
            try {
                
                await conn.OpenAsync();
                consultas = await conn.QueryAsync<Consulta>(_sql, new { idPaciente });
                await conn.CloseAsync();

            } catch (NpgsqlException e) {
                await conn.CloseAsync();
                throw new NpgsqlException(e.Message);
            }

            return consultas;
        }

        public async Task<Consulta?> GetById(int idPaciente, int idConsulta) {

            Consulta? Consulta;

            string _sql = "select id, idpaciente, idenfermeiro, descricao, dataconsulta, horaconsulta, idposto, observacoes, situacao "+
                          $"from {NOME_TABELA} where id=@idConsulta and idpaciente=@idPaciente";

            try {

                await conn.OpenAsync();
                Consulta = await conn.QuerySingleOrDefaultAsync<Consulta?>(_sql, new { idConsulta, idPaciente });
                await conn.CloseAsync();
                
            } catch(NpgsqlException e) {
                await conn.CloseAsync();
                throw new NpgsqlException(e.Message);
            }

            return Consulta;
        }

        public async Task Insert(Consulta Consulta) {

            string _sql = $"insert into {NOME_TABELA}( "+
                           "idpaciente, idenfermeiro, descricao, idposto, observacoes, situacao, datasolicitada, horasolicitada) " +
                           "values "+
                           "(@IdPaciente, @IdEnfermeiro, @Descricao, @IdPosto, @Observacoes, @Situacao, to_date(@Data, 'yyyy-MM-dd'), to_timestamp(@Hora, 'hh:mm:ss'))";

            try {
                
                var data = Consulta.DataSolicitada.ToString("yyyy-MM-dd");
                var hora = Consulta.HoraSolicitada.ToString("hh:mm:ss");

                var parametros = new {
                    Consulta.IdPaciente,
                    Consulta.IdEnfermeiro,
                    Consulta.Descricao,
                    Consulta.IdPosto,
                    Consulta.Observacoes,                    
                    Consulta.Situacao,
                    data,
                    hora                    
                };

                if (await conn.ExecuteAsync(_sql, parametros) > 0) {
                    Console.WriteLine("Consulta Inserido com sucesso!");
                }
                
            } catch(NpgsqlException e) {
                throw new NpgsqlException(e.Message);
            }
        }

        public async Task Update(Consulta Consulta) {

            string _sql = $"update {NOME_TABELA} set "+
                           "descricao=@Descricao, "+
                           "idposto=@IdPosto, "+
                           "observacoes=@Observacoes, "+
                           "situacao=@Situacao " +
                           "where idPaciente=@IdPaciente and id=@Id";

            try {

                var parametros = new {
                    Consulta.Descricao,
                    Consulta.IdPosto,
                    Consulta.Observacoes,                    
                    Consulta.Situacao,
                    Consulta.IdPaciente,
                    Consulta.Id
                };

                if (await conn.ExecuteAsync(_sql, parametros) > 0) {
                    Console.WriteLine("Consulta Atualizado com Sucesso!");
                }
                
            } catch(NpgsqlException e) {
                throw new NpgsqlException(e.Message);
            } 
        }

        public async Task Delete(int idPaciente, int idConsulta) {
            string _sql = $"delete from {NOME_TABELA} where id=@idConsulta and idpaciente=@idPaciente";

            try {
                
                if (await conn.ExecuteAsync(_sql, new { idPaciente, idConsulta }) > 0) {
                    Console.WriteLine("Consulta deletada com sucesso!");
                }
        
            } catch(NpgsqlException e) {
                throw new NpgsqlException(e.Message);
            } 
        }

        public async Task UpdateStatus(int idPaciente, int idConsulta, string novaSituacao) {
            string _sql = $"update {NOME_TABELA} set situacao=@NovaSituacao " +
                          "where idpaciente=@idPaciente and id=@idConsulta";

            try {

                var parametros = new { idPaciente, idConsulta, novaSituacao };

                if (await conn.ExecuteAsync(_sql, parametros) > 0) {
                    Console.WriteLine("Situação Atualizada com Sucesso!");
                }
                
            } catch(NpgsqlException e) {
                throw new NpgsqlException(e.Message);
            } 
        }
    }
}
