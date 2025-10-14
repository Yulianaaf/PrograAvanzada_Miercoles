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

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("categoriaId")]
        [Required(ErrorMessage = "Debe seleccionar una categoría")]
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        [Column("precio", TypeName = "decimal(10,2)")]
        public decimal Precio { get; set; }

        [Column("impuestosPorCompra", TypeName = "decimal(10,2)")]
        public decimal ImpuestosPorCompra { get; set; }

        [Required(ErrorMessage = "El stock es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock debe ser mayor o igual a 0")]
        [Column("stock")]
        public int Stock { get; set; }

        [Column("imagenUrl")]
        public string? ImagenUrl { get; set; }

        [Column("activo")]
        public bool Activo { get; set; }

        [ForeignKey("CategoriaId")]
        public Categoria? Categoria { get; set; }

        // 🔹 Propiedad calculada (no se guarda en BD)
        [NotMapped]
        public decimal IVA { get; set; } = 13; // Porcentaje fijo (puede cambiar en la vista)

        // 🔹 Propiedad de solo lectura (total con IVA)
        [NotMapped]
        public decimal Total => Precio + ImpuestosPorCompra;
    }
}
