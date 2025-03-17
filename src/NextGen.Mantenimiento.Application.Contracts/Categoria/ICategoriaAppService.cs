using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Application.Dtos;

namespace NextGen.Mantenimiento.Categoria
{
    public interface ICategoriaAppService : IApplicationService
    {
        Task<CategoriaDto> GetAsync(int id);
        Task<PagedResultDto<CategoriaDto>> GetListAsync(FilterCategoriaListDto input);
        Task<CategoriaDto> CreateAsync(CreateCategoriaDto input);
        Task UpdateAsync(int id, UpdateCategoriaDto input);
        Task DeleteAsync(int id);
    }
}
