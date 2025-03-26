using Microsoft.AspNetCore.Authorization;
using NextGen.Mantenimiento.Permissions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace NextGen.Mantenimiento.Empresa
{
    [RemoteService]
    [Authorize(MantenimientoPermissions.Empresa.Default)]
    public class EmpresaAppService : MantenimientoAppService, IEmpresaAppService
    {
        private readonly IEmpresaRepository _empresaRepository;
        private readonly EmpresaManager _empresaManager;

        public EmpresaAppService(IEmpresaRepository empresaRepository, EmpresaManager empresaManager)
        {
            _empresaRepository = empresaRepository;
            _empresaManager = empresaManager;
        }
        public async Task<EmpresaDto> GetAsync(int id)
        {
            var empresa = await _empresaRepository.GetAsync(id);
            return ObjectMapper.Map<Empresa, EmpresaDto>(empresa);
        }

        public async Task<PagedResultDto<EmpresaDto>> GetListAsync(Filter.GetAllListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Empresa.Nombre);
            }

            var empresa = await _empresaRepository.GetListAsync(
                input.Sorting,
                input.MaxResultCount,
                input.SkipCount,
                input.Filter
            );
            var totalCount = input.Filter == null
                ? await _empresaRepository.CountAsync()
                : await _empresaRepository.CountAsync(
                    empresa => empresa.Nombre.Contains(input.Filter));

            return new PagedResultDto<EmpresaDto>(
                totalCount,
                ObjectMapper.Map<List<Empresa>, List<EmpresaDto>>(empresa)
            );
        }

        [Authorize(MantenimientoPermissions.Empresa.Create)]
        public async Task<EmpresaDto> CreateAsync(CreateEmpresaDto input)
        {
            var empresa = await _empresaManager.CreateAsync(
                input.Id,
                input.Nombre,
                input.NombreAbreviado,
                input.Direccion,
                input.Correo,
                input.Logo
            );

            await _empresaRepository.InsertAsync(empresa);

            return ObjectMapper.Map<Empresa, EmpresaDto>(empresa);
        }

        [Authorize(MantenimientoPermissions.Empresa.Edit)]
        public async Task UpdateAsync(int id, UpdateEmpresaDto input)
        {
            var empresa = await _empresaRepository.GetAsync(id);

            if (empresa.Nombre != input.Nombre)
            {
                await _empresaManager.ChangeNameAsync(empresa, input.Nombre);
            }

            empresa.Nombre = input.Nombre;
            empresa.NombreAbreviado = input.NombreAbreviado;
            empresa.Direccion = input.Direccion;
            empresa.Correo = input.Correo;
            empresa.Logo = input.Logo;

            await _empresaRepository.UpdateAsync(empresa);
        }

        [Authorize(MantenimientoPermissions.Empresa.Delete)]
        public async Task DeleteAsync(int id)
        {
            await _empresaRepository.DeleteAsync(id);
        }
    }
}

