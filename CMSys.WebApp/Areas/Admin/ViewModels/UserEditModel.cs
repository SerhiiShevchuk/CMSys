using System;
using CMSys.Core.Entities.Membership;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMSys.WebApp.Areas.Admin.ViewModels
{
    public class UserEditModel
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public string Name { get; set; }

        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        public string RepeatPassword { get; set; }


        public IEnumerable<Role> Roles { get; set; }
        public IEnumerable<Role> AllRoles { get; set; }

    }
}
