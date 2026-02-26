using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.ProjectTasks
{
    public class SaveProjectTaskCommandHandler : IRequestHandler<SaveProjectTaskCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveProjectTaskCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<OperationResult> Handle(SaveProjectTaskCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var result = new OperationResult();
            if (request.Id < 0)
            {
                result.AddError("Request ID cannot be negative");
                return result;
            }

            var task = new ProjectTask();
            if (request.Id == 0)
            {
                
                task.ProjectId = request.ProjectId;
                task.UserId = request.UserId;

                await _dbContext.ProjectTasks.AddAsync(task);
            }
            else
            {
                task = await _dbContext.ProjectTasks.FindAsync(request.Id);
                if (task == null)
                {
                    result.AddError("Cannot find task with ID " + request.Id);
                    return result;
                }

                
                task.ProjectId = request.ProjectId;
                task.UserId = request.UserId;
            }

            await _dbContext.SaveChangesAsync();

            return result;
        }
    }
}
