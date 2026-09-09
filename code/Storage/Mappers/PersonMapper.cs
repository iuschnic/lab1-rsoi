using Application.Dtos;
using Domain.Models;
using Storage.Models;

namespace Storage.Mappers;

public static class PersonMapper
{
    public static PersonDb ToDb(this Person person) => new()
    {
        Id = person.Id,
        Name = person.Name,
        Age = person.Age,
        Address = person.Address,
        Work = person.Work,
    };

    public static Person ToDomain(this PersonDb person)
        => Person.Restore(person.Id, person.Name, person.Age, person.Address, person.Work);

    public static PersonDto ToDto(this PersonDb person) => new()
    {
        Id = person.Id,
        Name = person.Name,
        Age = person.Age,
        Address = person.Address,
        Work = person.Work,
    };

    public static IReadOnlyList<PersonDto> ToDto(this IEnumerable<PersonDb> persons)
        => persons.Select(p => p.ToDto()).ToList();
}