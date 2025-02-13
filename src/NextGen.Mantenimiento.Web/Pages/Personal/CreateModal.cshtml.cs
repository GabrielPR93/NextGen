using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NextGen.Mantenimiento.PersonalDtos;
using NextGen.Mantenimiento.Web.Pages;
using System.Threading.Tasks;


namespace NextGen.Mantenimiento.Web.Pages.Personal
{
    public class CreateModalModel : PersonalPageModel
    {
        [BindProperty]
        public CreateUpdatePersonalDto Personal { get; set; }

        private readonly IPersonalAppService _personalAppService;

        public CreateModalModel(IPersonalAppService personalAppService)
        {
            _personalAppService = personalAppService;
        }

        public void OnGet()
        {
            Personal = new CreateUpdatePersonalDto();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _personalAppService.CreateAsync(Personal);
            return NoContent();
        }
    }
}

