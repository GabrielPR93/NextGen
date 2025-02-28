using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace NextGen.Mantenimiento.Departamento
{
    public class GetDepartamentoListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
