using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using NextGen.Mantenimiento.EntityFrameworkCore;
using NextGen.Mantenimiento.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly MantenimientoDbContext _dbContext;

        public EmpresaAppService(IEmpresaRepository empresaRepository, EmpresaManager empresaManager, MantenimientoDbContext dbContext)
        {
            _empresaRepository = empresaRepository;
            _empresaManager = empresaManager;
            _dbContext = dbContext;
        }
        public async Task<EmpresaDto> GetAsync(int id)
        {
            var empresa = await _dbContext.Empresa
                                          .Where(e => e.Id == id)
                                          .Select(e => new EmpresaDto
                                          {
                                              Id = e.Id,
                                              Nombre = e.Nombre,
                                              NombreAbreviado = e.NombreAbreviado,
                                              Direccion = e.Direccion == null ? string.Empty : e.Direccion,
                                              Correo = e.Correo,
                                              Logo = e.Logo == null ? new byte[0] : e.Logo // Si es NULL, lo convertimos en un array vacío
                                          })
                                          .FirstOrDefaultAsync();

            if (empresa == null)
            {
                throw new BusinessException($"No se encontró la empresa con ID {id}.");
            }

            return empresa;
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
        //TODO: REvisar el guardado
        [Authorize(MantenimientoPermissions.Empresa.Edit)]
        public async Task UpdateAsync(int id, UpdateEmpresaDto input)
        {
            var empresa = await _dbContext.Empresa
                                          .Where(e => e.Id == id)
                                          .FirstOrDefaultAsync();

            if (empresa == null)
            {
                throw new BusinessException($"No se encontró la empresa con ID {id}.");
            }

            // Manejamos valores nulos
            empresa.Nombre = input.Nombre ?? empresa.Nombre;
            empresa.NombreAbreviado = input.NombreAbreviado ?? empresa.NombreAbreviado;
            empresa.Direccion = input.Direccion == null ? string.Empty : input.Direccion;
            empresa.Correo = input.Correo ?? empresa.Correo;
            empresa.Logo = input.Logo == null ? new byte[0] : input.Logo;

            await _empresaRepository.UpdateAsync(empresa);
        }


        [Authorize(MantenimientoPermissions.Empresa.Delete)]
        public async Task DeleteAsync(int id)
        {

            var empresa = await _dbContext.Empresa
                                         .Where(e => e.Id == id)
                                         .Select(e => new Empresa { Id = e.Id })
                                         .FirstOrDefaultAsync();

            if (empresa == null)
            {
                throw new BusinessException($"No se encontró la empresa con ID {id}.");
            }

            await _empresaRepository.DeleteAsync(empresa);
        }
    }
}

