using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.ProjectTeams
{
    public class GetProjectTeamQueryHandler : IRequestHandler<GetProjectTeamQuery, OperationResult<ProjectTeamDto>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetProjectTeamQueryHandler(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            _dbContext = dbContext;
        }

        public async Task<OperationResult<ProjectTeamDto>> Handle(GetProjectTeamQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<ProjectTeamDto>();
            result.Value = await _dbContext
                .ProjectTeams
                .Where(list => list.Id == request.Id)
                .Select(list => new ProjectTeamDto 
                {
                    Id = list.Id,
                    ProjectId = list.ProjectId,
                    ProjectName = list.Project.Name,
                    // veel andmeid project team kohta
                })
                .FirstOrDefaultAsync(cancellationToken);

            return result;
        }
    }
}
