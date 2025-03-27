using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using NextGen.Mantenimiento.Empresa;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace NextGen.Mantenimiento.Web.Pages.Empresa
{
    public class EditModalModel : MantenimientoPageModel
    {
        [BindProperty]
        public EditEmpresaViewModel Empresa { get; set; }

        private readonly IEmpresaAppService _empresaAppService;

        public EditModalModel(IEmpresaAppService empresaAppService)
        {
            _empresaAppService = empresaAppService;
        }
        public async Task OnGetAsync(int id)
        {

            var empresaDto = await _empresaAppService.GetAsync(id);
            Empresa = ObjectMapper.Map<EmpresaDto, EditEmpresaViewModel>(empresaDto);
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _empresaAppService.UpdateAsync(Empresa.Id, ObjectMapper.Map<EditEmpresaViewModel, UpdateEmpresaDto>(Empresa));
            return NoContent();
        }

        public class EditEmpresaViewModel
        {
            [HiddenInput]
            public int Id { get; set; }

            [Required]
            [StringLength(EmpresaConsts.MaxNameLength)]
            public string Nombre { get; set; }

            [Required]
            [StringLength(EmpresaConsts.MaxNameAbrevLength)]
            public string NombreAbreviado { get; set; }

            public string? Direccion { get; set; }

            [Required]
            [EmailAddress]
            public string Correo { get; set; }

            public IFormFile? Logo { get; set; }
        }
    }
}
