using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.Projects;
using KooliProjekt.Application.Features.ProjectTasks;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.ProjectTasks
{
    public class DeleteProjectTaskCommandHandler : IRequestHandler<DeleteProjectTaskCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteProjectTaskCommandHandler(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeleteProjectTaskCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var result = new OperationResult();

            if (request.Id <= 0)
            {
                return result;
            }

            var task = await _dbContext
                .ProjectTasks
                .Include(t => t.WorkLogs)
                .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

            if (task == null)
            {
                return result;
            }

            if (task.WorkLogs != null)
            {
                _dbContext.ProjectWorkLogs.RemoveRange(task.WorkLogs);
            }

            _dbContext.ProjectTasks.Remove(task);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}

