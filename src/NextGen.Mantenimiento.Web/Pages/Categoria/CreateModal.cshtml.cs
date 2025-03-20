using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NextGen.Mantenimiento.Permissions;
using System.Threading.Tasks;
using NextGen.Mantenimiento.Categoria;
using System.ComponentModel.DataAnnotations;

namespace NextGen.Mantenimiento.Web.Pages.Categoria
{
    public class CreateModalModel : MantenimientoPageModel
    {
        [BindProperty]
        public CreateCategoriaViewModel Categoria { get; set; }

        private readonly ICategoriaAppService _categoriaAppService;

        public CreateModalModel(ICategoriaAppService categoriaAppService)
        {
            _categoriaAppService = categoriaAppService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            if (!(await AuthorizationService.IsGrantedAsync(MantenimientoPermissions.Categoria.Create)))
            {
                return Redirect("/Account/AccessDenied?ReturnUrl=%2FPersonal%2FCreateModal");
            }
            Categoria = new CreateCategoriaViewModel();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!(await AuthorizationService.IsGrantedAsync(MantenimientoPermissions.Categoria.Create)))
            {
                return Redirect("/Account/AccessDenied?ReturnUrl=%2FPersonal%2FCreateModal");
            }
            var dto = ObjectMapper.Map<CreateCategoriaViewModel, CreateCategoriaDto>(Categoria);
            await _categoriaAppService.CreateAsync(dto);
            return NoContent();
        }


        public class CreateCategoriaViewModel
        {
            [System.ComponentModel.DataAnnotations.Required]
            [StringLength(CategoriaConsts.MaxNameLength)]
            public string Nombre { get; set; }
            public string? Descripcion { get; set; }
        }
    }
}
