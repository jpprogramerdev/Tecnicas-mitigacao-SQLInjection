using CRUD_Tg.Models;
using MySql.Data.MySqlClient;

namespace CRUD_Tg.DAO{
    public interface IDAO{ 
        public void setStringConnection(string connection);
        public void ConnectionDataBase();
        public void OpenConnection();
        public bool Insert(Usuario usuario);
        public List<TipoUsuario> SelectTipos();
        public List<Usuario> SelectUsuarios();
        public Usuario SelectUsuarioPorId(int Id);
        public bool UpdateUsuario(Usuario usuario);
    }
}
