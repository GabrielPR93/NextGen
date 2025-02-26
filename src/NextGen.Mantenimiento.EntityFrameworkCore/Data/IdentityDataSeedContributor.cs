using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.Uow;

namespace NextGen.Mantenimiento.EntityFrameworkCore.Data
{
    public class IdentityDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public IdentityDataSeedContributor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [UnitOfWork] // Asegura que las operaciones sean transaccionales
        public async Task SeedAsync(DataSeedContext context)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<IdentityUserManager>();
                var roleManager = scope.ServiceProvider.GetRequiredService<IdentityRoleManager>();

                string adminEmail = "admin@example.com";
                string adminPassword = "Admin123!"; // Cambiar esto en producción
                string adminRoleName = "Admin";

                // Verificar si el rol "Admin" ya existe // yo lo creara manualmente en la base de datos
                //var existingRole = await roleManager.FindByNormalizedNameAsync(adminRoleName.ToUpper());
                //if (existingRole == null)
                //{
                //    var adminRole = new IdentityRole(Guid.NewGuid(), adminRoleName);
                //    await roleManager.CreateAsync(adminRole);
                //}

                // Verificar si el usuario administrador ya existe
                var existingUser = await userManager.FindByEmailAsync(adminEmail);
                if (existingUser == null)
                {
                    // 🔹 Crear el usuario usando el constructor correcto
                    var adminUser = new IdentityUser(
                        Guid.NewGuid(), // ID del usuario
                        adminEmail,     // Nombre de usuario
                        adminEmail,     // Correo electrónico
                        null            // TenantId (null si no usas multitenancy)
                    );

                    adminUser.SetEmailConfirmed(true);

                    // Crear el usuario administrador
                    var result = await userManager.CreateAsync(adminUser, adminPassword);
                    //if (result.Succeeded)
                    //{
                    //    // 4️⃣ Asignar el rol "Admin" al usuario
                    //    await userManager.AddToRoleAsync(adminUser.Id, adminRoleName);
                    //}
                }
            }
        }
    }
}


