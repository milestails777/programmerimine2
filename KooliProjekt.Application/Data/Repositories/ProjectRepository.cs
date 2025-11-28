using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; // Added for Include extension method

namespace KooliProjekt.Application.Data.Repositories
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(ApplicationDbContext dbContext) :
            base(dbContext)
        {
        }

        public override async Task<Project> GetByIdAsync(int id)
        {
            return await DbContext
                .Projects
                .Include(project => project.Tasks)
                .Where(project => project.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
