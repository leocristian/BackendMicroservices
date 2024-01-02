namespace ApiGateway.Models {
    public class Agendamento {

        public int Id { get; set; }
        public int IdPaciente { get; set; }
        public int IdEnfermeiro { get; set; }
        public string Descricao { get; set; }
        public DateOnly Data { get; set; }
        public TimeOnly Hora { get; set; }
        public int IdLocal { get; set; }
        public string Observacoes { get; set; }

        public Agendamento(int id, int idPaciente, int idEnfermeiro, string descricao, DateOnly data, TimeOnly hora, int idLocal, string observacoes) {
            Id = id;
            IdPaciente = idPaciente;
            IdEnfermeiro = idEnfermeiro;
            Descricao = descricao;
            Data = data;
            Hora = hora;
            IdLocal = idLocal;
            Observacoes = observacoes;
        }
    }
}
