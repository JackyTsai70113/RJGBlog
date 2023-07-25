using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Web.Areas.Back.Models.Role
{
    public class RoleViewModel
    {
        public List<IdentityRole> Roles { get; set; }
    }
}
