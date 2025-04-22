using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace NextGen.Mantenimiento.AcreditacionesAsignadas
{
    public class AcreditacionesAsignadasManager : DomainService
    {

        private readonly IAcreditacionesAsignadasRepository _acreditacionesAsignadasRepository;
        public AcreditacionesAsignadasManager(IAcreditacionesAsignadasRepository acreditacionesAsignadasRepository)
        {
            _acreditacionesAsignadasRepository = acreditacionesAsignadasRepository;
        }
        public async Task<AcreditacionesAsignadas> CreateAsync(int personalId, int? empresaId, int acreditacionId, DateTime fechaEmision, DateTime? fechaCaducidad)
        {
            var acreditacion = new AcreditacionesAsignadas(personalId, empresaId, acreditacionId, fechaEmision, fechaCaducidad);
            return await _acreditacionesAsignadasRepository.InsertAsync(acreditacion);
        }
    }
}
