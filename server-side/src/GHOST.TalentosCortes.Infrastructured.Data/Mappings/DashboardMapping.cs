using GHOST.TalentosCortes.Domain.Entities.Comercial.Clientes;
using GHOST.TalentosCortes.Infrastructured.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GHOST.TalentosCortes.Infrastructured.Data.Mappings
{
    public class DashboardMapping : EntityTypeConfiguration<Dashboard>
    {
        public override void Map(EntityTypeBuilder<Dashboard> builder)
        {
           
            builder.Property(e => e.YearDate)
                .HasColumnType("varchar(5)")
                .IsRequired();

            builder.Property(e => e.Janeiro)
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            builder.Property(e => e.Fevereiro)
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            builder.Property(e => e.Marco)
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            builder.Property(e => e.Abril)
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            builder.Property(e => e.Maio)
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            builder.Property(e => e.Junho)
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            builder.Property(e => e.Julho)
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            builder.Property(e => e.Agosto)
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            builder.Property(e => e.Setembro)
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            builder.Property(e => e.Outubro)
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            builder.Property(e => e.Novembro)
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            builder.Property(e => e.Dezembro)
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            builder.Ignore(e => e.ValidationResult);

            builder.Ignore(e => e.CascadeMode);

            builder.ToTable("Dashboards");


        }
    }
}


