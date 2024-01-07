using Microsoft.AspNetCore.Mvc;
using ApiGateway.Models;
using ApiGateway.Generics;
using Microsoft.AspNetCore.Authorization;
using ApiGateway.Services;
using Newtonsoft.Json;

namespace ApiGateway {

    [ApiController]
    public class UsuarioServiceController : ControllerBase {

        public HttpClient _client;
        public Constants _constants;
        public IConfiguration _config;
        public TokenService _tokenService;

        public UsuarioServiceController(Constants constants, IConfiguration config, TokenService tokenService) {
            _client    = new HttpClient() { BaseAddress = new Uri("http://localhost:5091/") };
            _constants = constants;
            _config    = config;
            _tokenService = tokenService;
        }

        [HttpGet]
        [Authorize(Policy = "Usuarios")]
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
        [Route("api/usuario/novo")]
        public async Task<IActionResult> Insert([FromBody] Usuario usuario) {
            try {
                
                using HttpResponseMessage res = await _client.PostAsJsonAsync<Usuario>($"enfermeiro/novo", usuario);

                if ((int)res.StatusCode == 200) {
                    return Ok("Usuário Inserido com Sucesso!");
                } else {
                    return StatusCode((int)res.StatusCode, await res.Content.ReadAsStringAsync());
                }

            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return _constants.ErroServer;
            }
        }

        [HttpPost]
        [Route("api/usuario/autenticar")]
        public async Task<IActionResult> Autenticar(LoginInfo loginInfo) {

            try {
                using HttpResponseMessage res = await _client.PostAsJsonAsync<LoginInfo>($"enfermeiro/login", loginInfo);

                string usuarioStr = await res.Content.ReadAsStringAsync();

                Usuario? usuario = JsonConvert.DeserializeObject<Usuario>(usuarioStr);
            
                if (usuario is not null) {
                    var token = _tokenService.GerarToken(usuario);
                    return Ok(new { usuario, token });
                } else {
                    return NotFound("Usuário ou senha inválidos!");
                }
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                return _constants.ErroServer;
            }
        }

        [HttpPut]
        [Authorize(Policy = "Usuarios")]
        [Route("api/usuario/{id}")]
        public async Task<IActionResult> Put(string id, Usuario usuario) {
            try {
                using HttpResponseMessage res = await _client.PutAsJsonAsync<Usuario>($"enfermeiro/{id}", usuario);
            
                if ((int)res.StatusCode == 200) {
                    return StatusCode(200,  JsonConvert.DeserializeObject<Usuario>(await res.Content.ReadAsStringAsync()));
                } else {
                    return StatusCode((int)res.StatusCode);
                }

            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return _constants.ErroServer;
            }
        }
        
        [HttpDelete]
        [Authorize(Policy = "Usuarios")]
        [Route("api/usuario/{id}")]
        public async Task<IActionResult> Delete(int id) {

            try {
                
                using HttpResponseMessage res = await _client.DeleteAsync($"enfermeiro/{id}");

                if ((int)res.StatusCode == 200) {
                    return Ok("Usuário Deletado com Sucesso!");
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