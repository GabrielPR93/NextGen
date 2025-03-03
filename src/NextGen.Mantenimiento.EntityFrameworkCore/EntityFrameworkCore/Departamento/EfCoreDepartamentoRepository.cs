using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NextGen.Mantenimiento.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace NextGen.Mantenimiento.Departamento
{
    public class EfCoreDepartamentoRepository
        : EfCoreRepository<MantenimientoDbContext, Departamento, int>,
            IDepartamentoRepository
    {
        public EfCoreDepartamentoRepository(
            IDbContextProvider<MantenimientoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<Departamento> FindByNameAsync(string nombre)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(depto => depto.Nombre == nombre);
        }

        public async Task<List<Departamento>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    depto => depto.Nombre.Contains(filter)
                )
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}

