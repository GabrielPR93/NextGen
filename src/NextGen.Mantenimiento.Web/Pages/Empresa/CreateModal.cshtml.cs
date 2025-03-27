using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NextGen.Mantenimiento.Empresa;
using NextGen.Mantenimiento.Permissions;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;

namespace NextGen.Mantenimiento.Web.Pages.Empresa
{
        public class CreateModalModel : MantenimientoPageModel
        {
            [BindProperty]
            public CreateEmpresaViewModel Empresa { get; set; }

            private readonly IEmpresaAppService _empresaAppService;

            public CreateModalModel(IEmpresaAppService empresaAppService)
            {
                _empresaAppService = empresaAppService;
            }
            public async Task<IActionResult> OnGetAsync()
            {
                if (!(await AuthorizationService.IsGrantedAsync(MantenimientoPermissions.Empresa.Create)))
                {
                    return Redirect("/Account/AccessDenied?ReturnUrl=%2FPersonal%2FCreateModal");
                }
                Empresa = new CreateEmpresaViewModel();
                return Page();
            }

            public async Task<IActionResult> OnPostAsync()
            {
                if (!(await AuthorizationService.IsGrantedAsync(MantenimientoPermissions.Empresa.Create)))
                {
                    return Redirect("/Account/AccessDenied?ReturnUrl=%2FPersonal%2FCreateModal");
                }

                var dto = ObjectMapper.Map<CreateEmpresaViewModel, CreateEmpresaDto>(Empresa);
            
            if (Empresa.Logo != null)
            {
                using (var ms = new MemoryStream())
                {
                    await Empresa.Logo.CopyToAsync(ms);
                    dto.Logo = ms.ToArray();
                }
            }
            await _empresaAppService.CreateAsync(dto);
                return NoContent();
            }


            public class CreateEmpresaViewModel
            {
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

