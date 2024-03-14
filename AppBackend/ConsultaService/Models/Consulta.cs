namespace ConsultaService.Models {
    public class Consulta {

        public int Id                     { get; set; }
        public int IdPaciente             { get; set; }
        public int IdEnfermeiro           { get; set; }
        public string? Descricao          { get; set; }
        public int IdPosto                { get; set; }
        public string? Observacoes        { get; set; }
        public string? Situacao           { get; set; }    
        public DateOnly DataSolicitada    { get; set; }
        public TimeOnly HoraSolicitada    { get; set; }
        public DateOnly DataRealizada     { get; set; }
        public TimeOnly HoraRealizada     { get; set; }
        public string? MotivoCancelamento { get; set; }
        public string? Resultados         { get; set; }
        public string? ArqResultado       { get; set; }

    }
}