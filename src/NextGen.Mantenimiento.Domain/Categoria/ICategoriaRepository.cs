using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NextGen.Mantenimiento.Categoria
{
    public interface ICategoriaRepository : IRepository<Categoria, int>
    {
        Task<Categoria> FindByNameAsync(string nombre);

        Task<List<Categoria>> GetListAsync(
            string sorting,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            string filter = null
        );
    }
}
