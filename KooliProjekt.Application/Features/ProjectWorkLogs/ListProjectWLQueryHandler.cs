using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.ProjectWorkLogs;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.ProjectWorkLogs
{
    public class ListProjectWLQueryHandler : IRequestHandler<ListProjectWLQuery, OperationResult<PagedResult<ProjectWorkLog>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListProjectWLQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<OperationResult<PagedResult<ProjectWorkLog>>> Handle(ListProjectWLQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var result = new OperationResult<PagedResult<ProjectWorkLog>>();

            if (request.Page <= 0 || request.PageSize <= 0)
            {
                result.Value = new PagedResult<ProjectWorkLog>();
                return result;
            }

            var query = _dbContext.ProjectWorkLogs.AsQueryable();

            
            if (!string.IsNullOrWhiteSpace(request.Title))
            {
                query = query.Where(wl => wl.Description != null && wl.Description.Contains(request.Title));
            }

            

            result.Value = await query
                .OrderBy(wl => wl.Date)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}