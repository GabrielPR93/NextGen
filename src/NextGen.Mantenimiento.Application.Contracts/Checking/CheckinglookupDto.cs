using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace NextGen.Mantenimiento.Checking
{
    public class CheckinglookupDto : EntityDto<Guid> 
    {
        public string NombreUsuario { get; set; }
    }
}
