using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Projects
{
    // 16.01.2026 - ProjectDetailsDto
    public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, OperationResult<ProjectDetailsDto>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetProjectsQueryHandler(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            _dbContext = dbContext;
        }

        public async Task<OperationResult<ProjectDetailsDto>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var result = new OperationResult<ProjectDetailsDto>();

            if (request.Id <= 0)
            {
                return result;
            }

            result.Value = await _dbContext
                .Projects
                .Include(list => list.Tasks)
                .Where(list => list.Id == request.Id)
                .Select(list => new ProjectDetailsDto
                {
                    Id = list.Id,
                    Title = list.Name,
                    Items = new System.Collections.Generic.List<ProjectDto>()
                })
                .FirstOrDefaultAsync(cancellationToken);

            return result;
        }
    }
}