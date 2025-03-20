using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NextGen.Mantenimiento.Departamento;
using static NextGen.Mantenimiento.Web.Pages.Departamentos.EditModalModel;
using System.Threading.Tasks;
using NextGen.Mantenimiento.Categoria;
using System.ComponentModel.DataAnnotations;

namespace NextGen.Mantenimiento.Web.Pages.Categoria
{
    public class EditModalModel : MantenimientoPageModel
    {
        [BindProperty]
        public EditCategoriaViewModel Categoria { get; set; }

        private readonly ICategoriaAppService _categoriaAppService;

        public EditModalModel(ICategoriaAppService categoriaAppService)
        {
            _categoriaAppService = categoriaAppService;
        }
        public async Task OnGetAsync(int id)
        {

            var categoriaDto = await _categoriaAppService.GetAsync(id);
            Categoria = ObjectMapper.Map<CategoriaDto, EditCategoriaViewModel>(categoriaDto);
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _categoriaAppService.UpdateAsync(Categoria.Id, ObjectMapper.Map<EditCategoriaViewModel, UpdateCategoriaDto>(Categoria));
            return NoContent();
        }

        public class EditCategoriaViewModel
        {
            [HiddenInput]
            public int Id { get; set; }
            [System.ComponentModel.DataAnnotations.Required]
            [StringLength(CategoriaConsts.MaxNameLength)]
            public string Nombre { get; set; }
            public string? Descripcion { get; set; }
        }
    }
}
