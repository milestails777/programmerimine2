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
    public class SaveProjectTeamCommandHandler : IRequestHandler<SaveProjectTeamCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveProjectTeamCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveProjectTeamCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            // Implement your logic for saving the project team here
            // Example: Validate, add, or update ProjectTeam entities

            await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
