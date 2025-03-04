using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NextGen.Mantenimiento.Departamento;
using NextGen.Mantenimiento.Permissions;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using System.Threading.Tasks;
using NextGen.Mantenimiento.Personal;
using NextGen.Mantenimiento.PersonalDtos;

namespace NextGen.Mantenimiento.Web.Pages.Departamentos
{
    public class EditModalModel : MantenimientoPageModel
    {
        [BindProperty]
        public EditDepartamentoViewModel Departamento { get; set; }

        private readonly IDepartamentoAppService _departamentoAppService;

        public EditModalModel(IDepartamentoAppService departamentoAppService)
        {
            _departamentoAppService = departamentoAppService;
        }
        public async Task OnGetAsync(int id)
        {
      
            var departamentoDto = await _departamentoAppService.GetAsync(id);
            Departamento = ObjectMapper.Map<DepartamentoDto, EditDepartamentoViewModel>(departamentoDto);
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _departamentoAppService.UpdateAsync(Departamento.Id, ObjectMapper.Map<EditDepartamentoViewModel, UpdateDepartamentoDto>(Departamento));
            return NoContent();
        }

        public class EditDepartamentoViewModel
        {

            [HiddenInput]
            public int Id { get; set; }

            [System.ComponentModel.DataAnnotations.Required]
            [StringLength(DepartamentoConsts.MaxNameLength)]
            public string Nombre { get; set; }

            [System.ComponentModel.DataAnnotations.Required]
            [StringLength(DepartamentoConsts.MaxabreviateNameLength)]
            public string NombreAbreviado { get; set; }
        }
    }
}

