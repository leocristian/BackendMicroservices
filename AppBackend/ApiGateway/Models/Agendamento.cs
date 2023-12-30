namespace ApiGateway.Models {
    public class Agendamento {

        public int Id { get; set; }
        public int IdPaciente { get; set; }
        public int IdEnfermeiro { get; set; }
        public string Descricao { get; set; }
        public DateTime DataHora { get; set; }
        public int IdLocal { get; set; }
        public string Observacoes { get; set; }

        public Agendamento(int id, int idPaciente, int idEnfermeiro, string descricao, DateTime dataHora, int idLocal, string observacoes) {
            Id = id;
            IdPaciente = idPaciente;
            IdEnfermeiro = idEnfermeiro;
            Descricao = descricao;
            DataHora = dataHora;
            IdLocal = idLocal;
            Observacoes = observacoes;
        }
    }
}
