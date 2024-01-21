namespace ConsultaService.Models {
    public class Agendamento(int id, int idPaciente, int idEnfermeiro, string descricao, DateOnly data, TimeOnly hora, int idLocal, string observacoes, string status) {

        public int Id             { get; set; } = id;
        public int IdPaciente     { get; set; } = idPaciente;
        public int IdEnfermeiro   { get; set; } = idEnfermeiro;
        public string Descricao   { get; set; } = descricao;
        public DateOnly Data      { get; set; } = data;
        public TimeOnly Hora      { get; set; } = hora;
        public int IdLocal        { get; set; } = idLocal;
        public string Observacoes { get; set; } = observacoes;
        public string Status      { get; set; } = status;
    }
}
