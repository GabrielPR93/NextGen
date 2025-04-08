using NextGen.Mantenimiento.Categoria;
using NextGen.Mantenimiento.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace NextGen.Mantenimiento.Checking
{
    public interface ICheckingAppService : IApplicationService
    {
        Task<CheckingDto> GetAsync(Guid id);
        Task<PagedResultDto<CheckingDto>> GetListAsync(GetAllListDto input);
        Task<CheckingDto> CreateAsync(CreateCheckingDto input);

        Task<CheckingDto> GetLastOpenCheckingAsync();

        Task <CheckingDto>UpdateAsync(Guid id, UpdateCheckingDto input);
        Task DeleteAsync(Guid id);
    }
}
