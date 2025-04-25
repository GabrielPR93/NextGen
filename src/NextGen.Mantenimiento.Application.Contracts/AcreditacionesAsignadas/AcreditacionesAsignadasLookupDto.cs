using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace NextGen.Mantenimiento.AcreditacionesAsignadas
{
    public class AcreditacionesAsignadasLookupDto: EntityDto<int>
    {
        public string Nombre { get; set; }
    }
}
