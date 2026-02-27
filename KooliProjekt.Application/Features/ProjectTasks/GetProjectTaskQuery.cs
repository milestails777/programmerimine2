using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace KooliProjekt.Application.Features.ProjectTasks
{
    [ExcludeFromCodeCoverage]
    public class GetProjectTaskQuery : IRequest<OperationResult<ProjectTask>>, IBaseRequest
    {
        public int Id { get; set; }
    }
}
