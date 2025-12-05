using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Features.ProjectWorkLogs;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.ProjectUsers
{
    public class SaveProjectUserCommandHandler : IRequestHandler<SaveProjectUserCommand, OperationResult>
    {
        private readonly IProjectUserRepository _dbContext;

        public SaveProjectUserCommandHandler(IProjectUserRepository dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveProjectUserCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            ProjectUser user;
            if (request.Id == 0)
            {
                user = new ProjectUser();
                user.Id = request.UserId;


                await _dbContext.SaveAsync(user);
            }
            else
            {
                user = await _dbContext.GetByIdAsync(request.Id);
                if (user != null)
                {
                    user.Id = request.UserId;


                    await _dbContext.SaveAsync(user);
                }
            }

            return result;
        }
    }
}
