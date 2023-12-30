using Microsoft.AspNetCore.Mvc;
using EnfermeiroService.Services;
using EnfermeiroService.Models;

namespace EnfermeiroService.Controllers {
    
    [ApiController]
    public class UsuarioController : ControllerBase {

        public UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService) {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        [Route("enfermeiro/login")]
        public async Task<IActionResult> Login(string username, string senha) {

            Usuario? usuario;

            try {
                usuario = await _usuarioService.ReadByLogin(username, senha);
                if (usuario is not null) {
                    return Ok(usuario);
                } else {
                    return NotFound();
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return Problem("Erro interno no servidor!", null, 500);
            }
        }

        [HttpPost]
        [Route("enfemeiro/novo")]
        public async Task<IActionResult?> SignUp(Usuario usuario) {
            try {
                await _usuarioService.SignUp(usuario);
                return Ok("Usuário cadastrado com sucesso!");
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return Problem("Erro interno no servidor!", null, 500);
            }
        }
    }
}