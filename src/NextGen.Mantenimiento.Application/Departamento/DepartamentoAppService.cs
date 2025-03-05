using Microsoft.AspNetCore.Authorization;
using NextGen.Mantenimiento.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace NextGen.Mantenimiento.Departamento

{
    [RemoteService]
    [Authorize(MantenimientoPermissions.Departamento.Default)]
    public class DepartamentoAppService :MantenimientoAppService ,IDepartamentoAppService
    {
        private readonly IDepartamentoRepository _departamentoRepository;
        private readonly DepartamentoManager _departamentoManager;

        public DepartamentoAppService(
            IDepartamentoRepository departamentoRepository,
            DepartamentoManager departamentoManager)
        {
            _departamentoRepository = departamentoRepository;
            _departamentoManager = departamentoManager;
        }

        public async Task<DepartamentoDto> GetAsync(int id)
        {
            var departamento = await _departamentoRepository.GetAsync(id);
            return ObjectMapper.Map<Departamento, DepartamentoDto>(departamento);
        }

        public async Task<PagedResultDto<DepartamentoDto>> GetListAsync(GetDepartamentoListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Departamento.Nombre);
            }

            var departamentos = await _departamentoRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );

            var totalCount = input.Filter == null
                ? await _departamentoRepository.CountAsync()
                : await _departamentoRepository.CountAsync(
                    departamento => departamento.Nombre.Contains(input.Filter));

            return new PagedResultDto<DepartamentoDto>(
                totalCount,
                ObjectMapper.Map<List<Departamento>, List<DepartamentoDto>>(departamentos)
            );
        }

        [Authorize(MantenimientoPermissions.Departamento.Create)]
        public async Task<DepartamentoDto> CreateAsync(CreateDepartamentoDto input)
        {
            var departamento = await _departamentoManager.CreateAsync(
                input.Id,
                input.Nombre,
                input.NombreAbreviado
            );

            await _departamentoRepository.InsertAsync(departamento);

            return ObjectMapper.Map<Departamento, DepartamentoDto>(departamento);
        }

        [Authorize(MantenimientoPermissions.Departamento.Edit)]
        public async Task UpdateAsync(int id, UpdateDepartamentoDto input)
        {
            var departamento = await _departamentoRepository.GetAsync(id);

            if (departamento.Nombre != input.Nombre)
            {
                await _departamentoManager.ChangeNameAsync(departamento, input.Nombre);
            }

            departamento.Nombre = input.Nombre;
            departamento.NombreAbreviado = input.NombreAbreviado;

            await _departamentoRepository.UpdateAsync(departamento);
        }

        [Authorize(MantenimientoPermissions.Departamento.Delete)]
        public async Task DeleteAsync(int id)
        {
            await _departamentoRepository.DeleteAsync(id);
        }



    }
}
