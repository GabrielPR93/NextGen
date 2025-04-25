using NextGen.Mantenimiento.Categoria;
using Microsoft.AspNetCore.Authorization;
using NextGen.Mantenimiento.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using NextGen.Mantenimiento.Filter;
using NextGen.Mantenimiento.TipoAcreditaciones;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Entities;

namespace NextGen.Mantenimiento.AcreditacionesAsignadas
{
    [RemoteService]
    [Authorize(MantenimientoPermissions.AcreditacionesAsignadas.Default)]
    public class AcreditacionesAsignadasAppService : MantenimientoAppService, IAcreditacionesAsignadasAppService
    {
        private readonly IAcreditacionesAsignadasRepository _acreditacionesAsignadasRepository;
        private readonly AcreditacionesAsignadasManager _acreditacionesAsignadasManager;
        public AcreditacionesAsignadasAppService(IAcreditacionesAsignadasRepository acreditacionesAsignadasRepository, AcreditacionesAsignadasManager acreditacionesAsignadasManager)
        {
            _acreditacionesAsignadasRepository = acreditacionesAsignadasRepository;
            _acreditacionesAsignadasManager = acreditacionesAsignadasManager;
        }
        public async Task<AcreditacionesAsignadasDto> GetAsync(int id)
        {
            try
            {
                var entity = await _acreditacionesAsignadasRepository.GetAsync(id);
                return ObjectMapper.Map<AcreditacionesAsignadas, AcreditacionesAsignadasDto>(entity);
            }
            catch (EntityNotFoundException)
            {
                throw new UserFriendlyException("La acreditación asignada con el ID proporcionado no existe.");
            }
        }


        public async Task<AcreditacionesAsignadasDto> GetByIdAsync(int personalId)
        {
            var entity = await _acreditacionesAsignadasRepository.FindByIdAsync(personalId);
            return ObjectMapper.Map<AcreditacionesAsignadas, AcreditacionesAsignadasDto>(entity);
        }
        //Revisar por fecha de emision
        public async Task<PagedResultDto<AcreditacionesAsignadasDto>> GetListAsync(GetAllListDto input)
        {

            var tipoAcreditaciones = await _acreditacionesAsignadasRepository.GetListAsync(
                input.Sorting,
                input.MaxResultCount,
                input.SkipCount,
                input.Filter
            );
            var totalCount = input.Filter == null
                ? await _acreditacionesAsignadasRepository.CountAsync()
                : await _acreditacionesAsignadasRepository.CountAsync(
                    tipoAcreditaciones => tipoAcreditaciones.FechaEmision.ToString().Contains(input.Filter));

            return new PagedResultDto<AcreditacionesAsignadasDto>(
                totalCount,
                ObjectMapper.Map<List<AcreditacionesAsignadas>, List<AcreditacionesAsignadasDto>>(tipoAcreditaciones)
            );
        }
        [Authorize(MantenimientoPermissions.AcreditacionesAsignadas.Create)]
        public async Task<AcreditacionesAsignadasDto> CreateAsync(CreateUpdateAcreditacionesAsignadasDto input)
        {
            var entity = ObjectMapper.Map<CreateUpdateAcreditacionesAsignadasDto, AcreditacionesAsignadas>(input);
            await _acreditacionesAsignadasRepository.InsertAsync(entity);
            return ObjectMapper.Map<AcreditacionesAsignadas, AcreditacionesAsignadasDto>(entity);
        }
        public async Task UpdateAsync(int id, CreateUpdateAcreditacionesAsignadasDto input)
        {
            var entity = await _acreditacionesAsignadasRepository.GetAsync(id);
            ObjectMapper.Map(input, entity);
            await _acreditacionesAsignadasRepository.UpdateAsync(entity);
        }
        public async Task DeleteAsync(int id)
        {
            await _acreditacionesAsignadasRepository.DeleteAsync(id);
        }
    }
}
