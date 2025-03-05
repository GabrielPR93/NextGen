using AutoMapper;

using NextGen.Mantenimiento.Entities;
using Volo.Abp.AutoMapper;
using NextGen.Mantenimiento.PersonalDtos;
using NextGen.Mantenimiento.Personal;
using NextGen.Mantenimiento.Departamento;

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
    }
}
