using Microsoft.AspNetCore.Mvc;
using ConsultaService.Services;
using ConsultaService.Models;

namespace ConsultaService.Controllers {

    [ApiController]
    public class PacientesController : ControllerBase {

        public PacientesService _pacientesService;

        public PacientesController(PacientesService pacientesService) {
            _pacientesService = pacientesService;
        }

        [HttpGet]
        [Route("pacientes")]
        public async Task<IActionResult> Get() {
            try {
                IEnumerable<Paciente> pacientes = await _pacientesService.GetAll();
                return Ok(pacientes);
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                return Problem("Erro interno no servidor", null, 500);
            }
        }

        [HttpGet]
        [Route("pacientes/{id}")]
        public async Task<IActionResult> GetById(int id) {
            Paciente? paciente = await _pacientesService.FindById(id);;

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
                await _pacientesService.Insert(paciente);
                return Ok();
            } catch (Exception e) {             
                Console.WriteLine(e.Message);
                return Problem("Erro interno no servidor", null, 500);
            }            
        }

        [HttpPut]
        [Route("pacientes/{id}")]
        public async Task<IActionResult> Put(int id, Paciente paciente) {
            try {

                paciente.Id = id;

                if (await _pacientesService.FindById(id) is not null) {
                   
                    await _pacientesService.Update(paciente);
                    Paciente? pacienteUpdated = await _pacientesService.FindById(id)!;

                    return Ok(pacienteUpdated);
                
                } else {
                    return NotFound();
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return Problem("Erro interno no servidor", null, 500);
            }
        }

        [HttpDelete]
        [Route("pacientes/{id}")]
        public async Task<IActionResult> Delete(int id) {
            try {
                if (await _pacientesService.FindById(id) is not null) {
			
		            await _pacientesService.DeleteAgendamentosFromPaciente(id);	
                    await _pacientesService.Delete(id);

                    return Ok();
                } else {
                    return NotFound();
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return Problem("Erro interno no servidor", null, 500);
            }
        }
    }
}
