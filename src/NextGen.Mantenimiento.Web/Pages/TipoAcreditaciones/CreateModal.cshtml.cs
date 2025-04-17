using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NextGen.Mantenimiento.TipoAcreditaciones;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace NextGen.Mantenimiento.Web.Pages.TipoAcreditaciones
{
    public class CreateModalModel : MantenimientoPageModel
    {
        [BindProperty]
        public CreateTipoAcreditacionesViewModel TipoAcreditaciones { get; set; }
        private readonly ITipoAcreditacionesAppService _tipoAcreditacionesAppService;
        public CreateModalModel(ITipoAcreditacionesAppService tipoAcreditacionesAppService)
        {
            _tipoAcreditacionesAppService = tipoAcreditacionesAppService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            TipoAcreditaciones = new CreateTipoAcreditacionesViewModel();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateTipoAcreditacionesViewModel, CreateTipoAcreditacionesDto>(TipoAcreditaciones);
            await _tipoAcreditacionesAppService.CreateAsync(dto);
            return NoContent();
        }
        public class CreateTipoAcreditacionesViewModel
        {
            [System.ComponentModel.DataAnnotations.Required]
            [StringLength(TipoAcreditacionesConsts.MaxNameLength)]
            public string Nombre { get; set; }

            public int? Nivel { get; set; }

            public DateTime? Duracion { get; set; }
            public string? Descripcion { get; set; }
        }
    }

}
