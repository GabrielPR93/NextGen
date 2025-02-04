using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGen.Mantenimiento.PersonalDtos
{
    public class CreateUpdatePersonalDto
    {
        [Required]
        [StringLength(128)]
        public string Nombre { get; set; }
        [Required]
        public string Apellidos { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public string CorreoElectronico { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaAlta { get; set; }
    }
}
