﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.RewardModule.DTO.Request
{
    public class UpdateBadgeRequest
    {
        [Required]
        public Guid BadgeId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public int Status { get; set; }

    }
}
