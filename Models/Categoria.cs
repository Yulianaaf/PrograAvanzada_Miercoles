using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyPymeStore.Models
{
    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }

        [Required]
        public string Nombre { get; set; }

        public ICollection<Producto> Productos { get; set; }
    }
}

