using Microsoft.VisualBasic;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace NextGen.Mantenimiento.TipoAcreditaciones;

public class TipoAcreditaciones : Entity<int>
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public int? Nivel { get; set; }

    public DateTime? Duracion { get; set; }

    public string? Descripcion { get; set; }

    public TipoAcreditaciones()
    {

    }

    internal TipoAcreditaciones(int id, string nombre, int? nivel, DateTime? duracion, string? descripcion) : base(id)
    {
        Id = id;
        SetName(nombre);
        Nivel = nivel;
        Duracion = duracion;
        Descripcion = descripcion ?? string.Empty;
    }

    internal TipoAcreditaciones ChangeName(string nombre)
    {
        SetName(nombre);
        return this;
    }

    private void SetName(string nombre)
    {
        Nombre = Check.NotNullOrWhiteSpace(nombre, nameof(nombre), maxLength: TipoAcreditacionesConsts.MaxNameLength);
    }
}