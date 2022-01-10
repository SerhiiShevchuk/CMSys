using System;
using CMSys.Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMSys.WebApp.Areas.Admin.ViewModels
{
    public class VisibleEntityEditModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(64, ErrorMessage = "Max length is 64")]
        public string Name { get; set; }

        [Required(ErrorMessage = "VisualOrder is required")]
        public int VisualOrder { get; set; }

        [StringLength(256, ErrorMessage = "Max length is 64")]
        public string Description { get; set; }

        public IEnumerable<VisibleEntity> Entities { get; set; }
    }
}
