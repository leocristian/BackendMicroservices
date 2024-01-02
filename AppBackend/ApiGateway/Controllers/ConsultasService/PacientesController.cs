using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ApiGateway.Models;
using ApiGateway.Generics;
using Newtonsoft.Json;
namespace ApiGateway.Controllers {

    [ApiController]
    public class PacientesController : ControllerBase {

        public HttpClient _client;

        public Constants _constants;

        public PacientesController(Constants constants, HttpClient client) {
            _client = client;
            _constants = constants;
        }

        [HttpGet]
        [Route("api/consultas/pacientes")]
        public async Task<IActionResult> GetPacientes() {

            try {

                using HttpResponseMessage res = await _client.GetAsync($"pacientes");

                if ((int)res.StatusCode == 200) {
                    string strJson = await res.Content.ReadAsStringAsync();
                    return Ok(strJson);   
                } else {
                    return StatusCode((int)res.StatusCode);
                }
            
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return _constants.ErroServer;
            }
            
        } 

        [HttpGet]
        [Route("api/consultas/pacientes/{id}")]
        public async Task<IActionResult> GetPacienteById(int id) {

            try {

                using HttpResponseMessage res = await _client.GetAsync($"pacientes/{id}");
                
                if ((int)res.StatusCode == 200) {
                    string strJson = await res.Content.ReadAsStringAsync();
                    return Ok(strJson);   
                } else {
                    return StatusCode((int)res.StatusCode);
                }
            
            } catch(Exception e) {
            
                Console.WriteLine(e.Message);
                return _constants.ErroServer;
            }
        }

        [HttpPost]
        [Route("api/consultas/pacientes/novo")]
        public async Task<IActionResult> Insert(Paciente paciente) {

            try {

                string pacienteStr = JsonConvert.SerializeObject(paciente);
                using HttpResponseMessage res = await _client.PostAsJsonAsync<Paciente>($"pacientes/novo", paciente);

                if ((int)res.StatusCode == 200) {
                    return Ok("Paciente Inserido com Sucesso!");
                } else {
                    return StatusCode((int)res.StatusCode);
                }    

            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return _constants.ErroServer;
            }
        }
        
        [HttpPut]      
        [Route("api/consultas/pacientes/{id}")]
        public async Task<IActionResult> Update(int id, Paciente paciente) {

            try {
                string pacienteStr = JsonConvert.SerializeObject(paciente);
                using HttpResponseMessage res = await _client.PutAsJsonAsync<Paciente>($"pacientes/{id}", paciente);

                if ((int)res.StatusCode == 200) {
                    return Ok("Paciente Atualizado com Sucesso!");
                } else {
                    return StatusCode((int)res.StatusCode);
                }    

            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return _constants.ErroServer;
            }
        }

        [HttpDelete]
        [Route("api/consultas/pacientes/{id}")]
        public async Task<IActionResult> Delete(int id) {

            try {
                
                using HttpResponseMessage res = await _client.DeleteAsync($"pacientes/{id}");

                if ((int)res.StatusCode == 200) {
                    return Ok("Paciente Deletado com Sucesso!");
                } else {
                    return StatusCode((int)res.StatusCode);
                } 

            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return _constants.ErroServer;
            }
        }
    }
}