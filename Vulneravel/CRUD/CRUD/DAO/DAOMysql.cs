using CRUD_Tg.Controllers;
using CRUD_Tg.Models;
using MySql.Data.MySqlClient;

namespace CRUD_Tg.DAO{
    public class DAOMysql : IDAO {
        public string StrConnection { get; private set; }

        public DAOMysql(){
            setStringConnection("server=localhost;uid=root;pwd=1234;database=CRUD_TG");
        }

        public void setStringConnection(string connection){
            StrConnection = connection; // a string devera conter: "server=;uid;pwd;database;"
        }

        public bool Insert(Usuario usuario){
            try{
                string insert = "INSERT INTO Usuarios (USU_Nome, USU_Cpf, USU_Senha, USU_TPU_Id) values('" + usuario.Name + "','" + usuario.Cpf + "','" + usuario.Senha + "', " + usuario.Tipo.Id + ");";

                using(MySqlConnection conn = new(StrConnection)) {
                    using (MySqlCommand query = new (insert, conn)) {
                        conn.Open();
                        query.ExecuteNonQuery();
                    }
                }
                
                return true;
            }catch(Exception ex) {
                return false;
            }
        }

        public List<Usuario> SelectUsuarios() {
            try {
                List<Usuario> usuarios = new List<Usuario>();
                string select = "SELECT USU_Nome, TPU_TIPO, USU_Cpf, USU_Senha, USU_Id, TPU_Id FROM usuarios LEFT JOIN tipos_usuario ON USU_TPU_Id = TPU_Id;";

                using (MySqlConnection conn = new(StrConnection)) {
                    using (MySqlCommand query = new(select, conn)) {
                        conn.Open();
                        using (MySqlDataReader reader = query.ExecuteReader()) {
                            while (reader.Read()) {
                                usuarios.Add(CriarUsuario.Criar(
                                    reader.GetString(reader.GetOrdinal("USU_Nome")),
                                    reader.GetString(reader.GetOrdinal("USU_Cpf")),
                                    reader.GetString(reader.GetOrdinal("USU_Senha")),
                                    reader.GetInt32(reader.GetOrdinal("USU_Id")),
                                    reader.GetString(reader.GetOrdinal("TPU_TIPO")),
                                    reader.GetInt32(reader.GetOrdinal("TPU_Id"))
                                    ));
                            }
                        }
                    }
                }
                return usuarios;
            } catch (Exception ex) {
                return null;
            }
        }

        public Usuario SelectUsuarioPorId(int Id) {
            try {
                Usuario user = new Usuario();
                string select = "SELECT " +
                    "USU_Nome, " +
                    "TPU_TIPO, " +
                    "USU_Cpf, " +
                    "USU_Senha, " +
                    "USU_Id, " +
                    "TPU_Id " +
                    "FROM usuarios " +
                    "LEFT JOIN tipos_usuario ON USU_TPU_Id = TPU_Id " +
                    "WHERE USU_id = "+ Id + ";";

                using (MySqlConnection conn = new(StrConnection)) {
                    using (MySqlCommand query = new(select, conn)) {
                        conn.Open();
                        using (MySqlDataReader reader = query.ExecuteReader()) {
                            while (reader.Read()) {
                                user = CriarUsuario.Criar(
                                    reader.GetString(reader.GetOrdinal("USU_Nome")),
                                    reader.GetString(reader.GetOrdinal("USU_Cpf")),
                                    reader.GetString(reader.GetOrdinal("USU_Senha")),
                                    reader.GetInt32(reader.GetOrdinal("USU_Id")),
                                    reader.GetString(reader.GetOrdinal("TPU_TIPO")),
                                    reader.GetInt32(reader.GetOrdinal("TPU_Id"))
                                    );
                            }
                        }
                    }
                }
                return user;
            } catch (Exception ex) {
                return null;
            }
        }

        public List<TipoUsuario> SelectTipos() {
            string select = "SELECT * FROM TIPOS_USUARIO;";
            List<TipoUsuario> tipoUsuarios = new();

            using (MySqlConnection conn = new(StrConnection)) {
                using (MySqlCommand query = new(select, conn)) {
                    conn.Open();
                    using (MySqlDataReader reader = query.ExecuteReader()) {
                        while (reader.Read()) {
                            tipoUsuarios.Add(
                                new TipoUsuario {
                                    Id = reader.GetInt32(reader.GetOrdinal("TPU_Id")),
                                    Description = reader.GetString(reader.GetOrdinal("TPU_TIPO"))
                                }
                                );
                        }
                    }
                }
            }
            return tipoUsuarios;
        }

        public Usuario SelectUsuarioLogin(Usuario UsuarioLogin) {
            string select = "SELECT * FROM USUARIOS WHERE USU_CPF = '" + UsuarioLogin.Cpf + "' AND USU_SENHA = '" + UsuarioLogin.Senha + "';";

            Usuario usuarioAchado = new();
  
            using (MySqlConnection conn = new(StrConnection)) {
                using(MySqlCommand query = new(select, conn)) {
                    conn.Open();
                    using (MySqlDataReader reader = query.ExecuteReader()) {
                        while (reader.Read()) {
                            usuarioAchado.Cpf = reader.GetString(reader.GetOrdinal("USU_CPF"));
                            usuarioAchado.Name = reader.GetString(reader.GetOrdinal("USU_Nome"));

                        }
                    }
                }
            }
            return usuarioAchado;
        }

        public bool UpdateUsuario(Usuario usuario) {
            string Update = "UPDATE USUARIOS SET USU_Nome = '" + usuario.Name + "' WHERE USU_Id = " + usuario.Id + ";" +
                            "UPDATE USUARIOS SET USU_Cpf = '" + usuario.Cpf + "' WHERE USU_Id = " + usuario.Id + ";" +
                            "UPDATE USUARIOS SET USU_Senha = '" + usuario.Senha + "' WHERE USU_Id = " + usuario.Id + ";" +
                            "UPDATE USUARIOS SET USU_TPU_ID = " + usuario.Tipo.Id + " WHERE USU_Id = " + usuario.Id + ";";

            try {
                using (MySqlConnection conn = new(StrConnection)) {
                    using (MySqlCommand query = new(Update, conn)) {
                        conn.Open();
                        query.ExecuteNonQuery();
                    }
                }
                return true;
            }catch(MySqlException ex) {
                return false;
            }
        }
    }
}   
