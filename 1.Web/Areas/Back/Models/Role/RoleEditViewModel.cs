using System.Collections.Generic;
using Core.Domain;
using Core.Enum;

namespace Web.Areas.Back.Models.Role
{
    public class RoleEditViewModel
    {
        public RoleEditViewModel()
        {
            CheckMenuId = new List<int>();
            MenuTrees = new List<MenuTree>();
        }

        public ActionType ActionType { get; set; }

        public List<MenuTree> MenuTrees { get; set; }

        public List<int> CheckMenuId { get; set; }

        public string RoleName { get; set; }
    }
}
