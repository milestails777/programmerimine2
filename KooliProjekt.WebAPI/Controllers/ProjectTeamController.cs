using Azure;
using KooliProjekt.Application.Features.Projects;
using KooliProjekt.WebAPI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KooliProjekt.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectTeamController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectTeamController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetProjectTasks([FromQuery] ProjectTeamQuery query)
        {
            var result = await _mediator.Send(query);

            return Result(result);
        }

        // Get project by id
        // Save project

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(DeleteProjectTeamCommand command)
        {
            var response = await _mediator.Send(command);

            return Result(response);
        }
    }

}