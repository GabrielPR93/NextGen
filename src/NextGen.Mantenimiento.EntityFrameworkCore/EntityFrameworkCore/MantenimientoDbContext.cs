using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using NextGen.Mantenimiento.Entities;
using NextGen.Mantenimiento.Departamento;



namespace NextGen.Mantenimiento.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class MantenimientoDbContext :
    AbpDbContext<MantenimientoDbContext>,
    ITenantManagementDbContext,
    IIdentityDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    public DbSet<Personal> Personal { get; set; }
    public DbSet<Departamento.Departamento> Departamento { get; set; }

    public DbSet<Categoria.Categoria> Categoria { get; set; }


    #region Entities from the modules

    /* Notice: We only implemented IIdentityProDbContext and ISaasDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityProDbContext and ISaasDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    // Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
    public DbSet<IdentitySession> Sessions { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public MantenimientoDbContext(DbContextOptions<MantenimientoDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureFeatureManagement();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureTenantManagement();
        builder.ConfigureBlobStoring();

        /* Configure your own tables/entities inside here */

        builder.Entity<Personal>(b =>
        {
        
            b.ToTable("Personal");
            b.ConfigureByConvention();

            b.Property(x => x.DepartamentoId)
            .IsRequired();

            b.Property(x => x.CategoriaId)
           .IsRequired();

            b.Property(x => x.Nombre)
                .IsRequired()
                .HasMaxLength(128);

            b.Property(x => x.Apellidos)
                .IsRequired()
                .HasMaxLength(128);

            b.Property(x => x.Dni)
                .IsRequired()
                .HasMaxLength(9); 

            b.Property(x => x.Telefono)
                .IsRequired()
                .HasMaxLength(20);

            b.Property(x => x.Direccion)
                .IsRequired();

            b.Property(x => x.CorreoElectronico)
                .IsRequired()
                .HasMaxLength(256); 

            b.Property(x => x.FechaNacimiento)
                .IsRequired()
                .HasColumnType("date"); 

            b.Property(x => x.FechaAlta)
                .IsRequired()
                .HasColumnType("date");

            b.Property(x => x.FechaBaja)
                .HasColumnType("date") 
                .IsRequired(false);

            b.HasOne<Departamento.Departamento>()
                .WithMany()
                .HasForeignKey(x => x.DepartamentoId)
                .IsRequired();

            b.HasOne<Categoria.Categoria>()
                .WithMany()
                .HasForeignKey(x => x.CategoriaId)
                .IsRequired();
        });

        builder.Entity<Departamento.Departamento>(b =>
        {
            b.ToTable("Departamento");
            b.ConfigureByConvention();
            b.Property(x => x.Nombre)
                .IsRequired()
                .HasMaxLength(DepartamentoConsts.MaxNameLength);
            b.Property(x => x.NombreAbreviado)
                .IsRequired()
                .HasMaxLength(DepartamentoConsts.MaxabreviateNameLength);
            b.HasIndex(x => x.Nombre);
        });

        builder.Entity<Categoria.Categoria>(b =>
        {
            b.ToTable("Categorias");
            b.ConfigureByConvention();
            b.Property(x => x.Nombre)
                .IsRequired()
                .HasMaxLength(128);
            b.Property(x => x.Descripcion)
                .IsRequired()
                .HasMaxLength(256);
            b.HasIndex(x => x.Nombre);
        });

    }
}
