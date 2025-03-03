using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Application.Dtos;

namespace NextGen.Mantenimiento.Departamento
{
    public interface IDepartamentoAppService : IApplicationService
    {
        Task<DepartamentoDto> GetAsync(int id);
        Task<PagedResultDto<DepartamentoDto>> GetListAsync(GetDepartamentoListDto input);
        Task<DepartamentoDto> CreateAsync(CreateDepartamentoDto input);
        Task UpdateAsync(int id, UpdateDepartamentoDto input);
        Task DeleteAsync(int id);
    }
}
