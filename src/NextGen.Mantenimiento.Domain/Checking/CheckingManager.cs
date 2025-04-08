using NextGen.Mantenimiento.Empresa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace NextGen.Mantenimiento.Checking
{
    public class CheckingManager : DomainService
    {
        private readonly ICheckingRepository _checkingRepository;
        public CheckingManager(ICheckingRepository checkingRepository)
        {
            _checkingRepository = checkingRepository;
        }
        public async Task<CheckingDiario> CreateAsync(Guid id, Guid userId, DateTime horaEntrada, DateTime? horaSalida, DateTime horaCreacion, string nombreusuario, string? nombre, string? apellidos)
        {
            Check.NotNull(userId, nameof(userId));
            Check.NotNull(nombreusuario, nameof(nombreusuario));

            var OpenChecking = await _checkingRepository.FindLastOpenByUserAsync(nombreusuario);
            if (OpenChecking != null)
            {
                throw new CheckingAlreadyExistsException(userId);
            }
            return new CheckingDiario(GuidGenerator.Create(), userId, horaEntrada, horaSalida, DateTime.UtcNow, nombreusuario, nombre, apellidos);
        }
    }
}
