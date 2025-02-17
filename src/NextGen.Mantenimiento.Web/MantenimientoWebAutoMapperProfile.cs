using AutoMapper;
using NextGen.Mantenimiento.Personal;
using NextGen.Mantenimiento.PersonalDtos;

namespace NextGen.Mantenimiento.Web;

public class MantenimientoWebAutoMapperProfile : Profile
{
    public MantenimientoWebAutoMapperProfile()
    {
        //Define your object mappings here, for the Web project
        CreateMap<PersonalDto, CreateUpdatePersonalDto>();
    }
}
