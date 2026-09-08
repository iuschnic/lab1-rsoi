using Application.Dtos;
using Domain.Models;

namespace Application.OutPorts;

public interface IPersonRepository
{
    Task<IReadOnlyList<PersonDto>> GetAllAsync(CancellationToken ct = default);
    Task<PersonDto?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Person?> GetDomainByIdAsync(int id, CancellationToken ct = default);
    Task<int> AddAsync(Person person, CancellationToken ct = default);
    Task<PersonDto> UpdateAsync(Person person, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
}