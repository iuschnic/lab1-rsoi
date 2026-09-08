using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Application.Dtos;
using Application.OutPorts;
using Application.Exceptions;
using Storage.Context;
using Storage.Mappers;

namespace Storage.Repositories;

public class PersonRepository(AppDbContext context) : IPersonRepository
{
    public async Task<IReadOnlyList<PersonDto>> GetAllAsync(CancellationToken ct = default)
    {
        return (await context.Persons
            .AsNoTracking()
            .ToListAsync(ct)).ToDto();
    }

    public async Task<PersonDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var entity = await context.Persons
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id, ct);
        return entity?.ToDto();
    }

    public async Task<Person?> GetDomainByIdAsync(int id, CancellationToken ct = default)
    {
        var entity = await context.Persons
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id, ct);
        return entity?.ToDomain();
    }

    public async Task<int> AddAsync(Person person, CancellationToken ct = default)
    {
        var entity = person.ToDb();
        entity.Id = 0;
        context.Persons.Add(entity);
        await context.SaveChangesAsync(ct);
        return entity.Id;
    }

    public async Task<PersonDto> UpdateAsync(Person person, CancellationToken ct = default)
    {
        var entity = await context.Persons
            .FirstOrDefaultAsync(p => p.Id == person.Id, ct);
        if (entity == null)
            throw new AppNotFoundException();

        entity.Name = person.Name;
        entity.Age = person.Age;
        entity.Address = person.Address;
        entity.Work = person.Work;

        await context.SaveChangesAsync(ct);
        return entity.ToDto();
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        await context.Persons.Where(p => p.Id == id).ExecuteDeleteAsync(ct);
    }
}