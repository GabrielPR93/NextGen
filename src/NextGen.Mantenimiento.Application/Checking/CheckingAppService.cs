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

namespace NextGen.Mantenimiento.Checking
{
    [RemoteService]
    [Authorize(MantenimientoPermissions.Checking.Default)]
    public class CheckingAppService : MantenimientoAppService, ICheckingAppService
    {
        private readonly ICheckingRepository _checkingRepository;
        private readonly CheckingManager _checkingManager;

        public CheckingAppService(ICheckingRepository checkingRepository, CheckingManager checkingManager)
        {
            _checkingRepository = checkingRepository;
            _checkingManager = checkingManager;
        }

        public async Task<CheckingDto> GetAsync(Guid id)
        {
            var checking = await _checkingRepository.GetAsync(id);
            return ObjectMapper.Map<CheckingDiario, CheckingDto>(checking);
        }

        public async Task<PagedResultDto<CheckingDto>> GetListAsync(Filter.GetAllListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(CheckingDiario.HoraEntrada);
            }

            var checking = await _checkingRepository.GetListAsync(
                input.Sorting,
                input.MaxResultCount,
                input.SkipCount,
                input.Filter
            );

            var totalCount = input.Filter == null
                ? await _checkingRepository.CountAsync()
                : await _checkingRepository.CountAsync(
                    checking => checking.NombreUsuario != null && checking.NombreUsuario.Contains(input.Filter));

            return new PagedResultDto<CheckingDto>(
                totalCount,
                ObjectMapper.Map<List<CheckingDiario>, List<CheckingDto>>(checking)
            );
        }

        [Authorize(MantenimientoPermissions.Checking.Create)]
        public async Task<CheckingDto> CreateAsync(CreateCheckingDto input)
        {
            var checking = await _checkingManager.CreateAsync(
                input.Id,
                input.UserId,
                input.HoraEntrada,
                input.HoraSalida,
                input.HoraCreacion,
                input.NombreUsuario,
                input.Nombre,
                input.Apellidos


            );

            await _checkingRepository.InsertAsync(checking);

            return ObjectMapper.Map<CheckingDiario, CheckingDto>(checking);
        }

        [Authorize(MantenimientoPermissions.Checking.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _checkingRepository.DeleteAsync(id);
        }
    }
}
