using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Generics {

    public class Constants : ControllerBase {
        public ObjectResult ErroServer { get; }

        public Constants() {
            Console.WriteLine("Constants Criado!");
            ErroServer = Problem("Erro interno no servidor!", null, 500);
        }
    }
}