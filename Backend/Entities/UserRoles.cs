namespace Entities
{
    public static class UserRoles
    {
        public const string MobileClient = "Mobile_client";

        public const string Admin = "Admin";

        public const string Owner = "Owner";

        public const string All = $"{MobileClient},{Admin},{Owner}";
    }
}
