using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Projects
{
    public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, OperationResult<object>>
    {
        private readonly IProjectRepository _projectRepository;

        public GetProjectsQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<OperationResult<object>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();
            var list = await _projectRepository.GetByIdAsync(request.Id);

            result.Value = new // Anonymous object
            {
                Id = list.Id,
                Title = list.Name, 
                Items = list.Tasks.Select(item => new
                {
                    Id = item.Id,
                    Title = item.Name, 
                    IsDone = item.Status == "Done" 
                })
            };

            return result;
        }
    }
}
