using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace NextGen.Mantenimiento.AcreditacionesAsignadas
{
    public class AcreditacionesAsignadasDto : EntityDto<int>
    {
        public int Id { get; set; }
        public int PersonalId { get; set; }
        public int? EmpresaId { get; set; }
        public int AcreditacionId { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime? FechaCaducidad { get; set; }
    }

}
