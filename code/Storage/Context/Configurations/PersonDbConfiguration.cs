using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.Models;
using Domain.Models;

namespace Storage.Context.Configurations;

public class PersonDbConfiguration : IEntityTypeConfiguration<PersonDb>
{
    public void Configure(EntityTypeBuilder<PersonDb> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.Name).IsRequired().HasMaxLength(Person.NameMaxLength);
        builder.Property(p => p.Address).HasMaxLength(Person.AddressMaxLength);
        builder.Property(p => p.Work).HasMaxLength(Person.WorkMaxLength);
    }
}