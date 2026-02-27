using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.ProjectTasks
{
    public class ListProjectTaskQueryHandler : IRequestHandler<ListProjectTaskQuery, OperationResult<PagedResult<ProjectTask>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListProjectTaskQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<OperationResult<PagedResult<ProjectTask>>> Handle(ListProjectTaskQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var result = new OperationResult<PagedResult<ProjectTask>>();

            if (request.Page <= 0 || request.PageSize <= 0)
            {
                // return empty success result when invalid paging parameters
                return result;
            }

            result.Value = await _dbContext
                .ProjectTasks
                .Include(t => t.Project)
                .Include(t => t.User)
                .AsNoTracking()
                .OrderBy(t => t.ProjectId)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}