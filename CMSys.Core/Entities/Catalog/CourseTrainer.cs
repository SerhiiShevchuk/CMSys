using System;

namespace CMSys.Core.Entities.Catalog
{
    public sealed class CourseTrainer : Entity
    {
        public Guid CourseId { get; set; }
        public Guid TrainerId { get; set; }
        public int VisualOrder { get; set; }

        public Trainer Trainer { get; set; }
    }
}