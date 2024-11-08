using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD_Tg.Models{
    [Table("Tipos_Usuario")]
    public class TipoUsuario{
        [Column("TPU_Id")]
        public int Id { get; set; }
        [Column("TPU_Descricao")]
        public string Description { get; set; }
    }
}
