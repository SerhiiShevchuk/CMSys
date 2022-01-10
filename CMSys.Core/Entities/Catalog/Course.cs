using System;
using System.Collections.Generic;
using System.Linq;

namespace CMSys.Core.Entities.Catalog
{
    public sealed class Course : VisibleEntity
    {
        public const int NameLength = 64;
        public const int DescriptionLength = 4000;

        public bool IsNew { get; set; }
        public Guid CourseTypeId { get; set; }
        public Guid CourseGroupId { get; set; }

        public CourseType CourseType { get; set; }
        public CourseGroup CourseGroup { get; set; }
        public ICollection<CourseTrainer> Trainers { get; set; }

        public bool HasTrainer(Trainer trainer) => Trainers.Select(x => x.Trainer).Contains(trainer);
    }
}