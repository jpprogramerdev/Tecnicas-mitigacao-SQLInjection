using CRUD_Tg.DAO;
using CRUD_Tg.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_Tg.Controllers {
    public class ReaderController : Controller {
        public IDAO banco { get; set; }
        
        public IActionResult Index() {
            Usuario usuarioLogado = new();
            usuarioLogado.Name = User.FindFirst("NOME").Value;
            usuarioLogado.Cpf = User.FindFirst("CPF").Value;
            usuarioLogado.Tipo.Description = User.FindFirst("Tipo").Value;
            if (usuarioLogado.Tipo.Description == "Administrador") {
                banco = new DAOMysql("server=localhost;uid=root;pwd=1234;database=CRUD_TG");
            } else {
                banco = new DAOMysql("server=localhost;uid=CLIENTE;pwd=teste1234;database=CRUD_TG");
            }

            return View(banco.SelectUsuarios());
        }
    }
}
