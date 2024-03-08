using Microsoft.AspNetCore.Mvc;
using ConsultaService.Services;
using ConsultaService.Lib;
using ConsultaService.Models;

namespace ConsultaService.Controllers {

    [ApiController]
    public class PacientesController : ControllerBase {

        public PacientesService _pacientesService;
        public Generics _generics;

        public PacientesController(PacientesService pacientesService, Generics generics) {
            _pacientesService = pacientesService;
            _generics         = generics;
        }

        [HttpGet]
        [Route("paciente/index")]
        public async Task<IActionResult> Get() {
            try {
                IEnumerable<Paciente> pacientes = await _pacientesService.GetAll();
                return Ok(pacientes);
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                return _generics.ErroServer;
            }
        }

        [HttpGet]
        [Route("paciente/{id}")]
        public async Task<IActionResult> GetById(int id) {
            Paciente? paciente = await _pacientesService.FindById(id);;

            if (paciente is null) {
                return NotFound();
            } else {
                return Ok(paciente);
            }            
        }

        [HttpPost]
        [Route("paciente/novo")]
        public async Task<IActionResult> Post(Paciente paciente) {
            try { 
                await _pacientesService.Insert(paciente);
                return Ok();
            } catch (Exception e) {             
                Console.WriteLine(e.Message);
                return _generics.ErroServer;
            }            
        }

        [HttpPut]
        [Route("paciente/{id}")]
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
                return _generics.ErroServer;
            }
        }
 
        [HttpDelete]
        [Route("paciente/{id}")]
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
                return _generics.ErroServer;
            }
        }
    }
}
