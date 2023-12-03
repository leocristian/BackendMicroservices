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
        [Route("pacientes/{id}")]
        public async Task<IActionResult> GetById(int id) {
            Paciente? paciente;

            paciente = await pacientesService.FindById(id);

            if (paciente is null) {
                return NotFound();
            } else {
                return Ok(paciente);
            }            
        }

        [HttpPost]
        [Route("pacientes/novo")]
        public async Task<IActionResult> Post(Paciente paciente) {

            try { 
                await pacientesService.Insert(paciente);
                return Ok();
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return Problem("Erro interno no servidor", null, 500);
            }            
        }
    }
}
