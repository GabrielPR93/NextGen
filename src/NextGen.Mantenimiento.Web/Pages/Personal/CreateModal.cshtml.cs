using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NextGen.Mantenimiento.Permissions;
using NextGen.Mantenimiento.PersonalDtos;
using NextGen.Mantenimiento.Web.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NextGen.Mantenimiento.Web.Pages.Personal
{
    [Authorize]
    public class CreateModalModel : MantenimientoPageModel
    {
        [BindProperty]
        public CreatePersonalViewModel Personal { get; set; }

        public List<SelectListItem> Departamentos { get; set; }

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

            Personal = new CreatePersonalViewModel();
            var departamentoLookup = await _personalAppService.GetDepartamentoLookupAsync();
            Departamentos = departamentoLookup.Items.Select(x => new SelectListItem(x.Nombre, x.Id.ToString())).ToList();
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!(await AuthorizationService.IsGrantedAsync(MantenimientoPermissions.Personal.Create)))
            {
                return Redirect("/Account/AccessDenied?ReturnUrl=%2FPersonal%2FCreateModal");
            }

            await _personalAppService.CreateAsync(
                ObjectMapper.Map<CreatePersonalViewModel, CreateUpdatePersonalDto>(Personal)
                );
            return NoContent();
        }

        public class CreatePersonalViewModel
        {
            [Required]
            public string Nombre { get; set; }
            [Required]

            public string Apellidos { get; set; }
            [Required]
          
            public string Dni { get; set; }

            [Required]
            [EmailAddress]
            public string CorreoElectronico { get; set; }

            [Required]
            [RegularExpression(@"^\d+$")]
            [StringLength(15, MinimumLength = 9)]
            public string Telefono { get; set; }

            [Required]
            public string Direccion { get; set; }
            [System.ComponentModel.DataAnnotations.Required]
            [Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form.SelectItems(nameof(Departamentos))]
            [DisplayName("Departamento")]
            public int DepartamentoId { get; set; }

            [Required]
            public int CategoriaId { get; set; }

            [Required]
            [DataType(DataType.Date)]
            public DateTime FechaNacimiento { get; set; }
            [Required]
            [DataType(DataType.Date)]
            public DateTime FechaAlta { get; set; }

            [DataType(DataType.Date)]
            public DateTime? FechaBaja { get; set; }
        }
    }
}


