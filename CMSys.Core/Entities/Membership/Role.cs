using System;
using System.Collections.Generic;

namespace CMSys.Core.Entities.Membership
{
    public sealed class Role : Entity<Guid>
    {
        public const int NameLength = 64;

        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}