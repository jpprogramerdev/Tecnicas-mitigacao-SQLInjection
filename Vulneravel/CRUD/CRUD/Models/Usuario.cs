using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD_Tg.Models{
    [Table("Usuarios")]
    public class Usuario{
        
        public Usuario() {
            Tipo = new();
        }

        [Key]
        [Column("USU_Id")]
        public int Id { get; set; }
        [Column("USU_Name")]
        public string Name { get; set; }
        [Column("USU_Senha")]
        public string Senha { get; set; }
        [Column("USU_Cpf")]
        public string Cpf { get; set; }
        [ForeignKey("USU_TPU_ID")]
        public TipoUsuario Tipo { get; set; }
    }
}
