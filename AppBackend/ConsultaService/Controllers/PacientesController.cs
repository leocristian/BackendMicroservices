using Microsoft.AspNetCore.Mvc;
using ConsultaService.Services;
using ConsultaService.Models;

namespace ConsultaService.Controllers {

    [ApiController]
    public class PacientesController : ControllerBase {

        public PacientesService pacientesService = new PacientesService();

        [HttpGet]
        [Route("pacientes")]
        public async Task<IActionResult> Get() {
            List<Paciente> pacientes = new List<Paciente>();

            pacientes = await pacientesService.GetAll();

            return Ok(pacientes);
        }

        [HttpGet]
        [Route("pacientes/[action]")]
        public async Task<IActionResult> GetById(int id) {
            Paciente? paciente;

            paciente = await pacientesService.FindById(id);

            if (paciente is null) {
                return NotFound();
            } else {
                return Ok(paciente);
            }            
        }

        [HttpGet]
        [Route("pacientes/[action]")]
        public async Task<IActionResult> GetByCpf(string cpf) {
            Paciente? paciente;

            paciente = await pacientesService.FindByCpf(cpf);

            if (paciente is null) {
                return NotFound();
            } else {
                return Ok(paciente);
            }
        }

        [HttpPost]
        [Route("pacientes/novo")]
        public async Task<IActionResult> Post(Paciente paciente) {

            await pacientesService.Insert(paciente);

            return Ok();
        }
    }
}
