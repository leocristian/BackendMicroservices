namespace ConsultaService.Models {
    public class Paciente {

        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateOnly? DataNascimento { get; set; }
        public string Cpf { get; set; }
        public string Endereco { get; set; }
        public string NumeroSus { get; set; }

        public Paciente(int id, string nomeCompleto, string email, string telefone, DateOnly? dataNascimento, string cpf, string endereco, string numeroSus) {
            Id = id;
            NomeCompleto = nomeCompleto;
            Email = email;
            Telefone = telefone;
            DataNascimento = dataNascimento;
            Cpf = cpf;
            Endereco = endereco;
            NumeroSus = numeroSus;
        }
    }
}
