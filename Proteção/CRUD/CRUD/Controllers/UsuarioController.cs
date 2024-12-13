using CRUD_Tg.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Controllers {
    public class UsuarioController : Controller {
        public IActionResult Index() {
            Usuario usuario = new();
            usuario.Name = User.FindFirst("NOME").Value;
            usuario.Cpf = User.FindFirst("CPF").Value;
            usuario.Tipo.Description = User.FindFirst("Tipo").Value;
            return View(usuario);
        }
    }
}
