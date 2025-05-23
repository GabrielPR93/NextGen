namespace NextGen.Mantenimiento.Permissions;

public static class MantenimientoPermissions
{
    public const string GroupName = "Mantenimiento";


    public static class Personal
    {
        public const string Default = GroupName + ".Personal";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
    public static class Departamento
    {
        public const string Default = GroupName + ".Departamento";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class Categoria
    {
        public const string Default = GroupName + ".Categoria";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class Empresa
    {
        public const string Default = GroupName + ".Empresa";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class Checking
    {
        public const string Default = GroupName + ".Checking";
        public const string ViewAll = Default + ".ViewAll";
        public const string ViewOwn = Default + ".ViewOwn";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class TipoAcreditaciones
    {
        public const string Default = GroupName + ".TipoAcreditaciones";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class AcreditacionesAsignadas
    {
        public const string Default = GroupName + ".AcreditacionesAsignadas";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

}
