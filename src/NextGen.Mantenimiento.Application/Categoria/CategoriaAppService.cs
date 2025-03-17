using Microsoft.AspNetCore.Authorization;
using NextGen.Mantenimiento.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace NextGen.Mantenimiento.Categoria
{
    public class CategoriaAppService : MantenimientoAppService, ICategoriaAppService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly CategoriaManager _categoriaManager;

        public CategoriaAppService(ICategoriaRepository categoriaRepository, CategoriaManager categoriaManager)
        {
            _categoriaRepository = categoriaRepository;
            _categoriaManager = categoriaManager;
        }

        [Authorize(MantenimientoPermissions.Departamento.Create)]
        public async Task<CategoriaDto> CreateAsync(CreateCategoriaDto input)
        {
            var categoria = await _categoriaManager.CreateAsync(
                input.Id,
                input.Nombre,
                input.Descripcion
            );

            await _categoriaRepository.InsertAsync(categoria);

            return ObjectMapper.Map<Categoria, CategoriaDto>(categoria);
        }

        [Authorize(MantenimientoPermissions.Departamento.Delete)]
        public async Task DeleteAsync(int id)
        {
            await _categoriaRepository.DeleteAsync(id);
        }

        public async Task<CategoriaDto> GetAsync(int id)
        {
            var categoria = await _categoriaRepository.GetAsync(id);
            return ObjectMapper.Map<Categoria, CategoriaDto>(categoria);
        }

        public async Task<PagedResultDto<CategoriaDto>> GetListAsync(FilterCategoriaListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Categoria.Nombre);
            }

            var categoria = await _categoriaRepository.GetListAsync(
                input.Sorting,
                input.MaxResultCount,
                input.SkipCount,
                input.Filter
            );
            var totalCount = input.Filter == null
                ? await _categoriaRepository.CountAsync()
                : await _categoriaRepository.CountAsync(
                    categoria => categoria.Nombre.Contains(input.Filter));

            return new PagedResultDto<CategoriaDto>(
                totalCount,
                ObjectMapper.Map<List<Categoria>, List<CategoriaDto>>(categoria)
            );
        }

        [Authorize(MantenimientoPermissions.Categoria.Edit)]
        public async Task UpdateAsync(int id, UpdateCategoriaDto input)
        {
            var categoria = await _categoriaRepository.GetAsync(id);

            if (categoria.Nombre != input.Nombre)
            {
                await _categoriaManager.ChangeNameAsync(categoria, input.Nombre);
            }

            categoria.Nombre = input.Nombre;
            categoria.Descripcion = input.Descripcion;

            await _categoriaRepository.UpdateAsync(categoria);
        }
    }
}
