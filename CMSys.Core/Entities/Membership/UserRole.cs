using System;

namespace CMSys.Core.Entities.Membership
{
    public sealed class UserRole : Entity
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}