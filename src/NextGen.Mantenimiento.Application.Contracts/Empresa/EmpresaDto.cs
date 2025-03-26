using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace NextGen.Mantenimiento.Empresa
{
    public class EmpresaDto : EntityDto<int>
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string NombreAbreviado { get; set; }

        public string? Direccion { get; set; }

        public string Correo { get; set; }

        public byte[]? Logo { get; set; }


    }
}
