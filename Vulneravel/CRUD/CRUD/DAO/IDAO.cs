using CRUD_Tg.Models;
using MySql.Data.MySqlClient;

namespace CRUD_Tg.DAO{
    public interface IDAO{ 
        public void setStringConnection(string connection);
        public bool Insert(Usuario usuario);
        public List<TipoUsuario> SelectTipos();
        public List<Usuario> SelectUsuarios();
        public Usuario SelectUsuarioPorId(int Id);
        public Usuario SelectUsuarioLogin(Usuario UsuarioLogin);

        public bool UpdateUsuario(Usuario usuario);
    }
}
