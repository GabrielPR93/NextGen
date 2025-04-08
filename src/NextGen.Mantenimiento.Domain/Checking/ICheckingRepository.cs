using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NextGen.Mantenimiento.Checking
{
    public interface ICheckingRepository : IRepository<CheckingDiario, Guid>
    {
        Task<CheckingDiario> FindByUserAndDateAsync(string nombreUsuario, DateTime horaEntrada);

        Task<CheckingDiario?> FindLastOpenByUserAsync(string nombreUsuario);


        Task<List<CheckingDiario>> GetListAsync(
            string sorting,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            string filter = null
        );
    }
}
