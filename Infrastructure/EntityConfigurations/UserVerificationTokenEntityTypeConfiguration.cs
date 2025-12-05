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

            builder.HasOne(x => x.User) // Mối quan hệ một-nhiều với User
                   .WithMany()
                   .HasForeignKey(x => x.UserId) // Sử dụng thuộc tính UserId trực tiếp trong UserVerificationToken
                   .IsRequired() // Đảm bảo rằng UserId là bắt buộc
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}