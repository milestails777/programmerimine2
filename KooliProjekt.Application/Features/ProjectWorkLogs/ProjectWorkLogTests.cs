using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.ProjectWorkLogs;
using Xunit;

namespace KooliProjekt.Application.Features
{
    public class ProjectWorkLogTests : TestBase
    {
        [Fact]
        public void Get_throws_if_dbcontext_is_null()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new GetProjectWLQueryHandler(null);
            });
        }

        [Fact]
        public async Task Get_should_return_object_if_object_exists()
        {
            // Arrange
            var query = new GetProjectWLQuery { Id = 1 };
            var workLog = new ProjectWorkLog { Title = "Test ToDo List" };
            var handler = new GetProjectWLQueryHandler(DbContext);
            await DbContext.ProjectWorkLogs.AddAsync(workLog);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.HasErrors);
            Assert.NotNull(result.Value);
            Assert.Equal(1, result.Value.Id);
        }

        [Fact]
        public async Task Get_should_return_null_if_object_does_not_exist()
        {
            // Arrange
            var query = new GetProjectWLQuery { Id = 101 };
            var workLog = new ProjectWorkLog { Title = "Test ToDo List" };
            var handler = new GetProjectWLQueryHandler(DbContext);
            await DbContext.ProjectWorkLogs.AddAsync(workLog);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.HasErrors);
            Assert.Null(result.Value);
        }
    }
}
