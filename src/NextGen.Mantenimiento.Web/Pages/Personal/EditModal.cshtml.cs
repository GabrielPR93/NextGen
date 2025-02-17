using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NextGen.Mantenimiento.Personal;
using NextGen.Mantenimiento.PersonalDtos;

namespace NextGen.Mantenimiento.Web.Pages.Personal
{
    public class EditModalModel : PersonalPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]

        public int Id { get; set; }
       
        [BindProperty]
        public CreateUpdatePersonalDto Personal { get; set; } 

        private readonly IPersonalAppService _personalAppService;

        public EditModalModel(IPersonalAppService personalAppService)
        {
            _personalAppService = personalAppService;
          
        }

        public async Task<IActionResult> OnGetAsync()
        {
         
            if (Id <= 0)
            {
                return BadRequest("ID inválido.");
            }

            var personalDto = await _personalAppService.GetAsync(Id);
            if (personalDto == null)
            {
                return NotFound($"No se encontró el empleado con ID {Id}.");
            }
           
            Personal = ObjectMapper.Map<PersonalDto, CreateUpdatePersonalDto>(personalDto);
            return Page(); // Devuelve correctamente la página
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page(); // Devuelve el formulario con errores si la validación falla
            }

            if (Id <= 0)
            {
                return BadRequest("ID inválido.");
            }

            await _personalAppService.UpdateAsync(Id, Personal);
            return NoContent(); // actualización exitosa
        }
    }
}


