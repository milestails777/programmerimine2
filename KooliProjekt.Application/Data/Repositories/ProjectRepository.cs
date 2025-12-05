using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Data.Repositories
{
    // 28.11
    // ToDo listide repository klass
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(ApplicationDbContext dbContext) :
            base(dbContext)
        {
        }

        // Lisa siia spetsiifilisemad meetodid,
        // mis on seotud ToDoListidega

        // BaseRepository ei tea, et Get peab tooma kaasa ka Itemsid
        public override async Task<Project> GetByIdAsync(int id)
        {
            return await DbContext
                .Projects
                .Include(list => list.Tasks)
                .Where(list => list.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}