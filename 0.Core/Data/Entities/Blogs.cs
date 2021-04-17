﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Core.Data.Entities
{
    public partial class Blogs
    {
        [Key]
        public string Id { get; set; }
        [StringLength(255)]
        public string CoverImageUrl { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        [StringLength(4000)]
        public string Content { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UpdateTime { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}