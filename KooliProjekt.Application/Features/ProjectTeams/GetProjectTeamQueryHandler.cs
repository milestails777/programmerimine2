using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Features.Projects;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.ProjectTeams
{
    public class GetProjectTeamQueryHandler : IRequestHandler<GetProjectsQuery, OperationResult<object>>
    {
        private readonly IProjectTeamRepository _dbContext;

        public GetProjectTeamQueryHandler(IProjectTeamRepository dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<object>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();
            var team = await _dbContext.GetByIdAsync(request.Id);

            result.Value = new // Anonymous object
            {
                Id = team.Id,
                Title = team.User?.Name,
                Items = new[]
                {
                    new
                    {
                        Id = team.User?.Id,
                        Title = team.User?.Name,
                        IsDone = false
                    }
                }
            };

            return result;
        }
    }
}
