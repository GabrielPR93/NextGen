using Xunit;

namespace NextGen.Mantenimiento.EntityFrameworkCore;

[CollectionDefinition(MantenimientoTestConsts.CollectionDefinitionName)]
public class MantenimientoEntityFrameworkCoreCollection : ICollectionFixture<MantenimientoEntityFrameworkCoreFixture>
{

}
