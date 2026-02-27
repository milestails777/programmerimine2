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

            ProjectTask task;
            if (request.Id == 0)
            {
                task = new ProjectTask
                {
                    ProjectId = request.ProjectId,
                    UserId = request.UserId,
                    Name = request.Name,
                    StartDate = request.StartDate,
                    Price = request.Price,
                    Description = request.Description,
                    Status = request.Status
                };

                await _dbContext.ProjectTasks.AddAsync(task, cancellationToken);
            }
            else
            {
                task = await _dbContext.ProjectTasks.FindAsync(new object[] { request.Id }, cancellationToken);
                if (task == null)
                {
                    result.AddError("Cannot find task with ID " + request.Id);
                    return result;
                }

                // Map updated values
                task.ProjectId = request.ProjectId;
                task.UserId = request.UserId;
                task.Name = request.Name;
                task.StartDate = request.StartDate;
                task.Price = request.Price;
                task.Description = request.Description;
                task.Status = request.Status;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
