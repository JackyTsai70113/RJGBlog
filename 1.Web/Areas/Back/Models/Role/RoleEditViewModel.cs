using Core.Domain;
using Core.Enum;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Back.Models.Role
{
    public class RoleEditViewModel
    {
        public RoleEditViewModel()
        {
            this.CheckMenuId = new List<int>();
            this.MenuTrees = new List<MenuTree>();
        }

        public ActionType ActionType { get; set; }

        public IdentityRole Role { get; set; }

        public List<MenuTree> MenuTrees { get; set; }

        public List<int> CheckMenuId { get; set; }
    }
}
