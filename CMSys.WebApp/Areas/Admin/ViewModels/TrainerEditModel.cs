using System;
using CMSys.Core.Entities.Catalog;
using CMSys.Core.Entities.Membership;
using System.Collections.Generic;

namespace CMSys.WebApp.Areas.Admin.ViewModels
{
    public class TrainerEditModel
    {
        public Guid Id { get; set; }
        public Guid TrainerGroupId { get; set; }
        public string Name { get; set; }
        public int VisualOrder { get; set; }
        public string Description { get; set; }

        public IEnumerable<User> Users { get; set; }
        public IEnumerable<TrainerGroup> TrainerGroups { get; set; }
    }
}
