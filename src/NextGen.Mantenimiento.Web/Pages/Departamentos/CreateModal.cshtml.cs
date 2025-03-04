using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NextGen.Mantenimiento.Departamento;
using NextGen.Mantenimiento.Permissions;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace NextGen.Mantenimiento.Web.Pages.Departamentos
{
    public class CreateModalModel : MantenimientoPageModel
    {
        [BindProperty]
        public CreateDepartamentoViewModel Departamento { get; set; }

        private readonly IDepartamentoAppService _departamentoAppService;

        public CreateModalModel(IDepartamentoAppService departamentoAppService)
        {
            _departamentoAppService = departamentoAppService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            if (!(await AuthorizationService.IsGrantedAsync(MantenimientoPermissions.Departamento.Create)))
            {
                return Redirect("/Account/AccessDenied?ReturnUrl=%2FPersonal%2FCreateModal");
            }
            Departamento = new CreateDepartamentoViewModel();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!(await AuthorizationService.IsGrantedAsync(MantenimientoPermissions.Departamento.Create)))
            {
                return Redirect("/Account/AccessDenied?ReturnUrl=%2FPersonal%2FCreateModal");
            }
            var dto = ObjectMapper.Map<CreateDepartamentoViewModel, CreateDepartamentoDto>(Departamento);
            await _departamentoAppService.CreateAsync(dto);
            return NoContent();
        }

        public class CreateDepartamentoViewModel
        {
            [System.ComponentModel.DataAnnotations.Required]
            [StringLength(DepartamentoConsts.MaxNameLength)]
            public string Nombre { get; set; }

            [System.ComponentModel.DataAnnotations.Required]
            [StringLength(DepartamentoConsts.MaxabreviateNameLength)]
            public string NombreAbreviado { get; set; }
        }
    }
}
