
using System;
using System.Collections.Generic;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace NextGen.Mantenimiento.Categoria;

public class Categoria : Entity<int>
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public string? Descripcion { get; set; }

    public Categoria()
    {
        
    }

    internal Categoria(int id, string nombre, string descripcion) : base(id)
    {
        Id = id;
        SetName(nombre);
        Descripcion = descripcion ?? string.Empty;
    }

    internal Categoria ChangeName(string nombre)
    {
        SetName(nombre);
        return this;
    }

    private void SetName(string nombre)
    {
        Nombre = Check.NotNullOrWhiteSpace(nombre, nameof(nombre), maxLength: CategoriaConsts.MaxNameLength);
    }
}