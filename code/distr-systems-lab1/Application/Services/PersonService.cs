using Application.OutPorts;
using Application.Dtos;
using Application.Exceptions;
using Application.InPorts;
using Domain.Models;

namespace Application.Services;

public class PersonService(IPersonRepository repository) : IPersonService
{
    public Task<IReadOnlyList<PersonDto>> GetAllAsync(CancellationToken ct = default)
    {
        return repository.GetAllAsync(ct);
    }

    public async Task<PersonDto> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await repository.GetByIdAsync(id, ct)
               ?? throw new AppNotFoundException($"Person with id {id} not found");
    }

    public async Task<int> CreateAsync(PersonRequestDto request, CancellationToken ct = default)
    {
        var person = Person.Create(request.Name, request.Age, request.Address, request.Work);
        return await repository.AddAsync(person, ct);
    }

    public async Task<PersonDto> UpdateAsync(int id, PersonRequestDto request, CancellationToken ct = default)
    {
        var person = await repository.GetDomainByIdAsync(id, ct)
                     ?? throw new AppNotFoundException($"Person with id {id} not found");

        person.Update(request.Name, request.Age, request.Address, request.Work);
        return await repository.UpdateAsync(person, ct);
    }

    public Task<bool> DeleteAsync(int id, CancellationToken ct = default)
    {
        return repository.DeleteAsync(id, ct);
    }
}