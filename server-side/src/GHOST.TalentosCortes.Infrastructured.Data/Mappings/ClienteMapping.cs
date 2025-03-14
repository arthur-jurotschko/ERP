using GHOST.TalentosCortes.Domain.Entities.Comercial.Clientes;
using GHOST.TalentosCortes.Infrastructured.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;

namespace GHOST.TalentosCortes.Infrastructured.Data.Mappings
{
   public class ClienteMapping : EntityTypeConfiguration<Cliente>
    {
        public override void Map(EntityTypeBuilder<Cliente> builder)
        {
      
           builder.Property(x => x.NomeCompleto)
               .HasColumnType("varchar(100)")
               .IsRequired();

            builder.Property(x => x.CPF)
                .HasColumnType("varchar(150)");

            builder.Property(x => x.RG)
                .HasColumnType("varchar(150)")
                .IsRequired();

            builder.Property(x => x.TelResidencial)
                .HasColumnType("varchar(250)");
                
            builder.Property(e => e.TelCelular)
                .HasColumnType("varchar(250)")
                .IsRequired();

            builder.Property(e => e.Email)
                .HasColumnType("varchar(150)")
                .IsRequired();

            builder.Property(e => e.RedeSocial)
                .HasColumnType("varchar(150)")
                .IsRequired();


            builder.Ignore(e => e.ValidationResult);

            builder.Ignore(e => e.CascadeMode);

            builder.ToTable("Clientes");

            builder.HasOne(e => e.MasterUser)
            .WithMany(o => o.Clientes)
                .HasForeignKey(e => e.MasterUserId);

            builder.HasOne(e => e.Genero)
            .WithMany(e => e.Clientes)
                .HasForeignKey(e => e.GeneroId)
                .IsRequired(true);

        }
    }
}

