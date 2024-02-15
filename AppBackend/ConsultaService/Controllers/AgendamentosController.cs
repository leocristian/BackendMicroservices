using Microsoft.AspNetCore.Mvc;
using ConsultaService.Services;
using ConsultaService.Models;
using ConsultaService.Lib;

namespace ConsultaService.Controllers {

    [ApiController]
    public class AgendamentosController : ControllerBase {

        public AgendamentosService _agendamentoService;
        public Generics _generics;

        public AgendamentosController(AgendamentosService agendamentoService, Generics generics) {
            _agendamentoService = agendamentoService;
            _generics           = generics;
        }

        [HttpGet]
        [Route("pacientes/{id}/agendamentos")]
        public async Task<IActionResult> GetAllFromPaciente(int id) {

            try {
                IEnumerable<Agendamento> agendamentos = await _agendamentoService.GetAllFromPaciente(id); 
                return Ok(agendamentos);
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                return _generics.ErroServer;
            }
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
                return _generics.ErroServer;
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
                return _generics.ErroServer;
            }
        }

        [HttpPut]
        [Route("pacientes/{idPaciente}/agendamentos/{idAgendamento}")]
        public async Task<IActionResult> Put(int idPaciente, int idAgendamento, Agendamento agendamento) {

            try {
                if (idAgendamento != agendamento.Id) {
                    return BadRequest("Requisição Inválida!");
                } else {

                    agendamento.IdPaciente = idPaciente;
                    agendamento.Id         = idAgendamento;
                    
                    await _agendamentoService.Update(agendamento);
                    return Ok("Agendamento alterado com sucesso!");
                }
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                return _generics.ErroServer;
            }
        }

        [HttpPatch]
        [Route ("pacientes/{idPaciente}/agendamentos/{idAgendamento}")]
        public async Task<IActionResult> AtualizarStatus(int idPaciente, int idAgendamento, string novoStatus) {
            
            try { 
                
                await _agendamentoService.UpdateStatus(idPaciente, idAgendamento, novoStatus);
                return Ok("Status Alterado com Sucesso!");

            } catch(Exception e) {
                Console.WriteLine(e.Message);
                return _generics.ErroServer;
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
                return _generics.ErroServer;
            }
        }
    }
}
