using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace PersonAPI_Second.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IMediator mediatr;
        public PersonController(IMediator mediator)
        {
            mediatr = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonById(Guid id)
        {
            var person = await mediatr.Send(new GetPersonQuery(id));

            if (person == null) return NotFound();
                return Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePerson(CreatePersonCommand command)
        {
            var personId = await mediatr.Send(command);

            if (Guid.Empty == personId) return BadRequest();

            await mediatr.Publish(new PersonCreatedNotification(personId));

            return Created($"/people/{personId}", new { id = personId });
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePerson(Guid id)
        {
            await mediatr.Send(new DeletePersonCommand(id));

            return NoContent();
        }

        [HttpPatch]
        public async Task<IActionResult> PatchPerson(UpdatePersonCommand command)
        {
            var personId = await mediatr.Send(command);

            if (Guid.Empty == personId) return BadRequest();

            return Created($"/people/{personId}", new { id = personId });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPeople()
        {
            var people = await mediatr.Send(new ListPersonQuery());

            return Ok(people);
        }

    }

}
