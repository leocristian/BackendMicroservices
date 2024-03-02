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
        [Route("/usuario/index")]
        public async Task<IActionResult> Index() {
            try {
                IEnumerable<Usuario> usuarios = await _usuarioService.GetAll();
                return Ok(usuarios);    
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                return Problem("Erro interno no servidor", null, 500);
            }
        }

        [HttpPost]
        [Route("/usuario/login")]
        public async Task<IActionResult> Login(LoginInfo loginInfo) {

            try {

                Usuario? usuario = await _usuarioService.ReadByLoginInfo(loginInfo.NomeUsuario, loginInfo.Senha);

                if (usuario is null) {
                    return NotFound("Usuário não encontrado!");
                } else {
                    return Ok(usuario);
                }

            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return Problem("Erro interno no servidor!", null, 500);
            }
        }   

        [HttpPost]
        [Route("/usuario/novo")]
        public async Task<IActionResult> SignUp(Usuario usuario) {
            try {
                
                if (await _usuarioService.UserExists(usuario.Login!)) {
                    return Problem("Já existe um usuário com este login!", null, 409);
                }else {
                    await _usuarioService.SignUp(usuario);
                    return Ok();
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return Problem("Erro interno no servidor!", null, 500);
            }
        }

        [HttpPut]
        [Route("/usuario/{id}")]
        public async Task<IActionResult> Put(int id, Usuario usuario) {
            try {
                
                usuario.Id = id;

                if (await _usuarioService.FindById(id) is not null) {
                    
                    await _usuarioService.Update(usuario);
                    Usuario? usuarioUpdated = await _usuarioService.FindById(id)!;

                    return Ok(usuarioUpdated);
                } else {
                    return NotFound();
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return Problem("Erro interno no servidor!", null, 500);
            }
        }

        [HttpDelete]
        [Route("/usuario/{id}")]
        public async Task<IActionResult> Delete(int id) {
            try {
                if (await _usuarioService.FindById(id) is not null) {
                    await _usuarioService.Delete(id);
                    return Ok();
                } else {
                    return NotFound();
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return Problem("Erro interno no servidor!", null, 500);
            }
        }
    }
}
