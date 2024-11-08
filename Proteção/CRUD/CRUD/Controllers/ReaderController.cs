using CRUD_Tg.DAO;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_Tg.Controllers {
    public class ReaderController : Controller {
        public IDAO database { get; set; }
        
        public IActionResult Index() {
            database = new DAOMysql();
            return View(database.SelectUsuarios());
        }
    }
}
