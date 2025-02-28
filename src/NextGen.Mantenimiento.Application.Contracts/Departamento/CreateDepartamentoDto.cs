using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGen.Mantenimiento.Departamento
{
    public class CreateDepartamentoDto
    {
        [Required]
        public string Nombre { get; set; }

        [Required]
        [StringLength(DepartamentoConsts.MaxNameLength)]
        public string NombreAbreviado { get; set; } = string.Empty;
    }
}
