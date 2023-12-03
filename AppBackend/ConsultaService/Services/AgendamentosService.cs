﻿
using ConsultaService.Models;
using ConsultaService.Connection;
using Npgsql;

namespace ConsultaService.Services {
    public class AgendamentosService {

        private readonly string NOME_TABELA = "agendamentos";

        private readonly PgConnection connection;

        public AgendamentosService() {
            connection = new PgConnection();
        }

        public async Task<List<Agendamento>> GetAllFromPaciente(int idPaciente) {

            List<Agendamento> agendamentos = new ();

            string _sql = "";

            _sql = $"select * from {NOME_TABELA} where id_paciente={idPaciente}";

            try {
                await using var command = connection.dataSource.CreateCommand(_sql);
                await using var result  = await command.ExecuteReaderAsync();  

                while (await result.ReadAsync()) {
                    agendamentos.Add(new Agendamento(
                        result.GetFieldValue<int>(0),
                        result.GetFieldValue<int>(1),
                        result.GetFieldValue<int>(2),
                        result.GetFieldValue<string>(3),
                        result.GetFieldValue<DateTime>(4),
                        result.GetFieldValue<int>(5),
                        result.GetFieldValue<string>(6)                       
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

            _sql = $"select * from {NOME_TABELA} where id={idAgendamento} and id_paciente={idPaciente}";

            await using NpgsqlCommand command = connection.dataSource.CreateCommand(_sql);
            await using NpgsqlDataReader result  = await command.ExecuteReaderAsync();

            try {
                if (await result.ReadAsync()) {
                    agendamento = new Agendamento(
                        result.GetFieldValue<int>(0),
                        result.GetFieldValue<int>(1),
                        result.GetFieldValue<int>(2),
                        result.GetFieldValue<string>(3),
                        result.GetFieldValue<DateTime>(4),
                        result.GetFieldValue<int>(5),
                        result.GetFieldValue<string>(6)

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

            string _sql = $"insert into {NOME_TABELA}(id_paciente, id_enfermeiro, descricao, data_hora, id_local, observacoes) " +
                          "values ($1, $2, $3, $4, $5, $6)";

            try {
                await using NpgsqlCommand command = new NpgsqlCommand(_sql, await connection.Open()) {
                    Parameters = {
                        new() { Value = agendamento.IdPaciente   },
                        new() { Value = agendamento.IdEnfermeiro },
                        new() { Value = agendamento.Descricao    },
                        new() { Value = agendamento.DataHora     },
                        new() { Value = agendamento.IdLocal      },
                        new() { Value = agendamento.Observacoes  }
                    }
                };

                await command.ExecuteNonQueryAsync();
                
            } catch(NpgsqlException e) {
                throw new NpgsqlException(e.Message);
            }
        }
    }
}
