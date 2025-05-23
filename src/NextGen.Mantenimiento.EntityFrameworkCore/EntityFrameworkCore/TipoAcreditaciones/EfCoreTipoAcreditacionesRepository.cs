﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NextGen.Mantenimiento.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;


namespace NextGen.Mantenimiento.TipoAcreditaciones
{
    public class EfCoreTipoAcreditacionesRepository : EfCoreRepository<MantenimientoDbContext, TipoAcreditaciones,int>, ITipoAcreditacionesRepository
    {
        public EfCoreTipoAcreditacionesRepository(IDbContextProvider<MantenimientoDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
        public async Task<TipoAcreditaciones> FindByNameAsync(string nombre)
        {
            return await (await GetDbSetAsync()).FirstOrDefaultAsync(c => c.Nombre == nombre);
        }
        public async Task<List<TipoAcreditaciones>> GetListAsync(string sorting, int maxResultCount = int.MaxValue, int skipCount = 0, string filter = null)
        {

            return await (await GetDbSetAsync())
                .WhereIf(!filter.IsNullOrWhiteSpace(), c => c.Nombre.Contains(filter) || (c.Descripcion != null && c.Descripcion.Contains(filter)))
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
