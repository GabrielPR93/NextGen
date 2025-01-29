using NextGen.Mantenimiento.Samples;
using Xunit;

namespace NextGen.Mantenimiento.EntityFrameworkCore.Domains;

[Collection(MantenimientoTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<MantenimientoEntityFrameworkCoreTestModule>
{

}
