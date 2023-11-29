using Microsoft.AspNetCore.Mvc;
using ConsultaService.Services;
using ConsultaService.Models;

namespace ConsultaService.Controllers {

    [ApiController]
    public class AgendamentosController : ControllerBase {

        public AgendamentosService agendamentosService = new AgendamentosService();

        [HttpGet]
        [Route("pacientes/{id}/agendamentos")]
        public async Task<IActionResult> GetAllFromPaciente(int idPaciente) {

            List<Agendamento> agendamentos = new List<Agendamento>();

            agendamentos = await agendamentosService.GetAllFromPaciente(idPaciente);

            return Ok(agendamentos);
        }

        [HttpGet]
        [Route("pacientes/{idPaciente}/agendamentos/{idAgendamento}")]
        public async Task<IActionResult> GetById(int idPaciente, int idAgendamento) {

            List<Agendamento> agendamentos = new List<Agendamento>();

            agendamentos = await agendamentosService.GetById(idPaciente, idAgendamento);

            return Ok(agendamentos);
        }

        [HttpPost]
        [Route("pacientes/{idPaciente}/agendamentos/novo")]
        public async Task<IActionResult> Post(Agendamento agendamento) {

            await Task.Delay(500);

            return Ok();
        }
    }
}
