using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Features.Projects;
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
        private readonly IProjectTaskRepository _dbContext;

        public GetProjectTaskQueryHandler(IProjectTaskRepository dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<object>> Handle(GetProjectTaskQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();
            var task = await _dbContext.GetByIdAsync(request.Id);

            result.Value = new // Anonymous object
            {
                Id = task.Id,
                Title = task.User?.Name,
                Items = new[]
                {
                    new
                    {
                        Id = task.User?.Id,
                        Title = task.User?.Name,
                        IsDone = false
                    }
                }
            };

            return result;
        }
    }
}
