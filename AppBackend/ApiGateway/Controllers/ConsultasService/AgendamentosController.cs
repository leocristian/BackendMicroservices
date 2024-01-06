using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ApiGateway.Models;
using ApiGateway.Generics;
using Microsoft.AspNetCore.Authorization;
// using Newtonsoft.Json;
// using System.Text.Json;

namespace ApiGateway.Controllers {

    [ApiController]
    public class AgendamentosController : ControllerBase {

        public HttpClient _client;

        public Constants _constants;

        public AgendamentosController(Constants constants, HttpClient client) {
            _client = client;
            _constants = constants;
        }

        [HttpGet]
        [Authorize(Policy = "Enfermeiros")]
        [Route("api/consultas/pacientes/{id}/agendamentos")]
        public async Task<IActionResult> GetAgendamentos(int id) {

            try {

                using HttpResponseMessage res = await _client.GetAsync($"pacientes/{id}/agendamentos");

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
        [Authorize(Policy = "Enfermeiros")]
        [Route("api/consultas/pacientes/{idPaciente}/agendamentos/{idAgendamento}")]
        public async Task<IActionResult> GetAgendamentosById(int idPaciente, int idAgendamento) {

            try {

                using HttpResponseMessage res = await _client.GetAsync($"pacientes/{idPaciente}/agendamentos/{idAgendamento}");
                
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
        [Authorize(Policy = "Medicos")]
        [Route("api/consultas/pacientes/{id}/agendamentos/novo")]
        public async Task<IActionResult> Insert(int id, Agendamento agendamento) {

            try {

                string agendamentoStr = JsonSerializer.Serialize(agendamento);

                Console.WriteLine(agendamentoStr);

                using HttpResponseMessage res = await _client.PostAsJsonAsync<Agendamento>($"pacientes/{id}/agendamentos/novo", agendamento);

                if ((int)res.StatusCode == 200) {
                    return Ok("Agendamento realizado com sucesso!");
                } else {
                    return StatusCode((int)res.StatusCode);
                }

            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return _constants.ErroServer;
            }
        }


        [HttpDelete]
        [Authorize(Policy = "Medicos")]
        [Route("api/consultas/pacientes/{idPaciente}/agendamentos/{idAgendamento}")]
        public async Task<IActionResult> Delete(int idPaciente, int idAgendamento) {

            try {
                
                using HttpResponseMessage res = await _client.DeleteAsync($"pacientes/{idPaciente}/agendamentos/{idAgendamento}");
                if ((int)res.StatusCode == 200) {
                    return Ok("Agendamento excluído com sucesso!");
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