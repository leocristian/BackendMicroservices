using Microsoft.AspNetCore.Mvc;
using ConsultaService.Services;
using ConsultaService.Models;

namespace ConsultaService.Controllers {

    [ApiController]
    [Route("pacientes")]
    public class PacientesController : ControllerBase {

        public PacienteService pacienteService = new PacienteService();

        [HttpGet]
        public async Task<IActionResult> Get() {
            List<Paciente> pacientes = new List<Paciente>();

            pacientes = await pacienteService.GetAll();

            return Ok(pacientes);
        }

        [HttpGet]
        [Route("/index/{id}")]
        public async Task<IActionResult> GetById(int id) {
            Paciente? paciente;

            paciente = await pacienteService.FindById(id);

            if (paciente is null) {
                return NotFound();
            } else {
                return Ok(paciente);
            }            
        }

        [HttpGet]
        [Route("/index/{cpf}")]
        public async Task<IActionResult> GetByCpf(string cpf) {
            Paciente? paciente;

            paciente = await pacienteService.FindByCpf(cpf);

            if (paciente is null) {
                return NotFound();
            } else {
                return Ok(paciente);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Paciente paciente) {

            await pacienteService.Insert(paciente);

            return Ok();
        }
    }
}
