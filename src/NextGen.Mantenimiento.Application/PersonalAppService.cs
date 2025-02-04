using NextGen.Mantenimiento.Personal;
using NextGen.Mantenimiento.PersonalDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;


namespace NextGen.Mantenimiento
{
    public class PersonalAppService : CrudAppService<NextGen.Mantenimiento.Entities.Personal, PersonalDto, int, PagedAndSortedResultRequestDto, CreateUpdatePersonalDto>, IPersonalAppService
    {
        public PersonalAppService(IRepository<Entities.Personal, int> repository) : base(repository)
        {
        }
    }
}
