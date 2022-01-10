using CMSys.Core.Entities.Membership;
using System;

namespace CMSys.Core.Entities.Catalog
{
    public sealed class Trainer : Entity<Guid>
    {
        public const int DescriptionLength = 4000;

        public int VisualOrder { get; set; }
        public string Description { get; set; }
        public Guid TrainerGroupId { get; set; }

        public User User { get; set; }
        public TrainerGroup TrainerGroup { get; set; }
    }
}