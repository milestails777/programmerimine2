using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Projects
{
    public class SaveProjectCommandHandler : IRequestHandler<SaveProjectCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveProjectCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<OperationResult> Handle(SaveProjectCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var result = new OperationResult();
            if (request.Id < 0)
            {
                result.AddError("Request ID cannot be negative");
                return result;
            }

            Project project;
            if (request.Id == 0)
            {
                
                project = new Project
                {
                    Name = request.Name,
                    StartDate = DateTime.UtcNow,
                    DueDate = request.DueDate,
                    Budget = request.Budget,
                    PricePerHour = request.PricePerHour
                };

                await _dbContext.Projects.AddAsync(project);
            }
            else
            {
                project = await _dbContext.Projects.FindAsync(request.Id);
                if (project == null)
                {
                    result.AddError("Cannot find project with ID " + request.Id);
                    return result;
                }
            }

            project.Name = request.Name;
            project.StartDate = DateTime.UtcNow;
            project.DueDate = request.DueDate;
            project.Budget = request.Budget;
            project.PricePerHour = request.PricePerHour;

            await _dbContext.SaveChangesAsync();

            return result;
        }
    }
}
