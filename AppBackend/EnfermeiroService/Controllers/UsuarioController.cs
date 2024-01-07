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

                Usuario? enfermeiro = await _usuarioService.ReadByLoginInfo(loginInfo.NomeUsuario, loginInfo.Senha);
                return Ok(enfermeiro);

            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return Problem("Erro interno no servidor!", null, 500);
            }
        }   

        [HttpPost]
        [Route("enfermeiro/novo")]
        public async Task<IActionResult> SignUp(Usuario usuario) {
            try {
                
                if (await _usuarioService.ReadByLogin(usuario.Login) is not null) {
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
        [Route("enfermeiro/{id}")]
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
        [Route("enfermeiro/{id}")]
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
