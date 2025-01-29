using System.Threading.Tasks;

namespace NextGen.Mantenimiento.Data;

public interface IMantenimientoDbSchemaMigrator
{
    Task MigrateAsync();
}
