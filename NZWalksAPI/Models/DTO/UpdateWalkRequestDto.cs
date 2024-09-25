﻿using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class UpdateWalkRequestDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be a maximun of 100 characters")]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000, ErrorMessage = "Description has to be a maximun of 1000 characters")]
        public string Description { get; set; }

        [Required]
        [Range(0,50, ErrorMessage = "Range should be from 0 to 50KM")]
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        // Foreign Keys
        [Required]
        public Guid DifficultyId { get; set; }

        [Required]
        public Guid RegionId { get; set; }
    }
}
