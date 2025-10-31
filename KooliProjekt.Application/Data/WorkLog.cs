﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Data
{
    public class WorkLog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TaskId { get; set; }

        [ForeignKey(nameof(TaskId))]
        public Task Task { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Time spent must be greater than 0.")]
        public decimal TimeSpent { get; set; }

        [Required]
        public DateTime Date { get; set; } // проверить, чтобы не была из будущего (через валидацию в коде)

        [Required]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [MaxLength(500)]
        [MinLength(10)]
        public string? Description { get; set; }
    }
}
