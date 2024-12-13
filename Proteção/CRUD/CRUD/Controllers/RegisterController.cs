using CRUD_Tg.DAO;
using CRUD_Tg.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace CRUD_Tg.Controllers {
    public class RegisterController : Controller {
        public IDAO banco { get; set; }
        public IActionResult Index() {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterUsuario(Usuario usuario) {
            if (usuario != null) {
                Usuario usuarioLogado = new();
                usuarioLogado.Name = User.FindFirst("NOME").Value;
                usuarioLogado.Cpf = User.FindFirst("CPF").Value;
                usuarioLogado.Tipo.Description = User.FindFirst("Tipo").Value;
                if (usuarioLogado.Tipo.Description == "Administrador") {
                    banco = new DAOMysql("server=localhost;uid=root;pwd=1234;database=CRUD_TG");
                } else {
                    banco = new DAOMysql("server=localhost;uid=CLIENTE;pwd=teste1234;database=CRUD_TG");
                }

                if (banco.Insert(usuario)) {
                    TempData["Sucesso"] = "Usuario cadastrado";
                    return RedirectToAction("Index", "Register");
                }
                TempData["Sucesso"] = "Falha no cadastro";

                return RedirectToAction("Index", "Register");
            } else {
                TempData["Sucesso"] = "Falha, dados do usuarios incorreto";
                return RedirectToAction("Index", "Register");
            }
        }
    }
}
