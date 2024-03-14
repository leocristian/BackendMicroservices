namespace ConsultaService.Models {
    public class Paciente {

        public int Id { get; set; }
        public string? NomeCompleto { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataInicioGravidez { get; set; }
        public string? Cpf { get; set; }
        public string? Bairro { get; set; }
        public string? Endereco { get; set; }
        public string? NumeroSus { get; set; }

    }
}