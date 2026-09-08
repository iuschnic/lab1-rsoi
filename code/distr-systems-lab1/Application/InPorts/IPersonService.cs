using Application.Dtos;
using Application.Exceptions;
using Domain.Models;
namespace Application.InPorts;

public interface IPersonService
{
    public Task<IReadOnlyList<PersonDto>> GetAllAsync(CancellationToken ct = default);
    public Task<PersonDto> GetByIdAsync(int id, CancellationToken ct = default);
    public Task<int> CreateAsync(PersonRequestDto request, CancellationToken ct = default);
    public Task<PersonDto> UpdateAsync(int id, PersonRequestDto request, CancellationToken ct = default);
    public Task DeleteAsync(int id, CancellationToken ct = default);
}
