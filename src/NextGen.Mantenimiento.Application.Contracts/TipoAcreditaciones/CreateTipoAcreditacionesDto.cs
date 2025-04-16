using NextGen.Mantenimiento.Categoria;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGen.Mantenimiento.TipoAcreditaciones
{
    public class CreateTipoAcreditacionesDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(TipoAcreditacionesConsts.MaxNameLength)]
        public string Nombre { get; set; }

        public int? Nivel { get; set; }

        public DateTime? Duracion { get; set; }

        public string? Descripcion { get; set; }
    }
}
