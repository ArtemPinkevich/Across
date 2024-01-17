namespace Entities
{
    public static class UserRoles
    {
        public const string Shipper = "Shipper";

        public const string Driver = "Driver";

        public const string Lawyer = "Lawyer";

        public const string Admin = "Admin";
        
        public const string Owner = "Owner";

        public const string All = $"{Shipper},{Driver},{Lawyer},{Admin},{Owner}";
    }
}
