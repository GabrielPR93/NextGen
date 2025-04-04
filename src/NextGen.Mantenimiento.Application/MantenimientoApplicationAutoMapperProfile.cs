using AutoMapper;

using NextGen.Mantenimiento.Entities;
using Volo.Abp.AutoMapper;
using NextGen.Mantenimiento.PersonalDtos;
using NextGen.Mantenimiento.Personal;
using NextGen.Mantenimiento.Departamento;
using NextGen.Mantenimiento.Categoria;
using NextGen.Mantenimiento.Empresa;
using NextGen.Mantenimiento.Checking;

namespace NextGen.Mantenimiento;

public class MantenimientoApplicationAutoMapperProfile : Profile
{
    public MantenimientoApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Entities.Personal, PersonalDto>()
            .IgnoreAuditedObjectProperties();
        CreateMap<CreateUpdatePersonalDto, Entities.Personal>();

        CreateMap<Departamento.Departamento, DepartamentoDto>();
        CreateMap<Departamento.Departamento, DepartamentoLookupDto>();
        CreateMap<Categoria.Categoria, Categoria.CategoriaLookupDto>();
        CreateMap<Categoria.Categoria, CategoriaDto>();
        CreateMap<Empresa.Empresa, EmpresaLookupDto>();
        CreateMap<Empresa.Empresa, EmpresaDto>();

        CreateMap<CheckingDiario, CheckingDto>();
        CreateMap<CheckingDiario, CheckinglookupDto>();

    }
}
