using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.ProjectUsers;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.ProjectUsers
{
    public class ListProjectUserQueryHandler : IRequestHandler<ListProjectUserQuery, OperationResult<PagedResult<ProjectUser>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListProjectUserQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<OperationResult<PagedResult<ProjectUser>>> Handle(ListProjectUserQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var result = new OperationResult<PagedResult<ProjectUser>>();

            if (request.Page <= 0 || request.PageSize <= 0)
            {
                //result.Value = new PagedResult<ProjectUser>();
                return result;
            }

            var query = _dbContext.ProjectUsers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Title))
            {
                query = query.Where(u => u.Name != null && u.Name.Contains(request.Title));
            }

            result.Value = await query
                .OrderBy(u => u.Name)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}