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

            var list = new Project();
            if (request.Id != 0)
            {
                list = await _projectRepository.GetByIdAsync(request.Id);
            }

            list.Name = request.Title;

            await _projectRepository.SaveAsync(list);

            return result;
        }
    }
}
