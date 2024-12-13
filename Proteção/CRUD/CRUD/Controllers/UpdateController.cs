using Microsoft.AspNetCore.Mvc;
using CRUD_Tg.DAO;
using CRUD_Tg.Models;

namespace CRUD_Tg.Controllers {
    public class UpdateController : Controller {
        public IDAO banco { get; set; }

        [HttpGet]
        public IActionResult Index(int id) {
            Usuario usuarioLogado = new();
            usuarioLogado.Name = User.FindFirst("NOME").Value;
            usuarioLogado.Cpf = User.FindFirst("CPF").Value;
            usuarioLogado.Tipo.Description = User.FindFirst("Tipo").Value;
            if (usuarioLogado.Tipo.Description == "Administrador") {
                banco = new DAOMysql("server=localhost;uid=root;pwd=1234;database=CRUD_TG");
            } else {
                banco = new DAOMysql("server=localhost;uid=CLIENTE;pwd=teste1234;database=CRUD_TG");
            }
            return View(banco.SelectUsuarioPorId(id));
        }

        [HttpPost]
        public IActionResult Atualizar(Usuario usuario) {
            Usuario usuarioLogado = new();
            usuario.Name = User.FindFirst("NOME").Value;
            usuario.Cpf = User.FindFirst("CPF").Value;
            usuario.Tipo.Description = User.FindFirst("Tipo").Value;
            if (usuarioLogado.Tipo.Description == "Administrador") {
                banco = new DAOMysql("server=localhost;uid=root;pwd=1234;database=CRUD_TG");
            } else {
                banco = new DAOMysql("server=localhost;uid=CLIENTE;pwd=teste1234;database=CRUD_TG");
            }
            TempData["SituacaoAtualizacao"] = banco.UpdateUsuario(usuario) ? "Usuario atualizado com sucesso" :  "Falha ao atualizar o usuario";

            return RedirectToAction("Index", "Update", usuario.Id);
        }
    }
}
