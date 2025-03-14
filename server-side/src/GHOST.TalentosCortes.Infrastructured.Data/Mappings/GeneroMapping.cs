using GHOST.TalentosCortes.Domain.Entities.Comercial.Clientes;
using GHOST.TalentosCortes.Infrastructured.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GHOST.TalentosCortes.Infrastructured.Data.Mappings
{
    public class GeneroMapping : EntityTypeConfiguration<Genero>
    {
        public override void Map(EntityTypeBuilder<Genero> builder)
        {

            builder.Property(e => e.Nome)
                .HasColumnType("varchar(150)")
                .IsRequired();

            builder.Ignore(e => e.ValidationResult);

            builder.Ignore(e => e.CascadeMode);

            builder.ToTable("Generos");


        }
    }
}


