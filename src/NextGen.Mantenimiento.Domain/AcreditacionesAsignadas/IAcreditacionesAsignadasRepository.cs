using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NextGen.Mantenimiento.AcreditacionesAsignadas
{
    public interface IAcreditacionesAsignadasRepository : IRepository<AcreditacionesAsignadas, int>
    {
        Task<AcreditacionesAsignadas> FindByNameAsync(string nombre);
        Task<AcreditacionesAsignadas> FindByIdAsync(int id);
        Task<List<AcreditacionesAsignadas>> GetListAsync(
            string sorting,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            string filter = null
        );
    }
   
}
