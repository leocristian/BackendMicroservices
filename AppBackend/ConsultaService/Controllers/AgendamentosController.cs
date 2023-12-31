﻿using Microsoft.AspNetCore.Mvc;
using ConsultaService.Services;
using ConsultaService.Models;

namespace ConsultaService.Controllers {

    [ApiController]
    public class AgendamentosController : ControllerBase {

        public AgendamentosService _agendamentoService;

        public AgendamentosController(AgendamentosService agendamentoService) {
            _agendamentoService = agendamentoService;
        }

        [HttpGet]
        [Route("pacientes/{id}/agendamentos")]
        public async Task<IActionResult> GetAllFromPaciente(int id) {

            List<Agendamento> agendamentos = await _agendamentoService.GetAllFromPaciente(id); 
            return Ok(agendamentos);
        }

        [HttpGet]
        [Route("pacientes/{idPaciente}/agendamentos/{idAgendamento}")]
        public async Task<IActionResult> GetById(int idPaciente, int idAgendamento) {
            try {

                Agendamento? agendamento = await _agendamentoService.GetById(idPaciente, idAgendamento);

                if (agendamento is not null) {
                    return Ok(agendamento);
                } else {
                    return NotFound();
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return Problem("Erro interno no servidor", null, 500);
            }
        }

        [HttpPost]
        [Route("pacientes/{id}/agendamentos/novo")]
        public async Task<IActionResult> Post(Agendamento agendamento, int id) {

            try {
                agendamento.IdPaciente = id;
                await _agendamentoService.Insert(agendamento);
                return Ok("Agendamento realizado com sucesso!");
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return Problem("Erro interno no servidor", null, 500);
            }
        }

        [HttpPut]
        [Route("pacientes/{idPaciente}/agendamentos/{idAgendamento}")]
        public async Task<IActionResult> Put(int idAgendamento, Agendamento agendamento) {

            try {
                if (idAgendamento != agendamento.Id) {
                    return BadRequest("Requisição Inválida!");
                } else {
                    await _agendamentoService.Update(agendamento);
                    return Ok("Agendamento alterado com sucesso!");
                }
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                return Problem("Erro interno no servidor", null, 500);
            }
        }

        [HttpDelete]
        [Route("pacientes/{idPaciente}/agendamentos/{idAgendamento}")]
        public async Task<IActionResult> Delete(int idPaciente, int idAgendamento) {

            try {
                if (await _agendamentoService.GetById(idPaciente, idAgendamento) is not null) {
                    await _agendamentoService.Delete(idPaciente, idAgendamento);
                    return Ok("Agendamento Excluído com Sucesso!");
                } else {
                    return NotFound("Agendamento não encontrado!");
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return Problem("Erro Interno no servidor", null, 500);
            }
        }
    }
}
