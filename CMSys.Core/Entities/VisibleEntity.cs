using System;

namespace CMSys.Core.Entities
{
    public class VisibleEntity : Entity<Guid>
    {
        public string Name { get; set; }
        public int VisualOrder { get; set; }
        public string Description { get; set; }
    }
}