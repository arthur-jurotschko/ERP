using GHOST.TalentosCortes.Domain.Entities.MasterUsers;
using GHOST.TalentosCortes.Infrastructured.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace GHOST.TalentosCortes.Infrastructured.Data.Mappings
{

    public class MasterUserMapping : EntityTypeConfiguration<MasterUser>

    {
        public override void Map(EntityTypeBuilder<MasterUser> builder)
        {
            builder.Property(e => e.Nome)
                .HasColumnType("varchar(150)")
                .IsRequired();

            builder.Property(e => e.Email)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(e => e.CPF)
                .HasColumnType("varchar(11)")
                .HasMaxLength(11)
                .IsRequired();

            builder.Ignore(e => e.ValidationResult);

            builder.Ignore(e => e.CascadeMode);

            builder.ToTable("MasterUsers");

        }
    }
}
