
using NextGen.Mantenimiento.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace NextGen.Mantenimiento.TipoAcreditaciones
{
    public interface ITipoAcreditacionesAppService : IApplicationService
    {
        Task<TipoAcreditacionesDto> GetAsync(int id);
        Task<PagedResultDto<TipoAcreditacionesDto>> GetListAsync(GetAllListDto input);
        Task<TipoAcreditacionesDto> CreateAsync(CreateTipoAcreditacionesDto input);
        Task UpdateAsync(int id, UpdateTipoAcreditacionesDto input);
        Task DeleteAsync(int id);
    }
}
