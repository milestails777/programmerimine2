using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.ProjectWorkLogs
{
    public class GetProjectWLQueryHandler : IRequestHandler<GetProjectWLQuery, OperationResult<ProjectWorkLog>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetProjectWLQueryHandler(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            _dbContext = dbContext;
        }

        public async Task<OperationResult<ProjectWorkLog>> Handle(GetProjectWLQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<ProjectWorkLog>();
            result.Value = await _dbContext
                .ProjectWorkLogs
                .AsNoTracking()
                .FirstOrDefaultAsync(wl => wl.Id == request.Id, cancellationToken);

            return result;
        }
    }
}
