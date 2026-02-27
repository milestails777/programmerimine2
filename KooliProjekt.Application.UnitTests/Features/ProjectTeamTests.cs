using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.ProjectTeams;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.UnitTests.Features
{
    public class ProjectTeamTests : TestBase
    {
        private ApplicationDbContext GetFaultyDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public void Get_should_throw_when_dbcontext_is_null()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new GetProjectTeamQueryHandler(null);
            });
        }

        [Fact]
        public async Task Get_should_return_existing_team_entry()
        {
            var query = new GetProjectTeamQuery { Id = 1 };
            var handler = new GetProjectTeamQueryHandler(DbContext);

            var team = new ProjectTeam { ProjectId = 1, UserId = 1 };
            await DbContext.ProjectTeams.AddAsync(team);
            await DbContext.SaveChangesAsync();

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.False(result.HasErrors);
            Assert.NotNull(result.Value);
            Assert.Equal(1, result.Value.Id);
        }

        [Fact]
        public void List_should_throw_when_dbcontext_is_null()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new ListProjectTeamQueryHandler(null);
            });
        }

        [Fact]
        public async Task List_should_return_page_of_team_entries()
        {
            var query = new ListProjectTeamQuery { Page = 1, PageSize = 5 };
            var handler = new ListProjectTeamQueryHandler(DbContext);

            for (var i = 1; i <= 10; i++)
            {
                var team = new ProjectTeam { ProjectId = i % 3 + 1, UserId = i };
                await DbContext.ProjectTeams.AddAsync(team);
            }
            await DbContext.SaveChangesAsync();

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.NotNull(result.Value);
            Assert.Equal(query.Page, result.Value.CurrentPage);
            Assert.Equal(query.PageSize, result.Value.Results.Count);
        }
    }
}