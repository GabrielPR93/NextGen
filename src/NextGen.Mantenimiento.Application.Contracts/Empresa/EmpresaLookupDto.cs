using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace NextGen.Mantenimiento.Empresa
{
    public class EmpresaLookupDto : EntityDto<int>
    {
        public string Nombre { get; set; }
    }
}
