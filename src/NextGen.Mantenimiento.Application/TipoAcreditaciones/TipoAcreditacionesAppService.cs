using NextGen.Mantenimiento.Filter;
using Microsoft.AspNetCore.Authorization;
using NextGen.Mantenimiento.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace NextGen.Mantenimiento.TipoAcreditaciones
{
    [RemoteService]
    [Microsoft.AspNetCore.Authorization.Authorize(MantenimientoPermissions.TipoAcreditaciones.Default)]
    public class TipoAcreditacionesAppService : MantenimientoAppService, ITipoAcreditacionesAppService
    {
        private readonly ITipoAcreditacionesRepository _tipoAcreditacionesRepository;
        private readonly TipoAcreditacionesManager _tipoAcreditacionesManager;

        public TipoAcreditacionesAppService(
            ITipoAcreditacionesRepository tipoAcreditacionesRepository,
            TipoAcreditacionesManager tipoAcreditacionesManager)
        {
            _tipoAcreditacionesRepository = tipoAcreditacionesRepository;
            _tipoAcreditacionesManager = tipoAcreditacionesManager;
        }

        public async Task<TipoAcreditacionesDto> GetAsync(int id)
        {
            var tipoAcreditaciones = await _tipoAcreditacionesRepository.GetAsync(id);
            return ObjectMapper.Map<TipoAcreditaciones, TipoAcreditacionesDto>(tipoAcreditaciones);
        }

        public async Task<PagedResultDto<TipoAcreditacionesDto>> GetListAsync(GetAllListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(TipoAcreditaciones.Nombre);
            }

            var tipoAcreditaciones = await _tipoAcreditacionesRepository.GetListAsync(
                input.Sorting,
                input.MaxResultCount,
                input.SkipCount,
                input.Filter
            );

            var totalCount = input.Filter == null
                ? await _tipoAcreditacionesRepository.CountAsync()
                : await _tipoAcreditacionesRepository.CountAsync(
                    tipoAcreditaciones => tipoAcreditaciones.Nombre.Contains(input.Filter));

            return new PagedResultDto<TipoAcreditacionesDto>(
                totalCount,
                ObjectMapper.Map<List<TipoAcreditaciones>, List<TipoAcreditacionesDto>>(tipoAcreditaciones)
            );
        }

        [Microsoft.AspNetCore.Authorization.Authorize(MantenimientoPermissions.TipoAcreditaciones.Create)]
        public async Task<TipoAcreditacionesDto> CreateAsync(CreateTipoAcreditacionesDto input)
        {
            var tipoAcreditaciones = await _tipoAcreditacionesManager.CreateAsync(
                input.Id,
                input.Nombre,
                input.Nivel,
                input.Duracion,
                input.Descripcion
            );

            await _tipoAcreditacionesRepository.InsertAsync(tipoAcreditaciones);

            return ObjectMapper.Map<TipoAcreditaciones, TipoAcreditacionesDto>(tipoAcreditaciones);
        }

        [Microsoft.AspNetCore.Authorization.Authorize(MantenimientoPermissions.TipoAcreditaciones.Edit)]
        public async Task UpdateAsync(int id, UpdateTipoAcreditacionesDto input)
        {
            var tipoAcreditaciones = await _tipoAcreditacionesRepository.GetAsync(id);

            if (tipoAcreditaciones.Nombre != input.Nombre)
            {
                await _tipoAcreditacionesManager.ChangeNameAsync(tipoAcreditaciones, input.Nombre);
            }

            tipoAcreditaciones.Nombre = input.Nombre;
            tipoAcreditaciones.Nivel = input.Nivel;
            tipoAcreditaciones.Duracion = input.Duracion;
            tipoAcreditaciones.Descripcion = input.Descripcion;

            await _tipoAcreditacionesRepository.UpdateAsync(tipoAcreditaciones);
        }

        [Microsoft.AspNetCore.Authorization.Authorize(MantenimientoPermissions.TipoAcreditaciones.Delete)]
        public async Task DeleteAsync(int id)
        {
            await _tipoAcreditacionesRepository.DeleteAsync(id);
        }
    }
}
