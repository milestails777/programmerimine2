using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.Projects;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace KooliProjekt.Application.Features.Projects
{
    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteProjectCommandHandler(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var result = new OperationResult();

            if (request.Id <= 0)
            {
                return result;
            }

            // Kustutamine üle relatsioonide (vihje: CASCADE DELETE)
            //await _dbContext
            //    .ToDoLists
            //    .Where(t => t.Id == request.Id)
            //    .ExecuteDeleteAsync();  <-- InMemory ei toeta veel ExecuteDeleteAsync meetodit

            var list = await _dbContext
                .Projects
                .Include(t => t.ProjectTeams)
                .FirstOrDefaultAsync(t => t.Id == request.Id);

            if (list == null)
            {
                return result;
            }

            _dbContext.ProjectTeams.RemoveRange(list.ProjectTeams);
            _dbContext.Projects.Remove(list);

            await _dbContext.SaveChangesAsync();

            return result;
        }
    }
}
