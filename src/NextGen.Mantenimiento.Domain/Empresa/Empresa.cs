using System;
using System.Collections.Generic;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace NextGen.Mantenimiento.Empresa;

public class Empresa : Entity<int>
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public string NombreAbreviado { get; set; }

    public string? Direccion { get; set; }

    public string Correo { get; set; }

    public byte[]? Logo { get; set; }

    public Empresa()
    {

    }

    internal Empresa(int id, string nombre, string nombreAbreviado, string direccion, string correo, byte[] logo) : base(id)
    {
        Id = id;
        SetName(nombre);
        SetNameAbreviado(nombreAbreviado);
        SetDireccion(direccion);
        SetCorreo(correo);
        Logo = logo;
    }

    internal Empresa ChangeName(string nombre)
    {
        SetName(nombre);
        return this;
    }

    internal Empresa ChangeNameAbreviado(string nombreAbreviado)
    {
        SetNameAbreviado(nombreAbreviado);
        return this;
    }

    internal Empresa ChangeDireccion(string direccion)
    {
        SetDireccion(direccion);
        return this;
    }

    internal Empresa ChangeCorreo(string correo)
    {
        SetCorreo(correo);
        return this;
    }

    private void SetName(string nombre)
    {
        Nombre = Check.NotNullOrWhiteSpace(nombre, nameof(nombre), maxLength: EmpresaConsts.MaxNameLength);
    }

    private void SetNameAbreviado(string nombreAbreviado)
    {
        NombreAbreviado = Check.NotNullOrWhiteSpace(nombreAbreviado, nameof(nombreAbreviado), maxLength: EmpresaConsts.MaxNameAbrevLength).ToUpperInvariant();
    }

    private void SetDireccion(string direccion)
    {
        if (direccion != null) { 

            Direccion = Check.NotNullOrWhiteSpace(direccion, nameof(direccion), maxLength: EmpresaConsts.MaxDireccionLength);
        }
        else
        {
            Direccion = null; 
        }
    }

    private void SetCorreo(string correo)
    {
        Correo = Check.NotNullOrWhiteSpace(correo, nameof(correo));
    }
}