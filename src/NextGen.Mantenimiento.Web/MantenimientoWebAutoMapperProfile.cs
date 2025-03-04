using AutoMapper;
using NextGen.Mantenimiento.Departamento;
using NextGen.Mantenimiento.Personal;
using NextGen.Mantenimiento.PersonalDtos;

namespace NextGen.Mantenimiento.Web;

public class MantenimientoWebAutoMapperProfile : Profile
{
    public MantenimientoWebAutoMapperProfile()
    {
        //Define your object mappings here, for the Web project
        CreateMap<PersonalDto, CreateUpdatePersonalDto>();
        CreateMap<Departamento.Departamento, DepartamentoDto>();
        CreateMap<Pages.Departamentos.CreateModalModel.CreateDepartamentoViewModel, CreateDepartamentoDto>();
        CreateMap<DepartamentoDto, Pages.Departamentos.EditModalModel.EditDepartamentoViewModel>();
        CreateMap<Pages.Departamentos.EditModalModel.EditDepartamentoViewModel, UpdateDepartamentoDto>();
    }
}
