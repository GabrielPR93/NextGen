using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGen.Mantenimiento.Categoria
{
    public class UpdateCategoriaDto
    {
        [Required]
        [StringLength(CategoriaConsts.MaxNameLength)]
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
    }
}
