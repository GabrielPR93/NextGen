
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities;

namespace NextGen.Mantenimiento.Entities;

[Table("Personal")]
public class Personal : Entity<int>
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public string Apellidos { get; set; }

    public string Direccion { get; set; }

    public string CorreoElectronico { get; set; }

    public DateTime FechaAlta { get; set; }
}