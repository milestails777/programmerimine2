using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.Projects;
using KooliProjekt.Application.Features.ProjectWorkLogs;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace KooliProjekt.Application.Features.ProjectWorkLogs
{
    public class DeleteProjectWLCommandHandler : IRequestHandler<DeleteProjectWLCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteProjectWLCommandHandler(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeleteProjectWLCommand request, CancellationToken cancellationToken)
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

            var workLog = await _dbContext
                .ProjectWorkLogs
                .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

            if (workLog == null)
            {
                return result;
            }

            _dbContext.ProjectWorkLogs.Remove(workLog);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}

