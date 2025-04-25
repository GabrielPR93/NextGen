using NextGen.Mantenimiento.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace NextGen.Mantenimiento.AcreditacionesAsignadas
{
    public interface IAcreditacionesAsignadasAppService : IApplicationService
    {
        Task<PagedResultDto<AcreditacionesAsignadasDto>> GetListAsync(GetAllListDto input);
        Task<AcreditacionesAsignadasDto> GetByIdAsync(int id);
        Task<AcreditacionesAsignadasDto> CreateAsync(CreateUpdateAcreditacionesAsignadasDto input);
        Task UpdateAsync(int id, CreateUpdateAcreditacionesAsignadasDto input);
        Task DeleteAsync(int id);
    }
}
