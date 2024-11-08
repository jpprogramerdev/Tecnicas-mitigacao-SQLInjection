using CRUD_Tg.DAO;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Controllers {
    public class DeleteController : Controller {
        public IDAO DAO { get; set; }
        public IActionResult Delete(int id) {
            DAO = new DAOMysql();
            DAO.DeleteUsuario(id);
            return RedirectToAction("Index","Reader");
        }
    }
}
