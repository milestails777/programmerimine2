using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Projects
{
    public class ProjectsQueryHandler : IRequestHandler<ProjectsQuery, OperationResult<PagedResult<Project>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ProjectsQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<Project>>> Handle(ProjectsQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<Project>>();

            try
            {
                var page = Math.Max(1, request.Page);
                var pageSize = Math.Clamp(request.PageSize, 1, 100);

                var query = _dbContext
                    .Projects
                    .Include(p => p.Tasks)
                    .AsNoTracking()
                    .OrderBy(p => p.Name);

                var totalCount = await query.CountAsync(cancellationToken);

                var items = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(cancellationToken);

                result.Value = new PagedResult<Project>(items, totalCount, page, pageSize);
            }
            catch (Exception ex)
            {
                result.AddError("Error while fetching projects: " + ex.Message);
            }

            return result;
        }
    }
}
