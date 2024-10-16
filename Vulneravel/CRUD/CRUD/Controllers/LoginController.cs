using CRUD_Tg.DAO;
using CRUD_Tg.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Controllers {
    public class LoginController : Controller {
        public IDAO DAO { get; set; }
        
        public IActionResult Index() {
            return View();
        }
        
        [HttpPost]
        public IActionResult Entrar(Usuario Usuario) {
            DAO = new DAOMysql();

            Usuario usuarioEncontrado = DAO.SelectUsuarioLogin(Usuario);

            if (usuarioEncontrado.Cpf != null) {
                return RedirectToAction("Index", "Reader");
            }


            TempData["AcessoNegado"] = "Falha no Login. CPF e/ou Senha incorreto";
            return View("Index");
        }
    }
}
