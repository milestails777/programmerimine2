using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.ProjectWorkLogs
{
    public class SaveProjectWLCommandHandler : IRequestHandler<SaveProjectWLCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveProjectWLCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<OperationResult> Handle(SaveProjectWLCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var result = new OperationResult();
            if (request.Id < 0)
            {
                result.AddError("Request ID cannot be negative");
                return result;
            }

            var workLog = new ProjectWorkLog();
            if (request.Id == 0)
            {
                
                workLog.TaskId = request.TaskId;
                workLog.UserId = request.UserId;

                await _dbContext.ProjectWorkLogs.AddAsync(workLog);
            }
            else
            {
                workLog = await _dbContext.ProjectWorkLogs.FindAsync(request.Id);
                if (workLog == null)
                {
                    result.AddError("Cannot find list with ID " + request.Id);
                    return result;
                }

                
                workLog.TaskId = request.TaskId;
                workLog.UserId = request.UserId;
            }

            await _dbContext.SaveChangesAsync();

            return result;
        }
    }
}
