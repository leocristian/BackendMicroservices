using Microsoft.AspNetCore.Mvc;

namespace ConsultaService.Lib {

    public class Generics : ControllerBase {
        public ObjectResult ErroServer { get; }

        public Generics() {
            ErroServer = Problem("Erro interno no servidor!", null, 500);
        }
    }
}