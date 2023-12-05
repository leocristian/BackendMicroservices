using Microsoft.AspNetCore.Mvc;
using EnfermeiroService.Services;
using EnfermeiroService.Models;

namespace EnfermeiroService.Controllers {
    
    [ApiController]
    public class UsuarioController : ControllerBase {

        private readonly UsuarioService usuarioService = new();

        [HttpPost]
        [Route("enfemeiro/novo")]
        public async Task<IActionResult?> SignUp(Usuario usuario) {
            try {
                await usuarioService.SignUp(usuario);
                return Ok("Usuário cadastrado com sucesso!");
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return Problem("Erro interno no servidor!", null, 500);
            }
        }
    }
}