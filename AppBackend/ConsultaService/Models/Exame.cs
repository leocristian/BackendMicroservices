namespace ConsultaService.Models {
    public class Exame {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public string NomeExame { get; set; }
        public string Descricao { get; set; }
        public string Resultado { get; set; }

        public Exame(int id, DateTime dataHora, String nomeExame, string descricao, string resultado) {
            Id        = id;
            DataHora  = dataHora;
            NomeExame = nomeExame;
            Descricao = descricao;
            Resultado = resultado;
        }
    }
}
