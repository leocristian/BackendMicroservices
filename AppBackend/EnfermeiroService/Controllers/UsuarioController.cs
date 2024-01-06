using Microsoft.AspNetCore.Mvc;
using EnfermeiroService.Services;
using EnfermeiroService.Models;
using Microsoft.AspNetCore.Authorization;

namespace EnfermeiroService.Controllers {
    
    [ApiController]
    public class UsuarioController : ControllerBase {

        public UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService) {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [Route("enfermeiro/index")]
        public async Task<IActionResult> Index() {
            try {
                List<Usuario> usuarios = await _usuarioService.GetAll();
                return Ok(usuarios);    
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                return Problem("Erro interno no servidor", null, 500);
            }
        } 

        [HttpPost]
        [Route("enfermeiro/login")]
        public async Task<IActionResult> Login(LoginInfo loginInfo) {

            try {
                Usuario? enfermeiro = await _usuarioService.ReadByLogin(loginInfo.NomeUsuario, loginInfo.Senha);

                return Ok(enfermeiro);
                // if (enfermeiro is not null) {
                //     return Ok(enfermeiro);
                // } else {
                //     return NotFound();
                // }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return Problem("Erro interno no servidor!", null, 500);
            }
        }   

        [HttpPost]
        [Route("enfermeiro/novo")]
        public async Task<IActionResult> SignUp(Usuario usuario) {
            try {
                await _usuarioService.SignUp(usuario);
                return Ok();
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return Problem("Erro interno no servidor!", null, 500);
            }
        }
    }
}
