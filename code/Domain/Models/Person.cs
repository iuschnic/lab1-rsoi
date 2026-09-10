using Domain.Exceptions;

namespace Domain.Models;

public class Person
{
    public const int NameMaxLength = 50;
    public const int AddressMaxLength = 100;
    public const int WorkMaxLength = 100;

    public int Id { get; private set; }
    public string Name { get; private set; }
    public int? Age { get; private set; }
    public string? Address { get; private set; }
    public string? Work { get; private set; }

    private Person(int id, string name, int? age = null, string? address = null, string? work = null)
    {
        Validate(name, age, address, work);
        Id = id;
        Name = name;
        Age = age;
        Address = address;
        Work = work;
    }

    public static Person Create(string name, int? age = null, string? address = null, string? work = null)
    {
        return new Person(0, name, age, address, work);
    }

    public static Person Restore(int id, string name, int? age = null, string? address = null, string? work = null)
    {
        return new Person(id, name, age, address, work);
    }

    public void Update(string newName, int? newAge = null, string? newAddress = null, string? newWork = null)
    {
        Validate(newName, newAge, newAddress, newWork);
        Name = newName;
        Age = newAge;
        Address = newAddress;
        Work = newWork;
    }

    private static void Validate(string name, int? age = null, string? address = null, string? work = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainValidationException("Name is required");
        if (name.Length > NameMaxLength)
            throw new DomainValidationException($"Name cannot exceed {NameMaxLength} characters");
        if (age.HasValue && age < 0)
            throw new DomainValidationException("Age cannot be negative");
        if (address != null && string.IsNullOrWhiteSpace(address))
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new DomainValidationException("Address should not be whitespace or empty string if not null");
            if (address.Length > AddressMaxLength)
                throw new DomainValidationException($"Address cannot exceed {AddressMaxLength} characters");
        }
        if (work != null && string.IsNullOrWhiteSpace(work))
        {
            if (string.IsNullOrWhiteSpace(work))
                throw new DomainValidationException("Work should not be whitespace or empty string if not null");
            if (work.Length > WorkMaxLength)
                throw new DomainValidationException($"Work cannot exceed {WorkMaxLength} characters");
        }
    }
}
