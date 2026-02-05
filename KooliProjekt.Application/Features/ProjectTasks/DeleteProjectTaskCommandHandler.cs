using KooliProjekt.Application.Data;
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
    public class DeleteProjectTaskCommandHandler : IRequestHandler<DeleteProjectTaskCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteProjectTaskCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeleteProjectTaskCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var team = await _dbContext.ProjectTasks.FindAsync(new object[] { request.Id }, cancellationToken);
            if (team == null)
            {
                result.AddError("Project task not found.");
                return result;
            }

            _dbContext.ProjectTasks.Remove(team);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
