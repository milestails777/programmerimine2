using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Features.ProjectTeams;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.ProjectTasks
{
    public class SaveProjectTaskCommandHandler : IRequestHandler<SaveProjectTaskCommand, OperationResult>
    {
        private readonly IProjectTaskRepository _dbContext;

        public SaveProjectTaskCommandHandler(IProjectTaskRepository dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveProjectTaskCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            ProjectTask task;
            if (request.Id == 0)
            {
                task = new ProjectTask
                {
                    ProjectId = request.ProjectId,
                    UserId = request.UserId
                };
                await _dbContext.SaveAsync(task);
            }
            else
            {
                task = await _dbContext.GetByIdAsync(request.Id);
                if (task != null)
                {
                    task.ProjectId = request.ProjectId;
                    task.UserId = request.UserId;
                    await _dbContext.SaveAsync(task);
                }

            }

            return result;
        }
    }
}
