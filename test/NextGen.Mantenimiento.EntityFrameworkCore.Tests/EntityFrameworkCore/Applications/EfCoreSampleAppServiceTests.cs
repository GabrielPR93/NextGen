using NextGen.Mantenimiento.Samples;
using Xunit;

namespace NextGen.Mantenimiento.EntityFrameworkCore.Applications;

[Collection(MantenimientoTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<MantenimientoEntityFrameworkCoreTestModule>
{

}
