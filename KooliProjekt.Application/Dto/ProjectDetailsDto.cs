using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Dto
{
    [ExcludeFromCodeCoverage]
    public class ProjectDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IList<ProjectDto> Items { get; set; } = new List<ProjectDto>();
    }

}
