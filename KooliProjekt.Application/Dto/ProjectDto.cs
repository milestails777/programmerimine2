using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;


namespace KooliProjekt.Application.Dto
{
    [ExcludeFromCodeCoverage]
    public class ProjectDto
    {
        public string Name { get; set; }  
        public DateTime StartDate { get; set; }     
        public DateTime DueDate { get; set; }
        public decimal Budget { get; set; }
        public decimal PricePerHour { get; set; }
    }
}
