using Microsoft.AspNetCore.Mvc;
using ApiGateway.Models;
using ApiGateway.Generics;
using System.Text.Json;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
// using Newtonsoft.Json;

namespace ApiGateway {

    [ApiController]
    public class UsuarioServiceController : ControllerBase {

        public HttpClient _client;

        public Constants _constants;

        public IConfiguration _config;

        public UsuarioServiceController(Constants constants, IConfiguration config) {
            _client    = new HttpClient() { BaseAddress = new Uri("http://localhost:5091/") };
            _constants = constants;
            _config    = config;
        }

        [HttpGet]
        [Authorize]
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
        public async Task<IActionResult> Login(LoginInfo loginInfo) {
            try {

                string loginStr = JsonSerializer.Serialize<LoginInfo>(loginInfo);

                Console.WriteLine(loginStr);

                using HttpResponseMessage res = await _client.PostAsJsonAsync<LoginInfo>($"enfermeiro/login", loginInfo);
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
                
                string usuarioStr = JsonSerializer.Serialize<Usuario>(usuario);
                using HttpResponseMessage res = await _client.PostAsJsonAsync<Usuario>($"enfermeiro/novo", usuario);

                if ((int)res.StatusCode == 200) {
                    return Ok("Usuário Inserido com Sucesso!");
                } else {
                    return StatusCode((int)res.StatusCode);
                }

            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return _constants.ErroServer;
            }
        }

        [HttpPost]
        [Route("api/usuario/gerartoken")]
        public async Task<IActionResult> GerarToken(LoginInfo loginInfo) {

            using HttpResponseMessage res = await _client.PostAsJsonAsync<LoginInfo>($"enfermeiro/login", loginInfo);

            if ((int)res.StatusCode == 200) {
                var issuer = _config["Jwt:Issuer"];
                var audience = _config["Jwt:Audience"];
                var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]!);
                var tokenDescriptor = new SecurityTokenDescriptor {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim("Id", Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Sub, loginInfo.NomeUsuario),
                        new Claim(JwtRegisteredClaimNames.Email, loginInfo.Senha),
                        new Claim(JwtRegisteredClaimNames.Jti,
                        Guid.NewGuid().ToString())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);
                var stringToken = tokenHandler.WriteToken(token);
                return Ok(stringToken);
            }
            return Unauthorized();
        }
    }
}