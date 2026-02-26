using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.ProjectTasks
{
    public class GetProjectTaskQueryHandler : IRequestHandler<GetProjectTaskQuery, OperationResult<object>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetProjectTaskQueryHandler(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            _dbContext = dbContext;
        }

        public async Task<OperationResult<object>> Handle(GetProjectTaskQuery request, CancellationToken cancellationToken)
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
