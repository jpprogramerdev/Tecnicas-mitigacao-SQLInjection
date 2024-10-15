using CRUD_Tg.Controllers;
using CRUD_Tg.Models;
using MySql.Data.MySqlClient;

namespace CRUD_Tg.DAO{
    public class DAOMysql : IDAO {
        public string StrConnection { get; private set; }
        public MySqlConnection conexao { get; private set; }

        public DAOMysql(){
            setStringConnection("server=localhost;uid=root;pwd=1234;database=CRUD_TG");
        }

        public void ConnectionDataBase(){
            try{
                conexao = new MySqlConnection(StrConnection);
            }catch(Exception e){
                Console.WriteLine("Erro para setar a conexao");
            }
        }

        public void setStringConnection(string connection){
            StrConnection = connection; // a string devera conter: "server=;uid;pwd;database;"
        }


        public void OpenConnection(){
            conexao.Open() ;
        }

        public bool Insert(Usuario usuario){
            try{
                string SqlString = "INSERT INTO Usuarios (USU_Nome, USU_Cpf, USU_Senha, USU_TPU_Id) values('" + usuario.Name + "','" + usuario.Cpf + "','" + usuario.Senha + "', " + usuario.Tipo.Id + ");";
                ConnectionDataBase();
                OpenConnection();
                MySqlCommand cmd = new(SqlString, conexao);
                cmd.ExecuteNonQuery();
                return true;
            }catch(Exception ex) {
                return false;
            }
        }

        public List<Usuario> SelectUsuarios() {
            try {
                List<Usuario> usuarios = new List<Usuario>();
                string query = "SELECT USU_Nome, TPU_TIPO, USU_Cpf, USU_Senha, USU_Id, TPU_Id FROM usuarios LEFT JOIN tipos_usuario ON USU_TPU_Id = TPU_Id;";
                ConnectionDataBase();
                OpenConnection();
                using (MySqlCommand cmd = new(query, conexao)) {
                    using (MySqlDataReader reader = cmd.ExecuteReader()) {
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
                return usuarios;
            } catch (Exception ex) {
                return null;
            }
        }

        public Usuario SelectUsuarioPorId(int Id) {
            try {
                Usuario user = new Usuario();
                string query = "SELECT " +
                    "USU_Nome, " +
                    "TPU_TIPO, " +
                    "USU_Cpf, " +
                    "USU_Senha, " +
                    "USU_Id, " +
                    "TPU_Id " +
                    "FROM usuarios " +
                    "LEFT JOIN tipos_usuario ON USU_TPU_Id = TPU_Id " +
                    "WHERE USU_id = "+ Id + ";";
                ConnectionDataBase();
                OpenConnection();
                using (MySqlCommand cmd = new(query, conexao)) {
                    using (MySqlDataReader reader = cmd.ExecuteReader()) {
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
                return user;
            } catch (Exception ex) {
                return null;
            }
        }

        public List<TipoUsuario> SelectTipos() {
            string select = "SELECT * FROM TIPOS_USUARIO;";
            List<TipoUsuario> tipoUsuarios = new();
            
            ConnectionDataBase();
            OpenConnection();
            using (MySqlCommand cmd = new(select, conexao)) {
                using (MySqlDataReader reader = cmd.ExecuteReader()) {
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
            return tipoUsuarios;
        }

        public bool UpdateUsuario(Usuario usuario) {
            string Update = "UPDATE USUARIOS SET USU_Nome = '" + usuario.Name + "' WHERE USU_Id = " + usuario.Id + ";" +
                            "UPDATE USUARIOS SET USU_Cpf = '" + usuario.Cpf + "' WHERE USU_Id = " + usuario.Id + ";" +
                            "UPDATE USUARIOS SET USU_Senha = '" + usuario.Senha + "' WHERE USU_Id = " + usuario.Id + ";" +
                            "UPDATE USUARIOS SET USU_TPU_ID = " + usuario.Tipo.Id + " WHERE USU_Id = " + usuario.Id + ";";

            try {
                ConnectionDataBase();
                OpenConnection();
                MySqlCommand cmd = new(Update, conexao);
                cmd.ExecuteNonQuery();
                return true;
            }catch(MySqlException ex) {
                return false;
            }
        }
    }
}   
