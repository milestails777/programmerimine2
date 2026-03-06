using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.ProjectTasks
{
    public class GetProjectTaskQueryHandler : IRequestHandler<GetProjectTaskQuery, OperationResult<ProjectTaskDto>>
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

        public async Task<OperationResult<ProjectTaskDto>> Handle(GetProjectTaskQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var result = new OperationResult<ProjectTaskDto>();

            if (request.Id <= 0)
            {
                // invalid id -> return empty successful result
                return result;
            }

            result.Value = await _dbContext
                .ProjectTasks
                .Include(t => t.Project)
                .Include(t => t.User)
                .AsNoTracking()
                .Select(task => new ProjectTaskDto
                {
                    Id = task.Id,
                    Name = task.Name,
                    StartDate = task.StartDate,
                    Price = task.Price,
                    Description = task.Description,
                    Status = task.Status,
                    ProjectId = task.ProjectId,
                    UserId = task.UserId,
                })
                .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

            return result;
        }
    }
}
