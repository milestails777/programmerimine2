using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.ProjectTeams
{
    public class SaveProjectTeamCommandHandler : IRequestHandler<SaveProjectTeamCommand, OperationResult>
    {
        private readonly IProjectTeamRepository _dbContext;

        public SaveProjectTeamCommandHandler(IProjectTeamRepository dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveProjectTeamCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            ProjectTeam team;
            if (request.Id == 0)
            {
                team = new ProjectTeam
                {
                    ProjectId = request.ProjectId,
                    UserId = request.UserId
                };
                await _dbContext.SaveAsync(team);
            }
            else
            {
                team = await _dbContext.GetByIdAsync(request.Id);
                if (team != null)
                {
                    team.ProjectId = request.ProjectId;
                    team.UserId = request.UserId;
                    await _dbContext.SaveAsync(team);
                }
                
            }

            return result;
        }
    }
}
