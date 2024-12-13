using CRUD_Tg.Controllers;
using CRUD_Tg.Models;
using MySql.Data.MySqlClient;
using System.Security.Cryptography.X509Certificates;

namespace CRUD_Tg.DAO{
    public class DAOMysql : IDAO {
        public string StrConnection { get; private set; }

        public DAOMysql(string connection){
            setStringConnection(connection);
        }

        public void setStringConnection(string connection){
            StrConnection = connection; // a string devera conter: "server=;uid;pwd;database;"
        }

        public bool Insert(Usuario usuario) {
            try {
                string insert = "INSERT INTO Usuarios (USU_Nome, USU_Cpf, USU_Senha, USU_TPU_Id) VALUES (@Nome, @Cpf, @Senha, @TipoId);";

                using (MySqlConnection conn = new MySqlConnection(StrConnection)) {
                    using (MySqlCommand query = new MySqlCommand(insert, conn)) {
                        query.Parameters.AddWithValue("@Nome", usuario.Name);
                        query.Parameters.AddWithValue("@Cpf", usuario.Cpf);
                        query.Parameters.AddWithValue("@Senha", usuario.Senha);
                        query.Parameters.AddWithValue("@TipoId", usuario.Tipo.Id);

                        conn.Open();
                        query.ExecuteNonQuery();
                    }
                }

                return true;
            } catch (Exception ex) {
                return false;
            }
        }

        public List<Usuario> SelectUsuarios() {
            try {
                List<Usuario> usuarios = new List<Usuario>();
                string select = "SELECT * FROM Vw_DadosUsuario;";

                using (MySqlConnection conn = new MySqlConnection(StrConnection)) {
                    using (MySqlCommand query = new MySqlCommand(select, conn)) {
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

        public Usuario SelectUsuarioPorId(int id) {
            try {
                Usuario user = new Usuario();
                string select = "SELECT USU_Nome, TPU_TIPO, USU_Cpf, USU_Senha, USU_Id, TPU_Id FROM usuarios LEFT JOIN tipos_usuario ON USU_TPU_Id = TPU_Id WHERE USU_Id = @Id;";

                using (MySqlConnection conn = new MySqlConnection(StrConnection)) {
                    using (MySqlCommand query = new MySqlCommand(select, conn)) {
                        query.Parameters.AddWithValue("@Id", id);

                        conn.Open();
                        using (MySqlDataReader reader = query.ExecuteReader()) {
                            if (reader.Read()) {
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

            using (MySqlConnection conn = new MySqlConnection(StrConnection)) {
                using (MySqlCommand query = new MySqlCommand(select, conn)) {
                    conn.Open();
                    using (MySqlDataReader reader = query.ExecuteReader()) {
                        while (reader.Read()) {
                            tipoUsuarios.Add(new TipoUsuario {
                                Id = reader.GetInt32(reader.GetOrdinal("TPU_Id")),
                                Description = reader.GetString(reader.GetOrdinal("TPU_TIPO"))
                            });
                        }
                    }
                }
            }
            return tipoUsuarios;
        }

        public Usuario SelectUsuarioLogin(Usuario UsuarioLogin) {
            string select = "SELECT * FROM USUARIOS INNER JOIN TIPOS_USUARIO ON TPU_ID = USU_TPU_ID WHERE USU_CPF = @CPF AND USU_SENHA = @Senha;";

            Usuario usuarioAchado = new();

            using (MySqlConnection conn = new MySqlConnection(StrConnection)) {
                using (MySqlCommand query = new MySqlCommand(select, conn)) {
                    query.Parameters.AddWithValue("@CPF", UsuarioLogin.Cpf);
                    query.Parameters.AddWithValue("@Senha", UsuarioLogin.Senha);

                    conn.Open();
                    using (MySqlDataReader reader = query.ExecuteReader()) {
                        if (reader.Read()) {
                            usuarioAchado.Cpf = reader.GetString(reader.GetOrdinal("USU_CPF"));
                            usuarioAchado.Name = reader.GetString(reader.GetOrdinal("USU_Nome"));
                            usuarioAchado.Tipo.Description = reader.GetString(reader.GetOrdinal("TPU_TIPO"));
                        }
                    }
                }
            }
            return usuarioAchado;
        }

        public bool UpdateUsuario(Usuario usuario) {
            string update = "UPDATE USUARIOS SET USU_Nome = @Nome, USU_Cpf = @Cpf, USU_Senha = @Senha, USU_TPU_ID = @TipoId WHERE USU_Id = @Id;";

            try {
                using (MySqlConnection conn = new MySqlConnection(StrConnection)) {
                    using (MySqlCommand query = new MySqlCommand(update, conn)) {
                        query.Parameters.AddWithValue("@Nome", usuario.Name);
                        query.Parameters.AddWithValue("@Cpf", usuario.Cpf);
                        query.Parameters.AddWithValue("@Senha", usuario.Senha);
                        query.Parameters.AddWithValue("@TipoId", usuario.Tipo.Id);
                        query.Parameters.AddWithValue("@TipoId", usuario.Tipo.Id);
                        query.Parameters.AddWithValue("@Id", usuario.Id);

                        conn.Open();
                        query.ExecuteNonQuery();
                    }
                }
                return true;
            } catch (MySqlException ex) {
                return false;
            }
        }

        public bool DeleteUsuario(int id) {
            string delete = "DELETE FROM USUARIOS WHERE USU_ID = @Id;";

            try {
                using (MySqlConnection conn = new MySqlConnection(StrConnection)) {
                    using (MySqlCommand query = new MySqlCommand(delete, conn)) {
                        query.Parameters.AddWithValue("@Id", id);
                        conn.Open();
                        query.ExecuteNonQuery();
                    }
                }
                return true;
            }catch(Exception ex) {
                return false;
            }
        }

    }
}   
