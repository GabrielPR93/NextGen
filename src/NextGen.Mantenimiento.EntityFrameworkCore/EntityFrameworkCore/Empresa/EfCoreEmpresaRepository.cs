using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NextGen.Mantenimiento.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;


namespace NextGen.Mantenimiento.Empresa
{
    public class EfCoreEmpresaRepository : EfCoreRepository<MantenimientoDbContext, Empresa, int>, IEmpresaRepository
    {

        public EfCoreEmpresaRepository(IDbContextProvider<MantenimientoDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
        public async Task<Empresa> FindByNameAsync(string nombre)
        {
            return await (await GetDbSetAsync()).FirstOrDefaultAsync(c => c.Nombre == nombre);
        }
        public async Task<List<Empresa>> GetListAsync(string sorting, int maxResultCount = int.MaxValue, int skipCount = 0, string filter = null)
        {
            var dbSet = await GetDbSetAsync();

            return await dbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    emp => emp.Nombre.Contains(filter)
                )
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .Select(emp => new Empresa
                {
                    Id = emp.Id,
                    Nombre = emp.Nombre,
                    NombreAbreviado = emp.NombreAbreviado,
                    Direccion = emp.Direccion == null ? String.Empty : emp.Direccion,
                    Correo = emp.Correo,
                    Logo = emp.Logo == null ? new byte[0] : emp.Logo
                })
                .ToListAsync();
        }
    }
}
