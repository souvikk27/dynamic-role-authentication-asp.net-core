namespace DunamicRoleAuth.Data
{
    public class Permissions
    {
        public static List<string> GeneratePermissionsForModule(string module)
        {
            return new List<string>()
        {
            $"Permissions.{module}.Create",
            $"Permissions.{module}.View",
            $"Permissions.{module}.Edit",
            $"Permissions.{module}.Delete",
            $"Permissions.{module}.Manage",
        };
        }
        public static class Products
        {
            public const string View = "Permissions.Products.View";
            public const string Create = "Permissions.Products.Create";
            public const string Edit = "Permissions.Products.Edit";
            public const string Delete = "Permissions.Products.Delete";
            public const string Manage = "Permissions.Products.Manage";
        }

        public static class Administration
        {
            public const string View = "Permissions.Administration.View";
            public const string Create = "Permissions.Administration.Create";
            public const string Edit = "Permissions.Administration.Edit";
            public const string Delete = "Permissions.Administration.Delete";
            public const string Manage = "Permissions.Administration.Manage";
        }
    }
}
