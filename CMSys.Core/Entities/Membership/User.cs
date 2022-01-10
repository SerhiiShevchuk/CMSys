using CMSys.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace CMSys.Core.Entities.Membership
{
    public sealed class User : Entity<Guid>
    {
        public const int EmailLength = 128;
        public const int PasswordHashLength = 128;
        public const int PasswordSaltLength = 128;
        public const int FirstNameLength = 128;
        public const int LastNameLength = 128;
        public const int DepartmentLength = 128;
        public const int PositionLength = 128;
        public const int LocationLength = 128;

        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public string Location { get; set; }
        public byte[] Photo { get; set; }

        public ICollection<Role> Roles { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public bool VerifyPassword(string password)
        {
            return !string.IsNullOrEmpty(password) &&
                   PasswordHelper.ComputeHash(password, PasswordSalt) == PasswordHash;
        }

        public void ChangePassword(string password)
        {
            PasswordSalt = PasswordHelper.GenerateSalt(PasswordSaltLength);
            PasswordHash = PasswordHelper.ComputeHash(password, PasswordSalt);
        }

        public IEnumerable<Claim> GetClaims()
        {
            var claims = Roles.Select(x => new Claim(ClaimTypes.Role, x.Name)).ToList();
            claims.Add(new Claim(ClaimTypes.Email, Email));
            claims.Add(new Claim(ClaimTypes.Name, FullName));
            return claims;
        }
    }
}