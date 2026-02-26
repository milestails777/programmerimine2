using System.Diagnostics.CodeAnalysis;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.ProjectUsers
{
    [ExcludeFromCodeCoverage]
    public class ListProjectUserQuery : IRequest<OperationResult<PagedResult<ProjectUser>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public string Title { get; set; }
        public bool? IsDone { get; set; }
    }
}
