using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NextGen.Mantenimiento.Empresa
{
    public interface IEmpresaRepository : IRepository<Empresa, int>
    {
    
            Task<Empresa> FindByNameAsync(string nombre);

            Task<List<Empresa>> GetListAsync(
                string sorting,
                int maxResultCount = int.MaxValue,
                int skipCount = 0,
                string filter = null
            );
    }
}
