using CRUD_Tg.DAO;
using CRUD_Tg.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace CRUD_Tg.Controllers{
    public class RegisterController : Controller{
        public IActionResult Index(){
            return View();
        }

        [HttpPost]
        public IActionResult RegisterUsuario(Usuario usuario){
            if (usuario != null){
                
                    IDAO banco = new DAOMysql();

                    if (banco.Insert(usuario)) {
                        TempData["Sucesso"] = "Usuario cadastrado";
                        return RedirectToAction("Index", "Register"); 
                    }
                    TempData["Sucesso"] = "Falha no cadastro";

                return RedirectToAction("Index", "Register");
            } else{
                TempData["Sucesso"] = "Falha, dados do usuarios incorreto";
                return RedirectToAction("Index", "Register");
            }
        }
    }
}
