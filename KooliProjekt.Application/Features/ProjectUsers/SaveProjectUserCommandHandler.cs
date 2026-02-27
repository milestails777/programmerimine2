using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.ProjectUsers;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.ProjectUsers
{
    public class SaveProjectUserCommandHandler : IRequestHandler<SaveProjectUserCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveProjectUserCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<OperationResult> Handle(SaveProjectUserCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var result = new OperationResult();
            if (request.Id < 0)
            {
                result.AddError("Request ID cannot be negative");
                return result;
            }

            var user = new ProjectUser();
            if(request.Id > 0)
            {
                user = await _dbContext.ProjectUsers.FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);
                if(user == null)
                {
                    result.AddError("Cannot find user with ID " + request.Id);
                    return result;
                }
            }
            else
            {
                _dbContext.ProjectUsers.Add(user);
            }

            user.Address = request.Address;
            user.Email = request.Email;
            user.Name = request.Name;
            user.Phone = request.Phone;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
