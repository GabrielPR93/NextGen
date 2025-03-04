using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NextGen.Mantenimiento.Permissions;
using NextGen.Mantenimiento.PersonalDtos;
using NextGen.Mantenimiento.Web.Pages;
using System.Threading.Tasks;

namespace NextGen.Mantenimiento.Web.Pages.Personal
{
    [Authorize]
    public class CreateModalModel : PersonalPageModel
    {
        [BindProperty]
        public CreateUpdatePersonalDto Personal { get; set; }

        private readonly IPersonalAppService _personalAppService;

        public CreateModalModel(IPersonalAppService personalAppService)
        {
            _personalAppService = personalAppService;
        }

        public async Task<IActionResult> OnGet()
        {
            // Verifica si el usuario tiene el permiso correcto
            if (!(await AuthorizationService.IsGrantedAsync(MantenimientoPermissions.Personal.Create)))
            {
                return Redirect("/Account/AccessDenied?ReturnUrl=%2FPersonal%2FCreateModal");
            }

            Personal = new CreateUpdatePersonalDto();
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!(await AuthorizationService.IsGrantedAsync(MantenimientoPermissions.Personal.Create)))
            {
                return Redirect("/Account/AccessDenied?ReturnUrl=%2FPersonal%2FCreateModal");
            }

            await _personalAppService.CreateAsync(Personal);
            return NoContent();
        }
    }
}


