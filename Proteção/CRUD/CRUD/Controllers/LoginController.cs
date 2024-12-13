using CRUD_Tg.DAO;
using CRUD_Tg.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;
using System.Security.Claims;

namespace CRUD.Controllers {
    public class LoginController : Controller {
        public IDAO DAO { get; set; }

        public IActionResult Index() {
            return View();
        }

        [HttpPost]
        public IActionResult Entrar(Usuario Usuario) {
            DAO = new DAOMysql("server=localhost;uid=root;pwd=1234;database=CRUD_TG");

            Usuario usuarioEncontrado = DAO.SelectUsuarioLogin(Usuario);

            if (usuarioEncontrado.Cpf != null) {
                var claims = new List<Claim> {
                        new Claim ("NOME", usuarioEncontrado.Name),
                        new Claim ("CPF", usuarioEncontrado.Cpf),
                        new Claim("Tipo", usuarioEncontrado.Tipo.Description)
                    };

                var indetity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(indetity);

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                if (usuarioEncontrado.Tipo.Description == "Administrador") {
                    return RedirectToAction("Index", "Reader");
                }
                return RedirectToAction("Index", "Usuario");



            }
            TempData["AcessoNegado"] = "Falha no Login. CPF e/ou Senha incorreto";
            return View("Index");
        }
    }
}
