using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.ProjectUsers
{
    [ExcludeFromCodeCoverage]
    public class SaveProjectUserCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
    }
}
