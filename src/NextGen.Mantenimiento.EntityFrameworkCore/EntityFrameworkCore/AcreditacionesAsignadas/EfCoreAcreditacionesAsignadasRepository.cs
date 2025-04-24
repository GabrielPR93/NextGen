using Microsoft.EntityFrameworkCore;
using NextGen.Mantenimiento.AcreditacionesAsignadas;
using NextGen.Mantenimiento.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace NextGen.Mantenimiento.AcreditacionesAsignadas
{
    public class EfCoreAcreditacionesAsignadasRepository : EfCoreRepository<MantenimientoDbContext, AcreditacionesAsignadas, int>, IAcreditacionesAsignadasRepository
    {

        public EfCoreAcreditacionesAsignadasRepository(IDbContextProvider<MantenimientoDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
        //public async Task<AcreditacionesAsignadas> FindByNameAsync(string nombre)
        //{
        //    return await (await GetDbSetAsync()).FirstOrDefaultAsync(c => c.FechaCaducidad == nombre);
        //}

        public async Task<AcreditacionesAsignadas> FindByIdAsync(int id)
        {
            return await (await GetDbSetAsync()).FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<List<AcreditacionesAsignadas>> GetListAsync(string sorting, int maxResultCount = int.MaxValue, int skipCount = 0, string filter = null)
        {
            return await (await GetDbSetAsync())
                .WhereIf(!filter.IsNullOrWhiteSpace(), c => c.FechaCaducidad != null && c.PersonalId != null)
                .OrderBy(c => EF.Property<object>(c, sorting)) 
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
