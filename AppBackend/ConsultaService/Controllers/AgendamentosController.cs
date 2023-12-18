using Microsoft.AspNetCore.Mvc;
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
    }
}
