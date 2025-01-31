
using System;
using System.Collections.Generic;

namespace NextGen.Mantenimiento.Entities;

public partial class Personal
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public string Apellidos { get; set; }

    public string Direccion { get; set; }

    public string CorreoElectronico { get; set; }

    public DateTime FechaAlta { get; set; }
}