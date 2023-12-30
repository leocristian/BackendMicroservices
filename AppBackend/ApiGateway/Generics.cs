using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Generics {

    public class Constants : ControllerBase {
        public ObjectResult ErroServer { get; }

        public Constants() {
            ErroServer = Problem("Erro interno no servidor!", null, 500);
        }
    }
}