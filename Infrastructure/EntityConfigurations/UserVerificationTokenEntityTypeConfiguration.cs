using Domain.AggregatesModel.VerificationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations
{
    public class UserVerificationTokenEntityTypeConfiguration: IEntityTypeConfiguration<UserVerificationToken>
    {
        public void Configure(EntityTypeBuilder<UserVerificationToken> builder)
        {
            builder.ToTable("UserVerificationTokens");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property<long>("UserId");
        }
    }
}