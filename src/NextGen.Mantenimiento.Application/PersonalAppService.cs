using NextGen.Mantenimiento.Permissions;
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
    public class PersonalAppService : CrudAppService<Entities.Personal, PersonalDto, int, PagedAndSortedResultRequestDto, CreateUpdatePersonalDto>, IPersonalAppService
    {
        public PersonalAppService(IRepository<Entities.Personal, int> repository) : base(repository)
        {
            GetPolicyName = MantenimientoPermissions.Personal.Default;
            GetListPolicyName = MantenimientoPermissions.Personal.Default;
            CreatePolicyName = MantenimientoPermissions.Personal.Create;
            UpdatePolicyName = MantenimientoPermissions.Personal.Edit;
            DeletePolicyName = MantenimientoPermissions.Personal.Delete;
        }
    }
}
