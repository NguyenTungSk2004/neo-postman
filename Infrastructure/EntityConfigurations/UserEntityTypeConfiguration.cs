using Domain.AggregatesModel.UserAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).ValueGeneratedNever();

        builder.OwnsOne(u => u.Email, email =>
        {
            email.Property(e => e.Address)
                 .HasColumnName("Email")
                 .IsRequired()
                 .HasMaxLength(255);
        });
    }
}