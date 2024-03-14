﻿using ConsultaService.Models;
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
            
            string _sql = $"select * from {NOME_TABELA}";

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

            string _sql = $"insert into {NOME_TABELA} (nomecompleto, telefone, email, datanascimento, datainiciogravidez, cpf, bairro, endereco, numerosus) " +
                          "values (@NomeCompleto, @Telefone, @Email, @DataNascimento, @DataInicioGravidez, @Cpf, @Bairro, @Endereco, @NumeroSus)";
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

        public async Task DeleteConsultasFromPaciente(int idPaciente) {
            
            string _sql = "delete from consultas where idpaciente=@idPaciente";

            try {
            
                if (await conn.ExecuteAsync(_sql, new { idPaciente }) > 0) {
                    Console.WriteLine($"Deletou consultas do paciente {idPaciente}");
                }

            } catch(NpgsqlException e) {
                throw new NpgsqlException(e.Message);
            }
        }
	}
}
