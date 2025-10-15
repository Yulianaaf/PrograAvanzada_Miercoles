using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPymeStore.Models
{
    [Table("Cliente")]
    public class Cliente
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Column("nombre")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres")]
        public required string Nombre { get; set; }

        [Required(ErrorMessage = "La cédula es obligatoria")]
        [Column("cedula")]
        [StringLength(20, ErrorMessage = "La cédula no puede superar los 20 caracteres")]
        public required string Cedula { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "El formato del correo no es válido")]
        [Column("correo")]
        [StringLength(150, ErrorMessage = "El correo no puede superar los 150 caracteres")]
        public required string Correo { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [Column("telefono")]
        [StringLength(20, ErrorMessage = "El teléfono no puede superar los 20 caracteres")]
        public required string Telefono { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria")]
        [Column("direccion")]
        [StringLength(200, ErrorMessage = "La dirección no puede superar los 200 caracteres")]
        public required string Direccion { get; set; }
    }
}
