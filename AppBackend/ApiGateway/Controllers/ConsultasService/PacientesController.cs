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
                Console.WriteLine(pacienteStr);
                using HttpResponseMessage res = await _client.PostAsync($"pacientes/novo", new StringContent(pacienteStr));

                return StatusCode((int)res.StatusCode);

            } catch (Exception e) {

                Console.WriteLine(e.Message);
                return _constants.ErroServer;
            }
        }
        
        [HttpPut]      
        [Route("api/consultas/pacientes/{id}")]
        public async Task<IActionResult> Update(int id, Paciente paciente) {

            await Task.Delay(100);

            return Ok();
        }

        [HttpDelete]
        [Route("api/consultas/pacientes/{id}")]
        public async Task<IActionResult> Delete(int id) {

            try {
                
                using HttpResponseMessage res = await _client.DeleteAsync($"pacientes/{id}");
                return StatusCode((int)res.StatusCode);

            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return _constants.ErroServer;
            }
        }
    }
}