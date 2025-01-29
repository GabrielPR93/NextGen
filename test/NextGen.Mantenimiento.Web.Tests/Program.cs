using Microsoft.AspNetCore.Builder;
using NextGen.Mantenimiento;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
builder.Environment.ContentRootPath = GetWebProjectContentRootPathHelper.Get("NextGen.Mantenimiento.Web.csproj"); 
await builder.RunAbpModuleAsync<MantenimientoWebTestModule>(applicationName: "NextGen.Mantenimiento.Web");

public partial class Program
{
}
