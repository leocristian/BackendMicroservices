using Microsoft.AspNetCore.Mvc;
using ConsultaService.Services;
using ConsultaService.Models;
using ConsultaService.Lib;

namespace ConsultaService.Controllers {

    [ApiController]
    public class ConsultasController : ControllerBase {

        public ConsultasService _consultasService;
        public Generics _generics;

        public ConsultasController(ConsultasService consultasService, Generics generics) {
            _consultasService = consultasService;
            _generics         = generics;
        }

        [HttpGet]
        [Route("paciente/{id}/consulta/index")]
        public async Task<IActionResult> GetAllFromPaciente(int id) {

            try {
                IEnumerable<Consulta> consultas = await _consultasService.GetAllFromPaciente(id); 
                return Ok(consultas);
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                return _generics.ErroServer;
            }
        }

        [HttpPost]
        [Route("paciente/{id}/consulta/novo")]
        public async Task<IActionResult> Post(Consulta consulta, int id) {

            try {
                consulta.IdPaciente = id;
                await _consultasService.Insert(consulta);
                return Ok("Consulta realizado com sucesso!");
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return _generics.ErroServer;
            }
        }


        [HttpGet]
        [Route("paciente/{idPaciente}/consulta/{idConsulta}")]
        public async Task<IActionResult> GetById(int idPaciente, int idConsulta) {
            try {

                Consulta? consulta = await _consultasService.GetById(idPaciente, idConsulta);

                if (consulta is not null) {
                    return Ok(consulta);
                } else {
                    return NotFound();
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return _generics.ErroServer;
            }
        }

        [HttpPut]
        [Route("paciente/{idPaciente}/consulta/{idConsulta}")]
        public async Task<IActionResult> Put(int idPaciente, int idConsulta, Consulta consulta) {

            try {
                if (idConsulta != consulta.Id) {
                    return BadRequest("Requisição Inválida!");
                } else {

                    consulta.IdPaciente = idPaciente;
                    consulta.Id         = idConsulta;
                    
                    await _consultasService.Update(consulta);
                    return Ok("Consulta alterado com sucesso!");
                }
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                return _generics.ErroServer;
            }
        }

        [HttpPatch]
        [Route ("paciente/{idPaciente}/consulta/{idConsulta}")]
        public async Task<IActionResult> AtualizarStatus(int idPaciente, int idConsulta, string novoStatus) {
            
            try { 
                
                await _consultasService.UpdateStatus(idPaciente, idConsulta, novoStatus);
                return Ok("Status Alterado com Sucesso!");

            } catch(Exception e) {
                Console.WriteLine(e.Message);
                return _generics.ErroServer;
            }
        }

        [HttpDelete]
        [Route("paciente/{idPaciente}/consulta/{idConsulta}")]
        public async Task<IActionResult> Delete(int idPaciente, int idConsulta) {

            try {
                if (await _consultasService.GetById(idPaciente, idConsulta) is not null) {
                    await _consultasService.Delete(idPaciente, idConsulta);
                    return Ok("Consulta Excluído com Sucesso!");
                } else {
                    return NotFound("Consulta não encontrado!");
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return _generics.ErroServer;
            }
        }
    }
}
