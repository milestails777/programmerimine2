using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.ProjectWorkLogs
{
    public class SaveProjectWLCommandHandler : IRequestHandler<SaveProjectWLCommand, OperationResult>
    {
        private readonly IProjectWLRepository _dbContext;

        public SaveProjectWLCommandHandler(IProjectWLRepository dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveProjectWLCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            ProjectWorkLog workLog;
            if (request.Id == 0)
            {
                workLog = new ProjectWorkLog();
                workLog.TaskId = request.TaskId;
                workLog.UserId = request.UserId;
                

                await _dbContext.SaveAsync(workLog);
            }
            else
            {
                workLog = await _dbContext.GetByIdAsync(request.Id);
                if (workLog != null)
                {
                    workLog.TaskId = request.TaskId;
                    workLog.UserId = request.UserId;
                    

                    await _dbContext.SaveAsync(workLog);
                }
            }

            return result;
        }
    }
}
