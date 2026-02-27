    using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System;

namespace KooliProjekt.Application.Features.ProjectTasks
{
    public class SaveProjectTaskCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public int? UserId { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public decimal? Price { get; set; }

        public string? Description { get; set; }

        public string Status { get; set; }
    }
}
