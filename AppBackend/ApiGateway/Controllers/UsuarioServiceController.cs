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

        [HttpGet]
        [Route("api/usuario/index")]
        public async Task<IActionResult> Index() {
            try {
                
                using HttpResponseMessage res = await _client.GetAsync("enfermeiro/index"); 
                
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

        [HttpPost]
        [Route("api/usuario/login")]
        public async Task<IActionResult> Login(Usuario usuario) {
            try {

                string loginStr = JsonConvert.SerializeObject(usuario);

                Console.WriteLine(loginStr);

                using HttpResponseMessage res = await _client.PostAsJsonAsync<Usuario>($"enfermeiro/login", usuario);
                return StatusCode((int)res.StatusCode);

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

                using HttpResponseMessage res = await _client.PostAsJsonAsync<Usuario>($"enfermeiro/novo", usuario);

                Console.WriteLine(_client.BaseAddress);

                return StatusCode((int)res.StatusCode); 

            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return _constants.ErroServer;
            }
        }
    }
}