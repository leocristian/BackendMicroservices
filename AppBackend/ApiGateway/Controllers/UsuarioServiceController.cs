using Microsoft.AspNetCore.Mvc;
using ApiGateway.Models;
using ApiGateway.Generics;
using Newtonsoft.Json;

namespace ApiGateway {

    [ApiController]
    public class UsuarioServiceController : ControllerBase {

        public HttpClient _client;

        public Constants _constants;

        public UsuarioServiceController(Constants constants) {
            _client = new HttpClient() { BaseAddress = new Uri("http://localhost:5091/") };
            _constants = constants;
        }

        [HttpPost]
        [Route("api/usuario/login")]
        public async Task<IActionResult> Login(Usuario usuario) {
            try {

                string loginStr = JsonConvert.SerializeObject(usuario);

                Console.WriteLine(loginStr);

                using HttpResponseMessage res = await _client.PostAsync($"enfermeiro/login", new StringContent(loginStr));
                return Ok(res.Content);

            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return _constants.ErroServer;
            }
        }

        [HttpPost]
        [Route("api/usuario/novo")]
        public async Task<IActionResult> Insert(Usuario usuario) {
            try {
                
                string usuarioStr = JsonConvert.SerializeObject(usuario);

                Console.WriteLine(usuarioStr);

                using HttpResponseMessage res = await _client.PostAsync($"enfermeiro/novo", new StringContent(usuarioStr));
                return StatusCode((int)res.StatusCode);

            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return _constants.ErroServer;
            }
        }
    }
}