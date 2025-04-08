using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGen.Mantenimiento.Checking
{
    public class CreateCheckingDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime HoraEntrada { get; set; }
        public DateTime? HoraSalida { get; set; }
        public DateTime HoraCreacion { get; set; }

        public string NombreUsuario { get; set; }
        public string? Nombre { get; set; }
        public string? Apellidos { get; set; }

    }
}
