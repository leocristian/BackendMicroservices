using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ApiGateway.Models;
using ApiGateway.Generics;

namespace ApiGateway.Controllers {

    [ApiController]
    public class ConsultasServiceController : ControllerBase {

        public HttpClient _client;

        public Constants _constants;

        public ConsultasServiceController(Constants constants) {
            _client = new HttpClient() { BaseAddress = new Uri("http://localhost:5092/") };
            _constants = constants;
        }

        [HttpGet]
        [Route("api/consultas/pacientes")]
        public async Task<IActionResult> GetPacientes() {

            try {
                using HttpResponseMessage res = await _client.GetAsync($"pacientes");

                string strJson = await res.Content.ReadAsStringAsync();

                Console.WriteLine(strJson);

                List<Paciente>? pacientes = JsonSerializer.Deserialize<List<Paciente>>(strJson);

                return Ok(strJson);
            
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
                string strJson = await res.Content.ReadAsStringAsync();

                return Ok(strJson);
            
            } catch(Exception e) {
            
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

            await Task.Delay(100);
            
            return Ok();
        }

        [HttpPost]
        [Route("api/consultas/pacientes/{id}")]
        public async Task<IActionResult> Insert(int id, Paciente paciente) {

            await Task.Delay(100);
            
            return Ok();
        }
    }
}