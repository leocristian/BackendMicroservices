namespace ConsultaService.Models {
    public class Agendamento {

        public int Id              { get; set; }
        public int IdPaciente      { get; set; }
        public int IdEnfermeiro    { get; set; }
        public string? Descricao   { get; set; }
        public DateOnly Data       { get; set; }
        public TimeOnly Hora       { get; set; }
        public int IdLocal         { get; set; }
        public string? Observacoes { get; set; }
        public string? Status      { get; set; }
    }
}
