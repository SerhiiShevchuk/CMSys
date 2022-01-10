using System;
using CMSys.Core.Entities.Catalog;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMSys.WebApp.Areas.Admin.ViewModels
{
    public class CourseEditModel
    {
        public Guid Id { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(64, ErrorMessage = "Max length is 64")]
        public string Name { get; set; }

        [DisplayName("IsNew")]
        [Required(ErrorMessage = "IsNew is required")]
        public bool IsNew { get; set; }

        [DisplayName("CourseType")]
        [Required(ErrorMessage = "CourseType is required")]
        public string CourseTypeName { get; set; }

        [DisplayName("CourseGroup")]
        [Required(ErrorMessage = "CourseGroup is required")]
        public string CourseGroupName { get; set; }

        [DisplayName("Order")]
        [Required(ErrorMessage = "Order is required")]
        public int VisualOrder { get; set; }

        [DisplayName("Description")] 
        [StringLength(4000, ErrorMessage = "Description length is 4000")]
        public string Description { get; set; }

        public IEnumerable<CourseType> CourseTypes { get; set; }
        public IEnumerable<CourseGroup> CourseGroups { get; set; }
    }
}
