﻿using ConsultaService.Models;
using ConsultaService.Connection;
using Npgsql;

namespace ConsultaService.Services {
    public class PacientesService {

        private readonly string NOME_TABELA = "pacientes";

        private readonly PgConnection connection;

        public PacientesService(PgConnection pgConnection) {
            connection = pgConnection;
        }
    
        public async Task<List<Paciente>> GetAll() {
            List<Paciente> pacientes = new();
            
            string _sql = $"select * from {NOME_TABELA}";

            try {
             
                await using NpgsqlCommand command   = connection.dataSource.CreateCommand(_sql);
                await using NpgsqlDataReader result = await command.ExecuteReaderAsync();

                while (await result.ReadAsync()) {
                    pacientes.Add(new Paciente(
                        result.GetFieldValue<int>(0),
                        result.GetFieldValue<string>(1),
                        result.GetFieldValue<string>(2),
                        result.GetFieldValue<string>(3),
                        result.GetFieldValue<DateTime>(4),
                        result.GetFieldValue<string>(5),
                        result.GetFieldValue<string>(6),
                        result.GetFieldValue<string>(7)
                    ));
                }

            } catch(NpgsqlException e) {
                throw new NpgsqlException(e.Message);
            }

            return pacientes;
        }

        public async Task Insert(Paciente paciente) { 

            string _sql = $"insert into {NOME_TABELA}(nome_completo, email, telefone, data_nascimento, cpf, endereco, numero_sus) " +
                          "values ($1, $2, $3, $4, $5, $6, $7)";

            try {
                
                await using NpgsqlCommand command = new NpgsqlCommand(_sql, await connection.Open()) {
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

                Console.WriteLine("Inseriu novo paciente!");
                
            } catch(NpgsqlException e) {
                throw new NpgsqlException(e.Message);
            }
        }

        public async Task<Paciente?> FindById(int id) {

            Paciente? paciente;

            string _sql = $"select * from {NOME_TABELA} where id = {id}";

            await using NpgsqlCommand command = connection.dataSource.CreateCommand(_sql);
            await using NpgsqlDataReader result = await command.ExecuteReaderAsync();

            if (await result.ReadAsync()) {
                paciente = new Paciente(
                    result.GetFieldValue<int>(0),
                    result.GetFieldValue<string>(1),
                    result.GetFieldValue<string>(2),
                    result.GetFieldValue<string>(3),
                    result.GetFieldValue<DateTime>(4),
                    result.GetFieldValue<string>(5),
                    result.GetFieldValue<string>(6),
                    result.GetFieldValue<string>(7)
                );
            } else {
                paciente = null;
            }
            
            return paciente;
        }

        public async Task<Paciente?> FindByCpf(string cpf) {

            Paciente? paciente;

            string _sql = $"select * from {NOME_TABELA} where cpf = '{cpf}' ";

            await using NpgsqlCommand command = connection.dataSource.CreateCommand(_sql);
            await using NpgsqlDataReader result = await command.ExecuteReaderAsync();

            if (await result.ReadAsync()) {
                paciente = new Paciente(
                    result.GetFieldValue<int>(0),
                    result.GetFieldValue<string>(1),
                    result.GetFieldValue<string>(2),
                    result.GetFieldValue<string>(3),
                    result.GetFieldValue<DateTime>(4),
                    result.GetFieldValue<string>(5),
                    result.GetFieldValue<string>(6),
                    result.GetFieldValue<string>(7)
                );
            } else {
                paciente = null;
            }
            
            return paciente;
        }

        public async Task Update(Paciente paciente) { 

            string _sql = $"update {NOME_TABELA} set nome_completo=$1, email=$2, telefone=$3, data_nascimento=$4, cpf=$5, endereco=$6, numero_sus=$7 " +
                          "where id=$8";

            try {
                await using NpgsqlCommand command = new(_sql, await connection.Open()) {
                    Parameters = {
                        new() { Value = paciente.NomeCompleto   },
                        new() { Value = paciente.Email          },
                        new() { Value = paciente.Telefone       },
                        new() { Value = paciente.DataNascimento },
                        new() { Value = paciente.Cpf            },
                        new() { Value = paciente.Endereco       },
                        new() { Value = paciente.NumeroSus      },
                        new() { Value = paciente.Id             }
                    }
                };

                await command.ExecuteNonQueryAsync();

                Console.WriteLine("Atualizou paciente!");
                
            } catch(NpgsqlException e) {
                throw new NpgsqlException(e.Message);
            }
        }

        public async Task Delete(int id) {
            string _sql = $"delete from {NOME_TABELA} where id=$1";

            try {
                await using NpgsqlCommand command = new NpgsqlCommand(_sql, await connection.Open()) {
                    Parameters = {
                        new() { Value = id }
                    }
                };

                await command.ExecuteNonQueryAsync();

                Console.WriteLine("Deletou Paciente!");
                
            } catch(NpgsqlException e) {
                throw new NpgsqlException(e.Message);
            }            
        }

        public async Task DeleteAgendamentosFromPaciente(int idPaciente) {
            
            string _sql = "delete from agendamentos where id_paciente=@p1";

            try {
                await using NpgsqlCommand command = new NpgsqlCommand(_sql, await connection.Open()) {
                Parameters = {
                        new("p1", idPaciente)
                }
            };

            await command.ExecuteNonQueryAsync();

            Console.WriteLine($"Deletou agendamentos do paciente {idPaciente}");

            } catch(NpgsqlException e) {
                throw new NpgsqlException(e.Message);
            }
        }
	}
}
