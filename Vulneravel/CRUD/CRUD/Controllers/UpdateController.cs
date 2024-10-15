using Microsoft.AspNetCore.Mvc;
using CRUD_Tg.DAO;
using CRUD_Tg.Models;

namespace CRUD_Tg.Controllers {
    public class UpdateController : Controller {
        [HttpGet]
        public IActionResult Index(int id) {
            IDAO dao = new DAOMysql();
            return View(dao.SelectUsuarioPorId(id));
        }

        [HttpPost]
        public IActionResult Atualizar(Usuario usuario) {
            IDAO dao = new DAOMysql();
            TempData["SituacaoAtualizacao"] = dao.UpdateUsuario(usuario) ? "Usuario atualizado com sucesso" :  "Falha ao atualizar o usuario";

            return RedirectToAction("Index", "Update", usuario.Id);
        }
    }
}
