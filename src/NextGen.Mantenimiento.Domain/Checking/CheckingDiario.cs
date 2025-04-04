using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace NextGen.Mantenimiento.Checking;

public class CheckingDiario : Entity<Guid>
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public DateTime HoraEntrada { get; set; }

    public DateTime? HoraSalida { get; set; }

    public DateTime HoraCreacion { get; set; }

    public string NombreUsuario { get; set; }

    public string? Nombre { get; set; }

    public string? Apellidos { get; set; }

    public CheckingDiario()
    {
    }

    internal CheckingDiario(Guid id, Guid userId, DateTime horaEntrada, DateTime? horaSalida, DateTime horaCreacion, string nombreUsuario, string nombre, string apellidos) : base(id)
    {
        Id = id;
        UserId = userId;
        HoraEntrada = horaEntrada;
        HoraSalida = horaSalida;
        HoraCreacion = horaCreacion;
        NombreUsuario = nombreUsuario;
        Nombre = nombre;
        Apellidos = apellidos;
    }
}