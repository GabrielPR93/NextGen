using Microsoft.AspNetCore.Authorization;
using NextGen.Mantenimiento.Checking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Users;
using Volo.Abp.Domain.Repositories;
using NextGen.Mantenimiento.Permissions;

namespace NextGen.Mantenimiento.EntityFrameworkCore.Checking
{
    public class EfCoreCheckingRepository : EfCoreRepository<MantenimientoDbContext, CheckingDiario, Guid>,
                ICheckingRepository
    {
        private readonly ICurrentUser _currentUser;
        private readonly IAuthorizationService _authorizationService;
        

        public EfCoreCheckingRepository(
            IDbContextProvider<MantenimientoDbContext> dbContextProvider,
            ICurrentUser currentUser,
            IAuthorizationService authorizationService) : base(dbContextProvider)
        {
            _currentUser = currentUser;
            _authorizationService = authorizationService;
           
        }

        
        public async Task<CheckingDiario> FindByUserAndDateAsync(string nombreUsuario)
        {
            var currentUserId = _currentUser.Id;

            if (await _authorizationService.IsGrantedAsync("MantenimientoPermissions.Checking.ViewAll"))
            {
                return await (await GetDbSetAsync()).FirstOrDefaultAsync(c => c.NombreUsuario == nombreUsuario);
            }

            return await (await GetDbSetAsync()).FirstOrDefaultAsync(c => c.NombreUsuario == nombreUsuario);
        }

        public async Task<List<CheckingDiario>> GetListAsync(string sorting, int maxResultCount = int.MaxValue, int skipCount = 0, string filter = null)
        {
            if (await _authorizationService.IsGrantedAsync(MantenimientoPermissions.Checking.ViewAll))
            {
                // Puede ver todo
                return await (await GetDbSetAsync())
                    .WhereIf(!filter.IsNullOrWhiteSpace(), c => c.Nombre.Contains(filter) || c.NombreUsuario.Contains(filter))
                    .OrderBy(sorting)
                    .Skip(skipCount)
                    .Take(maxResultCount)
                    .ToListAsync();
            }
            else
            {
                var currentUserName = _currentUser.UserName;
                return await (await GetDbSetAsync())
                    .Where(c => c.NombreUsuario == currentUserName)
                    .WhereIf(!filter.IsNullOrWhiteSpace(), c => c.Nombre.Contains(filter) || c.NombreUsuario.Contains(filter))
                    .OrderBy(sorting)
                    .Skip(skipCount)
                    .Take(maxResultCount)
                    .ToListAsync();
            }

        }

    }
}
