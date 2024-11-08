using CRUD_Tg.Models;

namespace CRUD_Tg.Controllers {
    public class CriarUsuario {
        public static Usuario Criar(string StrName, string StrCPF, string StrPWD, int IdUser, string StrTipoUser, int IdTipo) {
            Usuario usuario = new Usuario();    
            usuario.Name = StrName;
            usuario.Cpf = StrCPF;
            usuario.Senha = StrPWD;
            usuario.Id = IdUser;

            TipoUsuario tipo = new TipoUsuario();
            tipo.Description = StrTipoUser;
            tipo.Id = IdTipo;

            usuario.Tipo = tipo;

            return usuario;
        }
    }
}
