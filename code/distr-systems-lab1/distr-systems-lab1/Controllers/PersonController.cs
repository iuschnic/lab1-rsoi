using Api.Mappers;
using Api.Requests;
using Api.Responses;
using Application.InPorts;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/persons")]
public class PersonController(IPersonService service) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<PersonResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<PersonResponse>>> GetPersons(CancellationToken ct)
    {
        var persons = await service.GetAllAsync(ct);
        return Ok(persons.ToResponse());
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(PersonResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PersonResponse>> GetPersonById(int id, CancellationToken ct)
    {
        var person = await service.GetByIdAsync(id, ct);
        return Ok(person.ToResponse());
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreatePerson([FromBody] PersonRequest request, CancellationToken ct)
    {
        var createdId = await service.CreateAsync(request.ToDto(), ct);
        return CreatedAtAction(nameof(GetPersonById),
            routeValues: new { id = createdId },
            value: null);
    }

    [HttpPatch("{id:int}")]
    [ProducesResponseType(typeof(PersonResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PersonResponse>> EditPerson(int id, [FromBody] PersonRequest request,
        CancellationToken ct)
    {
        var person = await service.UpdateAsync(id, request.ToDto(), ct);
        return Ok(person.ToResponse());
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeletePerson(int id, CancellationToken ct)
    {
        await service.DeleteAsync(id, ct);
        return NoContent();
    }
}