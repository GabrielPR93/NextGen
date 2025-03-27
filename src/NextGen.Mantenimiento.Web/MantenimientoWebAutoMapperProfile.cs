using AutoMapper;
using Microsoft.AspNetCore.Http;
using NextGen.Mantenimiento.Categoria;
using NextGen.Mantenimiento.Departamento;
using NextGen.Mantenimiento.Empresa;
using NextGen.Mantenimiento.Personal;
using NextGen.Mantenimiento.PersonalDtos;
using System.IO;
using static NextGen.Mantenimiento.Web.Pages.Categoria.CreateModalModel;
using static NextGen.Mantenimiento.Web.Pages.Categoria.EditModalModel;
using static NextGen.Mantenimiento.Web.Pages.Empresa.CreateModalModel;
using static NextGen.Mantenimiento.Web.Pages.Empresa.EditModalModel;

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

        CreateMap<Empresa.Empresa, EmpresaDto>();
        ///CreateMap<EmpresaDto, EditEmpresaViewModel>();
        //CreateMap<EditEmpresaViewModel, UpdateEmpresaDto>();
        CreateMap<CreateEmpresaViewModel, CreateEmpresaDto>()
        .ForMember(dest => dest.Logo, opt => opt.MapFrom(src => src.Logo != null ? GetLogoBytes(src.Logo) : null));
        CreateMap<EditEmpresaViewModel, UpdateEmpresaDto>()
        .ForMember(dest => dest.Logo, opt => opt.MapFrom(src => src.Logo != null ? GetLogoBytes(src.Logo) : null));
        CreateMap<EmpresaDto, EditEmpresaViewModel>()
      .ForMember(dest => dest.Logo, opt => opt.MapFrom(src => src.Logo != null ? new FormFile(new MemoryStream(src.Logo), 0, src.Logo.Length, null, "Logo") : null));


    }

    // Función para convertir IFormFile a byte[]
    private byte[] GetLogoBytes(IFormFile logo)
    {
        using (var ms = new MemoryStream())
        {
            logo.CopyTo(ms);
            return ms.ToArray();
        }
    }
}
