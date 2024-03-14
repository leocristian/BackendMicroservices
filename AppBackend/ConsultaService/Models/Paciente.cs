namespace ConsultaService.Models {
    public class Paciente {

        public int Id { get; set; }
        public int IdEnfermeiro { get; set; }
        public string? NomeCompleto { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public DateOnly DataNascimento { get; set; }
        public DateOnly DataInicioGravidez { get; set; }
        public string? Cpf { get; set; }
        public string? Bairro { get; set; }
        public string? Endereco { get; set; }
        public string? NumeroSus { get; set; }

    }
}