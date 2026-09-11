using Domain.Exceptions;
using Domain.Models;
using FluentAssertions;
using Xunit;

namespace Domain.Tests.Models;

public class PersonTests
{
    private const string ValidName = "validName";
    private const int ValidAge = 30;
    private const string ValidAddress = "validAddress";
    private const string ValidWork = "validWork";

    // успешное создание 

    [Fact]
    public void Create_WithValidData_SetsAllPropertiesAndZeroId()
    {
        var person = Person.Create(ValidName, ValidAge, ValidAddress, ValidWork);

        person.Id.Should().Be(0);
        person.Name.Should().Be(ValidName);
        person.Age.Should().Be(ValidAge);
        person.Address.Should().Be(ValidAddress);
        person.Work.Should().Be(ValidWork);
    }

    [Fact]
    public void Create_WithNullOptionalFields_IsAllowed()
    {
        var person = Person.Create(ValidName);

        person.Name.Should().Be(ValidName);
        person.Age.Should().BeNull();
        person.Address.Should().BeNull();
        person.Work.Should().BeNull();
    }

    // валидация имени

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithMissingOrWhitespaceName_Throws(string? invalidName)
    {
        var act = () => Person.Create(invalidName!);

        act.Should().Throw<DomainValidationException>()
            .WithMessage("*Name is required*");
    }

    [Fact]
    public void Create_WithNameAtMaxLength_IsAllowed()
    {
        var maxName = new string('a', Person.NameMaxLength);

        var person = Person.Create(maxName);

        person.Name.Should().Be(maxName);
    }

    [Fact]
    public void Create_WithNameExceedingMaxLength_Throws()
    {
        var tooLongName = new string('a', Person.NameMaxLength + 1);

        var act = () => Person.Create(tooLongName);

        act.Should().Throw<DomainValidationException>()
            .WithMessage($"*{Person.NameMaxLength}*");
    }

    // валидация возраста

    [Fact]
    public void Create_WithNegativeAge_Throws()
    {
        var act = () => Person.Create(ValidName, age: -1);

        act.Should().Throw<DomainValidationException>()
            .WithMessage("*Age cannot be negative*");
    }

    [Fact]
    public void Create_WithZeroAge_IsAllowed()
    {
        var person = Person.Create(ValidName, age: 0);

        person.Age.Should().Be(0);
    }

    // валидация адреса

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithEmptyOrWhitespaceAddress_Throws(string invalidAddress)
    {
        var act = () => Person.Create(ValidName, address: invalidAddress);

        act.Should().Throw<DomainValidationException>()
            .WithMessage("*Address*");
    }

    [Fact]
    public void Create_WithAddressExceedingMaxLength_Throws()
    {
        var tooLongAddress = new string('a', Person.AddressMaxLength + 1);

        var act = () => Person.Create(ValidName, address: tooLongAddress);

        act.Should().Throw<DomainValidationException>()
            .WithMessage($"*{Person.AddressMaxLength}*");
    }

    [Fact]
    public void Create_WithAddressAtMaxLength_IsAllowed()
    {
        var maxAddress = new string('a', Person.AddressMaxLength);

        var person = Person.Create(ValidName, address: maxAddress);

        person.Address.Should().Be(maxAddress);
    }

    // ввалидация работы

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithEmptyOrWhitespaceWork_Throws(string invalidWork)
    {
        var act = () => Person.Create(ValidName, work: invalidWork);

        act.Should().Throw<DomainValidationException>()
            .WithMessage("*Work*");
    }

    [Fact]
    public void Create_WithWorkExceedingMaxLength_Throws()
    {
        var tooLongWork = new string('a', Person.WorkMaxLength + 1);

        var act = () => Person.Create(ValidName, work: tooLongWork);

        act.Should().Throw<DomainValidationException>()
            .WithMessage($"*{Person.WorkMaxLength}*");
    }

    // проверка Restore (восстановление доменной модели из базы данных)

    [Fact]
    public void Restore_SetsProvidedFields()
    {
        var id = 1;
        var person = Person.Restore(id, ValidName, ValidAge, ValidAddress, ValidWork);

        person.Id.Should().Be(id);
        person.Name.Should().Be(ValidName);
        person.Age.Should().Be(ValidAge);
        person.Address.Should().Be(ValidAddress);
        person.Work.Should().Be(ValidWork);
    }

    // проверка Update

    [Fact]
    public void Update_WithValidData_ChangesAllFields()
    {
        var person = Person.Create(ValidName, ValidAge, ValidAddress, ValidWork);
        var newName = "newName";
        var newAge = 99;
        var newAddress = "newAddress";
        var newWork = "newWork";

        person.Update(newName, newAge, newAddress, newWork);

        person.Name.Should().Be(newName);
        person.Age.Should().Be(newAge);
        person.Address.Should().Be(newAddress);
        person.Work.Should().Be(newWork);
    }

    [Fact]
    public void Update_WithInvalidData_ThrowsAndDoesNotMutateState()
    {
        var person = Person.Create(ValidName, ValidAge, ValidAddress, ValidWork);
        var invalidName = "";
        var newAge = 99;
        var newAddress = "newAddress";
        var newWork = "newWork";

        var act = () => person.Update(invalidName, newAge, newAddress, newWork);

        act.Should().Throw<DomainValidationException>();
        person.Name.Should().Be(ValidName);
        person.Age.Should().Be(ValidAge);
        person.Address.Should().Be(ValidAddress);
        person.Work.Should().Be(ValidWork);
    }

    [Fact]
    public void Update_WithNullOptionals_ClearsFields()
    {
        var person = Person.Create(ValidName, ValidAge, ValidAddress, ValidWork);

        person.Update(ValidName);

        person.Age.Should().Be(ValidAge);
        person.Address.Should().Be(ValidAddress);
        person.Work.Should().Be(ValidWork);
    }
}