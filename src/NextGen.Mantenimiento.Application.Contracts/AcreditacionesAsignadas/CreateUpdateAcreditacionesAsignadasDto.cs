using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGen.Mantenimiento.AcreditacionesAsignadas
{
    public class CreateUpdateAcreditacionesAsignadasDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int PersonalId { get; set; }
        public int? EmpresaId { get; set; }
        [Required]
        public int AcreditacionId { get; set; }
        [Required]
        public DateTime FechaEmision { get; set; }
        public DateTime? FechaCaducidad { get; set; }
    }
}
