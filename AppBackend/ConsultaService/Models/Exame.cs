namespace ConsultaService.Models {
    public class Exame(int id, DateTime dataHora, String nomeExame, string descricao, string resultado) {
        public int Id            { get; set; } = id;
        public DateTime DataHora { get; set; } = dataHora;
        public string NomeExame  { get; set; } = nomeExame;
        public string Descricao  { get; set; } = descricao;
        public string Resultado  { get; set; } = resultado;
    }
}
