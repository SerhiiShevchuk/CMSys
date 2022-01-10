using CMSys.Core.Entities.Catalog;
using CMSys.Core.Entities.Membership;
using System;
using System.Collections.Generic;

namespace CMSys.WebApp.Areas.Admin.ViewModels
{
    public class TrainersEditModel
    {
        public Guid Id { get; set; }

        public Guid TrainerId { get; set; }
        public string CourseName { get; set; }
        public int VisualOrder { get; set; }

        public IEnumerable<User> Trainers { get; set; }
        public IEnumerable<CourseTrainer> CourseTrainers { get; set; }
    }
}
