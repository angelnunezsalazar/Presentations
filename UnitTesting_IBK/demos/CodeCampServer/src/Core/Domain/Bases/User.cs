using CodeCampServer.Core.Bases;

namespace CodeCampServer.Core.Domain.Bases
{
    public class User : AuditedPersistentObjectOfGuid
    {
        public const string ADMIN_USERNAME = "admin";
        public virtual string Username { get; set; }
        public virtual string Name { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual string PasswordSalt { get; set; }

        public virtual bool IsAdmin()
        {
            if (Username == ADMIN_USERNAME)
                return true;
            else
                return false;
        }
    }
}