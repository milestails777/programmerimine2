using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Features.Projects;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.ProjectUsers
{
    public class GetProjectUserQueryHandler : IRequestHandler<GetProjectUserQuery, OperationResult<object>>
    {
        private readonly IProjectUserRepository _dbContext;

        public GetProjectUserQueryHandler(IProjectUserRepository dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<object>> Handle(GetProjectUserQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();
            var user = await _dbContext.GetByIdAsync(request.Id);

            result.Value = new // Anonymous object
            {
                Id = user.Id,
                Title = user.Phone,
                Items = new[]
                {
                    new
                    {
                        Id = user.Email,
                        Title = user.Name,
                        IsDone = false
                    }
                }
            };

            return result;
        }
    }
}
  
