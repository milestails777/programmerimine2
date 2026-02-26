using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.ProjectUsers;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.ProjectUsers
{
    public class SaveProjectUserCommandHandler : IRequestHandler<SaveProjectUserCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveProjectUserCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<OperationResult> Handle(SaveProjectUserCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var result = new OperationResult();
            if (request.Id < 0)
            {
                result.AddError("Request ID cannot be negative");
                return result;
            }

            var team = new ProjectTeam();
            if (request.Id == 0)
            {
                
                team.ProjectId = request.ProjectId;
                team.UserId = request.UserId;

                await _dbContext.ProjectTeams.AddAsync(team, cancellationToken);
            }
            else
            {
                team = await _dbContext.ProjectTeams.FindAsync(new object[] { request.Id }, cancellationToken);
                if (team == null)
                {
                    result.AddError("Cannot find team with ID " + request.Id);
                    return result;
                }

               
                team.ProjectId = request.ProjectId;
                team.UserId = request.UserId;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
