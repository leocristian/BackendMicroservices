namespace ConsultaService.Models {
    public class Consulta {

        public int Id { get; set; }
        public int IdPaciente { get; set; }
        public int IdEnfermeiro { get; set; }
        public string Descricao { get; set; }
        public DateTime DataHora { get; set; }
        public string Local { get; set; }
        public string Observacoes { get; set; }

        public Consulta(int id, int idPaciente, int idEnfermeiro, string descricao, DateTime dataHora, string local, string observacoes) {
            Id = id;
            IdPaciente = idPaciente;
            IdEnfermeiro = idEnfermeiro;
            Descricao = descricao;
            DataHora = dataHora;
            Local = local;
            Observacoes = observacoes;
        }

    }
}
