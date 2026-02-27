using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.ProjectTasks
{
    public class ListProjectTaskQuery : IRequest<OperationResult<Infrastructure.Paging.PagedResult<ProjectTask>>>, IBaseRequest
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}