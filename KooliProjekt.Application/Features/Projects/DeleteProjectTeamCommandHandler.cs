using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Projects
{
    public class DeleteProjectTeamCommandHandler : IRequestHandler<DeleteProjectTeamCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteProjectTeamCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeleteProjectTeamCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var project = await _dbContext.Projects.FindAsync(new object[] { request.Id }, cancellationToken);
            if (project == null)
            {
                result.AddError("Project not found.");
                return result;
            }

            _dbContext.Projects.Remove(project);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
