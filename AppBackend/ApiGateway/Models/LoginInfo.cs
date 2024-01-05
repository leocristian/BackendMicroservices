
namespace ApiGateway.Models {
    
    public class LoginInfo {

        public string NomeUsuario { get; set; }
        public string Senha { get; set; }

        public LoginInfo(string nomeUsuario, string senha) {
            NomeUsuario = nomeUsuario;
            Senha       = senha;
        }
    }
}