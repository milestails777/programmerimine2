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

namespace KooliProjekt.Application.Features.ProjectWorkLogs
{
    public class GetProjectWLQueryHandler : IRequestHandler<GetProjectsQuery, OperationResult<object>>
    {
        private readonly IProjectWLRepository _dbContext;

        public GetProjectWLQueryHandler(IProjectWLRepository dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<object>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();
            var projectWorkLog = await _dbContext.GetByIdAsync(request.Id);

            if (projectWorkLog == null)
            {
                result.AddError("Project work log not found.");
                return result;
            }

            
            result.Value = new
            {
                Id = projectWorkLog.Id,
                TaskId = projectWorkLog.TaskId,
                TaskName = projectWorkLog.Task?.Name,
                
            };

            return result;
        }
    }
}
