﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Back.Models.Role
{
    public class RoleAccountViewModel
    {
        public string RoleName { get; set; }

        public List<IdentityUser> Users { get; set; }
    }
}
