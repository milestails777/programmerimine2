using KooliProjekt.Application.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Dto
{
    public  class ProjectTaskDto
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public decimal? Price { get; set; }

        public int? UserId { get; set; }

        public string? Description { get; set; }

        public string Status { get; set; }
    }
}
