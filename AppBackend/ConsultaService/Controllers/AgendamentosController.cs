using Microsoft.AspNetCore.Mvc;
using ConsultaService.Services;
using ConsultaService.Models;

namespace ConsultaService.Controllers {

    [ApiController]
    public class AgendamentosController : ControllerBase {

        public AgendamentosService agendamentosService = new AgendamentosService();

        [HttpGet]
        [Route("pacientes/{id}/agendamentos")]
        public async Task<IActionResult> GetAllFromPaciente(int id) {

            List<Agendamento> agendamentos = await agendamentosService.GetAllFromPaciente(id); 

            return Ok(agendamentos);
        }

        [HttpGet]
        [Route("pacientes/{idPaciente}/agendamentos/{idAgendamento}")]
        public async Task<IActionResult> GetById(int idPaciente, int idAgendamento) {
            try {

                Agendamento? agendamento = await agendamentosService.GetById(idPaciente, idAgendamento);

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
                await agendamentosService.Insert(agendamento);
                return Ok();
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return Problem("Erro interno no servidor", null, 500);
            }
        }
    }
}
