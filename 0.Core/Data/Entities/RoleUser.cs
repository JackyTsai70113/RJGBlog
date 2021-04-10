using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Data.Entities
{
    public class RoleUser
    {
        [Key]
        public int RoleId { get; set; }
        [Key]
        public int UserId { get; set; }

        [ForeignKey(nameof(RoleId))]
        [InverseProperty("RoleUser")]
        public virtual Role Role { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("RoleUser")]
        public virtual User User { get; set; }
    }
}