using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace NextGen.Mantenimiento.Departamento
{
    public interface IDepartamentoAppService : IApplicationService
    {
        Task<DepartamentoDto> GetAsync(int id);
        Task<PagedResultDto<DepartamentoDto>> GetListAsync(GetDepartamentosInput input);
        Task<DepartamentoDto> CreateAsync(CreateUpdateDepartamentoDto input);
        Task UpdateAsync(int id, CreateUpdateDepartamentoDto input);
        Task DeleteAsync(int id);
    }
}
