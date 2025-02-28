using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NextGen.Mantenimiento.Departamento
{
    public interface IDepartamentoRepository : IRepository<Departamento, int>
    {
        Task<Departamento> FindByNameAsync(string nombre);

        Task<List<Departamento>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );
    }
}
