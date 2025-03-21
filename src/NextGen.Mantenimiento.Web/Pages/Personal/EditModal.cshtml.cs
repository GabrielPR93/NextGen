using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NextGen.Mantenimiento.Departamento;
using NextGen.Mantenimiento.Personal;
using NextGen.Mantenimiento.PersonalDtos;
using Microsoft.AspNetCore.Localization;
using NextGen.Mantenimiento.Localization;



namespace NextGen.Mantenimiento.Web.Pages.Personal
{
    public class EditModalModel : MantenimientoPageModel
    {
       
        [BindProperty]
        public EditPersonalViewModel Personal { get; set; }

        public List<SelectListItem> Departamentos { get; set; }

        public List<SelectListItem> Categorias { get; set; }

        private readonly IPersonalAppService _personalAppService;

        public EditModalModel(IPersonalAppService personalAppService)
        {
            _personalAppService = personalAppService;
        
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {

            var personalDto = await _personalAppService.GetAsync(id);
            Personal = ObjectMapper.Map<PersonalDto, EditPersonalViewModel>(personalDto);

            var departamentoLookup = await _personalAppService.GetDepartamentoLookupAsync();
            Departamentos = departamentoLookup.Items
                .Select(x => new SelectListItem(x.Nombre, x.Id.ToString()))
                .ToList();
            var categoriaLookup = await _personalAppService.GetCategoriaLookupAsync();
            Categorias = categoriaLookup.Items
                .Select(x => new SelectListItem(x.Nombre, x.Id.ToString()))
                .ToList();
            return Page(); // Devuelve correctamente la página
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _personalAppService.UpdateAsync(
           Personal.Id,
           ObjectMapper.Map<EditPersonalViewModel, CreateUpdatePersonalDto>(Personal)
       );

            return NoContent();
        }
        public class EditPersonalViewModel
        {
            
            [Required]
            [HiddenInput]
            public int Id { get; set; }
            [Required]
            public string Nombre { get; set; }
            [Required]

            public string Apellidos { get; set; }
            [Required]
            [StringLength(9, MinimumLength = 8)]

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
            [Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form.SelectItems(nameof(Categorias))]
            [DisplayName("Categorias")]
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
    


