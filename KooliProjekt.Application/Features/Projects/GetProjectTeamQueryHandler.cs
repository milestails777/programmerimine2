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

namespace KooliProjekt.Application.Features.Projects
{
    public class GetProjectTeamQueryHandler : IRequestHandler<GetProjectsQuery, OperationResult<object>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetProjectTeamQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<object>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();
            result.Value = await _dbContext
                .Projects
                .Where(list => list.Id == request.Id)
                .Select(list => new // Anonymous object
                {
                    Id = list.Id,
                    Title = list.Name,
                    Items = list.Tasks.Select(item => new
                    {
                        Id = item.Id,
                        Title = item.Name,
                        IsDone = item.Status == "Done"
                    })
                })
                .FirstOrDefaultAsync(cancellationToken); 

            return result;
        }
    }
}
