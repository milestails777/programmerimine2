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
    public class SaveProjectWLCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public int UserId { get; set; }

        // Added properties expected by unit tests
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
