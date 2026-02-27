using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.ProjectTeams
{
    [ExcludeFromCodeCoverage]
    public class GetProjectTeamQuery : IRequest<OperationResult<ProjectTeamDto>>, IBaseRequest
    {
        public int Id { get; set; }
    }
}
