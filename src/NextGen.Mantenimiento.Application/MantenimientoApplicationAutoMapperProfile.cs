using AutoMapper;

using NextGen.Mantenimiento.Entities;
using Volo.Abp.AutoMapper;
using NextGen.Mantenimiento.PersonalDtos;
using NextGen.Mantenimiento.Personal;

namespace NextGen.Mantenimiento;

public class MantenimientoApplicationAutoMapperProfile : Profile
{
    public MantenimientoApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<NextGen.Mantenimiento.Entities.Personal, PersonalDto>()
            .IgnoreAuditedObjectProperties();
        CreateMap<CreateUpdatePersonalDto, NextGen.Mantenimiento.Entities.Personal>();
    }
}
