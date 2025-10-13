using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPymeStore.Models
{
    [Table("Producto")]
    public class Producto
    {
        [Key]
        [Column("id")]  
        public int Id { get; set; }

        [Required]
        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("categoriaId")]
        public int CategoriaId { get; set; }

        [Required]
        [Column("precio", TypeName = "decimal(10,2)")]
        public decimal Precio { get; set; }

        [Column("impuestosPorCompra", TypeName = "decimal(10,2)")]
        public decimal ImpuestosPorCompra { get; set; }

        [Required]
        [Column("stock")]
        public int Stock { get; set; }

        [Column("imagenUrl")]
        public string? ImagenUrl { get; set; }

        [Column("activo")]
        public bool Activo { get; set; }

        
        [ForeignKey("CategoriaId")]
        public Categoria? Categoria { get; set; }
    }
}
