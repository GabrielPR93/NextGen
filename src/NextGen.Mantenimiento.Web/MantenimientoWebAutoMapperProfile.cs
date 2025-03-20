using AutoMapper;
using NextGen.Mantenimiento.Categoria;
using NextGen.Mantenimiento.Departamento;
using NextGen.Mantenimiento.Personal;
using NextGen.Mantenimiento.PersonalDtos;
using static NextGen.Mantenimiento.Web.Pages.Categoria.CreateModalModel;
using static NextGen.Mantenimiento.Web.Pages.Categoria.EditModalModel;

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
        CreateMap<Pages.Personal.CreateModalModel.CreatePersonalViewModel, CreateUpdatePersonalDto>();
        CreateMap<PersonalDto, Pages.Personal.EditModalModel.EditPersonalViewModel>();
        CreateMap<Pages.Personal.EditModalModel.EditPersonalViewModel, CreateUpdatePersonalDto>();

        CreateMap<Categoria.Categoria, CategoriaDto>();
        CreateMap<CreateCategoriaViewModel, CreateCategoriaDto>();
        CreateMap<CategoriaDto, EditCategoriaViewModel>();
        CreateMap<EditCategoriaViewModel, UpdateCategoriaDto>();


    }
}
