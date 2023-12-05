
namespace EnfermeiroService.Models {
    public class Usuario {
        public int Id { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public string NomeCompleto { get; set; }
        public string Coren { get; set; }
        public DateOnly DataNascimento { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }

        public Usuario(int id, string cpf, string telefone, string nomeCompleto, string coren, DateOnly dataNascimento, string login, string senha) {
            Id = id;
            Cpf = cpf;
            Telefone = telefone;
            NomeCompleto = nomeCompleto;
            Coren = coren;
            DataNascimento = dataNascimento;
            Login = login;
            Senha = senha;
        }
    }
}