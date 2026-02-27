using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace KooliProjekt.Application.Data
{
    [ExcludeFromCodeCoverage]
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<ProjectUser> ProjectUsers { get; set; }
        public DbSet<ProjectWorkLog> ProjectWorkLogs { get; set; }
        public DbSet<ProjectTeam> ProjectTeams { get; set; }

    }
}