using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
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
    public class GetProjectUserQueryHandler : IRequestHandler<GetProjectUserQuery, OperationResult<ProjectUserDto>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetProjectUserQueryHandler(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            _dbContext = dbContext;
        }

        public async Task<OperationResult<ProjectUserDto>> Handle(GetProjectUserQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var result = new OperationResult<ProjectUserDto>();
            result.Value = await _dbContext
                .ProjectUsers
                .Where(list => list.Id == request.Id)
                .Select(list => new ProjectUserDto
                {
                    Id = list.Id,
                    Name = list.Name,
                    Address = list.Address,
                    Phone = list.Phone,
                    Email = list.Email
                })
                .FirstOrDefaultAsync(cancellationToken);

            return result;
        }
    }
}
