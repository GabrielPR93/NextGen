using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace NextGen.Mantenimiento.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class MantenimientoDbContextFactory : IDesignTimeDbContextFactory<MantenimientoDbContext>
{
    public MantenimientoDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        MantenimientoEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<MantenimientoDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));
        
        return new MantenimientoDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../NextGen.Mantenimiento.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
