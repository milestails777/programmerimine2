using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Projects
{
    public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, OperationResult<object>>
    {
        private readonly IProjectRepository _dbContext;

        public GetProjectsQueryHandler(IProjectRepository dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<object>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();
            var project = await _dbContext.GetByIdAsync(request.Id);

            result.Value = new // Anonymous object
            {
                Id = project.Id,
                Title = project.Name,
                Items = project.Tasks.Select(item => new
                {
                    Id = item.Id,
                    Title = item.Name,
                    IsDone = item.Status == "Done"
                })
            };

            return result;
        }
    }
}
