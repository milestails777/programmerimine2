using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.ProjectWorkLogs
{
    [ExcludeFromCodeCoverage]
    public class DeleteProjectWLCommand : IRequest<OperationResult>, IBaseRequest, ITransactional
    {
        public int Id { get; set; }
    }
}
