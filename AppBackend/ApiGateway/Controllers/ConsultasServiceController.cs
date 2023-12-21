using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers {

    [ApiController]
    public class ConsultasServiceController : ControllerBase {

        public HttpClient _client;

        public ConsultasServiceController() {
            _client = new HttpClient() { BaseAddress = new Uri("http://localhost:1232/") };
        } 

        [HttpGet]
        [Route("api/consultas/pacientes/{id}")]
        public async Task<IActionResult> PacientesById(int id) {

            using HttpResponseMessage res = await _client.GetAsync("pacientes/{id}");

            return Ok();
        }
    }

}