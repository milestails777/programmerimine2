using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.ProjectUsers;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.UnitTests.Features
{
    public class ProjectUserTests : TestBase
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
                new GetProjectUserQueryHandler(null);
            });
        }

        [Fact]
        public async Task Get_should_return_existing_user()
        {
            var query = new GetProjectUserQuery { Id = 1 };
            var handler = new GetProjectUserQueryHandler(DbContext);

            var user = new ProjectUser { Name = "Test User", Email = "u@test.local" };
            await DbContext.ProjectUsers.AddAsync(user);
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
                new ListProjectUserQueryHandler(null);
            });
        }

        [Fact]
        public async Task List_should_return_page_of_users()
        {
            var query = new ListProjectUserQuery { Page = 1, PageSize = 5 };
            var handler = new ListProjectUserQueryHandler(DbContext);

            for (var i = 1; i <= 8; i++)
            {
                var user = new ProjectUser { Name = $"User {i}", Email = $"u{i}@test.local" };
                await DbContext.ProjectUsers.AddAsync(user);
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