using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.ProjectTeams
{
    public class GetProjectTeamQueryHandler : IRequestHandler<GetProjectTeamQuery, OperationResult<object>>
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

        public async Task<OperationResult<object>> Handle(GetProjectTeamQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();
            result.Value = await _dbContext
                .ProjectTeams
                .Where(list => list.Id == request.Id)
                .Select(list => new // Anonymous object
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
