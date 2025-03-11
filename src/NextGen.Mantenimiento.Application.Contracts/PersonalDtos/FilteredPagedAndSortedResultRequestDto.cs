using Volo.Abp.Application.Dtos;

namespace NextGen.Mantenimiento.PersonalDtos
{
    public class FilteredPagedAndSortedResultRequestDto : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
    }
}

