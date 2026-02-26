using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.ProjectUsers;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.ProjectTeams
{
    public class ListProjectTeamQueryHandler : IRequestHandler<ListProjectTeamQuery, OperationResult<PagedResult<ProjectTeam>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListProjectTeamQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<OperationResult<PagedResult<ProjectTeam>>> Handle(ListProjectTeamQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var result = new OperationResult<PagedResult<ProjectTeam>>();

            if (request.Page <= 0 || request.PageSize <= 0)
            {
                result.Value = new PagedResult<ProjectTeam>();
                return result;
            }

            var query = _dbContext.ProjectTeams.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Title))
            {
                // ProjectTeam does not have a Name property; search by related User's Name
                query = query.Where(u => u.User != null && u.User.Name != null && u.User.Name.Contains(request.Title));
            }

            result.Value = await query
                .OrderBy(u => u.User != null ? u.User.Name : string.Empty)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}