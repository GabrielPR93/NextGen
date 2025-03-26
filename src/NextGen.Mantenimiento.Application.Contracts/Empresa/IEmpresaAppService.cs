using NextGen.Mantenimiento.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace NextGen.Mantenimiento.Empresa
{
    public interface IEmpresaAppService : IApplicationService
    {
        Task<EmpresaDto> GetAsync(int id);
        Task<PagedResultDto<EmpresaDto>> GetListAsync(GetAllListDto input);
        Task<EmpresaDto> CreateAsync(CreateEmpresaDto input);
        Task UpdateAsync(int id, UpdateEmpresaDto input);
        Task DeleteAsync(int id);
    }
}
