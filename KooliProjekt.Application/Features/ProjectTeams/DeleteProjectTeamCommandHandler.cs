using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.Projects;
using KooliProjekt.Application.Features.ProjectTeams;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace KooliProjekt.Application.Features.ProjectTeams
{
    public class DeleteProjectTeamCommandHandler : IRequestHandler<DeleteProjectTeamCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteProjectTeamCommandHandler(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeleteProjectTeamCommand request, CancellationToken cancellationToken)
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

            var team = await _dbContext
                .ProjectTeams
                .Include(t => t.ProjectId)
                .FirstOrDefaultAsync(t => t.Id == request.Id);

            if (team == null)
            {
                return result;
            }

            
            _dbContext.ProjectTeams.Remove(team);

            await _dbContext.SaveChangesAsync();

            return result;
        }
    }
}

