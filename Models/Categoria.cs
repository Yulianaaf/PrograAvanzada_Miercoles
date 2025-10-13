using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPymeStore.Models
{
    [Table("Categoria")]
    public class Categoria
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("descripcion")]
        public string? Descripcion { get; set; }

        public ICollection<Producto>? Productos { get; set; }
    }
}
