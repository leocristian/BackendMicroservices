
namespace EnfermeiroService.Models {
    public class Usuario {
        public int Id { get; set; }
        public string? Cpf { get; set; }
        public string? Telefone { get; set; }
        public string? NomeCompleto { get; set; }
        public string? Coren { get; set; }
        public string? Login { get; set; }
        public string? Senha { get; set; }
        public int Grupo { get; set; }

        // public Usuario(int id, string cpf, string telefone, string nomeCompleto, string coren, string login, string senha, int grupo) {
        //     Id = id;
        //     Cpf = cpf;
        //     Telefone = telefone;
        //     NomeCompleto = nomeCompleto;
        //     Coren = coren;
        //     Login = login;
        //     Senha = senha;
        //     Grupo = grupo;
        // }
    }
}