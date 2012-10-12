using System;

namespace Ewk.BandWebsite.Domain.AppModel
{
    public class LoginAccount : Entity
    {
        public string EmailAddress { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public DateTime LastPasswordChangedDate { get; set; }

        public bool IsApproved { get; set; }

        public bool IsOnline { get; set; }
        public DateTime LastLoginDate { get; set; }
        public DateTime LastActivityDate { get; set; }

        public bool IsLockedOut { get; set; }
        public DateTime LastLockoutDate { get; set; }
    }
}