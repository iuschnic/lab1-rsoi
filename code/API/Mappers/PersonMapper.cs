using Api.Requests;
using Api.Responses;
using Application.Dtos;

namespace Api.Mappers;

public static class PersonContractMapper
{
    public static PersonRequestDto ToDto(this PersonRequest request) => new()
    {
        Name = request.Name,
        Age = request.Age,
        Address = request.Address,
        Work = request.Work,
    };

    public static PersonResponse ToResponse(this PersonDto dto) => new()
    {
        Id = dto.Id,
        Name = dto.Name,
        Age = dto.Age,
        Address = dto.Address,
        Work = dto.Work,
    };

    public static IReadOnlyList<PersonResponse> ToResponse(this IEnumerable<PersonDto> dtos)
        => dtos.Select(d => d.ToResponse()).ToList();
}