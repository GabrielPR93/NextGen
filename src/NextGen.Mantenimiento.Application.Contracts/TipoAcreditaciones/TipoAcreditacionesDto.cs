using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace NextGen.Mantenimiento.TipoAcreditaciones
{
    public class TipoAcreditacionesDto : EntityDto<int>
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public int? Nivel { get; set; }

        public DateTime? Duracion { get; set; }

        public string? Descripcion { get; set; }
    }
 
}
