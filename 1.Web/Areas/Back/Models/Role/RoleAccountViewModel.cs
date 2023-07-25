using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Web.Areas.Back.Models.Role
{
    public class RoleAccountViewModel
    {
        public string RoleName { get; set; }

        public List<IdentityUser> Users { get; set; }
    }
}
