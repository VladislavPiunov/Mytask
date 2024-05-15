namespace Mytask.API.Dto
{
    public class UserDto
    {
        public string Id { get; set; }
        public long CreatedTimestamp { get; set; }
        public string Username { get; set; }
        public bool Enabled { get; set; }
        public bool Totp { get; set; }
        public bool EmailVerified { get; set; }
        public object[] DisableableCredentialTypes { get; set; }
        public object[] RequiredActions { get; set; }
        public int NotBefore { get; set; }
        public Access Access { get; set; }
    }

    public class Access
    {
        public bool ManageGroupMembership { get; set; }
        public bool View { get; set; }
        public bool MapRoles { get; set; }
        public bool Impersonate { get; set; }
        public bool Manage { get; set; }
    }
}
