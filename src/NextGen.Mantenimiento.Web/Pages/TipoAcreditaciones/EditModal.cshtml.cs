using Microsoft.AspNetCore.Mvc;
using NextGen.Mantenimiento.TipoAcreditaciones;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace NextGen.Mantenimiento.Web.Pages.TipoAcreditaciones
{
    public class EditModalModel : MantenimientoPageModel
    {
        [BindProperty]
        public EditTipoAcreditacionesViewModel TipoAcreditaciones { get; set; }

        private readonly ITipoAcreditacionesAppService _tipoAcreditacionesAppService;

        public EditModalModel(ITipoAcreditacionesAppService tipoAcreditacionesAppService)
        {
            _tipoAcreditacionesAppService = tipoAcreditacionesAppService;
        }
        public async Task OnGetAsync(int id)
        {

            var tipoAcreditacionesDto = await _tipoAcreditacionesAppService.GetAsync(id);
            TipoAcreditaciones = ObjectMapper.Map<TipoAcreditacionesDto, EditTipoAcreditacionesViewModel>(tipoAcreditacionesDto);
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _tipoAcreditacionesAppService.UpdateAsync(TipoAcreditaciones.Id, ObjectMapper.Map<EditTipoAcreditacionesViewModel, UpdateTipoAcreditacionesDto>(TipoAcreditaciones));
            return NoContent();
        }

        public class EditTipoAcreditacionesViewModel
        {
            [HiddenInput]
            public int Id { get; set; }
            [System.ComponentModel.DataAnnotations.Required]
            [StringLength(TipoAcreditacionesConsts.MaxNameLength)]
            public string Nombre { get; set; }

            public int? Nivel { get; set; }

            public DateTime? Duracion { get; set; }
            public string? Descripcion { get; set; }
        }
    }
}
