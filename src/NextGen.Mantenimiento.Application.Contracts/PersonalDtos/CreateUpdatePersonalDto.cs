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
        public int DepartamentoId { get; set; }

        public string NombreDepartamento { get; set; }

        [Required]
        public int CategoriaId { get; set; }

        public string NombreCategoria { get; set; }

        [Required]
        [StringLength(128)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(128)]
        public string Apellidos { get; set; }

        [Required]
        [StringLength(9, MinimumLength = 8)]
        public string Dni { get; set; }

        [Required]
        [RegularExpression(@"^\d+$")]
        [StringLength(15, MinimumLength = 9)]
        public string Telefono { get; set; }

        [Required]
        public string Direccion { get; set; }

        [Required]
        [EmailAddress]
        public string CorreoElectronico { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaAlta { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FechaBaja { get; set; }
    }

}
