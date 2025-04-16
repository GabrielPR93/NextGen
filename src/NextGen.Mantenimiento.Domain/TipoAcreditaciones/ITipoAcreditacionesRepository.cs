using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NextGen.Mantenimiento.TipoAcreditaciones
{
    public interface ITipoAcreditacionesRepository : IRepository<TipoAcreditaciones, int>
    {
        Task<TipoAcreditaciones> FindByNameAsync(string nombre);

        Task<List<TipoAcreditaciones>> GetListAsync(
            string sorting,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            string filter = null
        );
    }
}
