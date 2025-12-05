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

namespace KooliProjekt.Application.Features.Projects
{
    public class SaveProjectCommandHandler : IRequestHandler<SaveProjectCommand, OperationResult>
    {
        private readonly IProjectRepository _projectRepository;

        public SaveProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<OperationResult> Handle(SaveProjectCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            Project project;
            if (request.Id == 0)
            {
                project = new Project
                {
                    Name = request.Name
                };
                await _projectRepository.SaveAsync(project);
            }
            else
            {
                project = await _projectRepository.GetByIdAsync(request.Id);
                if (project == null)
                {
                    result.AddError("Project not found.");
                    return result;
                }

                project.Name = request.Name;
                await _projectRepository.SaveAsync(project);
            }

            return result;
        }
    }
}
