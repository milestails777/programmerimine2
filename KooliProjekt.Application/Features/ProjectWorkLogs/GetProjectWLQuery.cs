using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace KooliProjekt.Application.Features.ProjectWorkLogs
{
    [ExcludeFromCodeCoverage]
    public class GetProjectWLQuery : IRequest<OperationResult<ProjectWorkLog>>, IBaseRequest
    {
        public int Id { get; set; }
    }
}
