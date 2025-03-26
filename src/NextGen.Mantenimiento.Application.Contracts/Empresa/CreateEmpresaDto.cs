using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGen.Mantenimiento.Empresa
{
    public class CreateEmpresaDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(EmpresaConsts.MaxNameLength)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(EmpresaConsts.MaxNameAbrevLength)]
        public string NombreAbreviado { get; set; }

        public string Direccion { get; set; }

        [Required]
        public string Correo { get; set; }
        public byte[] Logo { get; set; }

    }
}
