using GHOST.TalentosCortes.Domain.Entities.Comercial.Clientes;
using GHOST.TalentosCortes.Infrastructured.Data.Extensions;
using GHOST.TalentosCortes.Infrastructured.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using System.IO;


namespace GHOST.TalentosCortes.Infrastructured.Data.Context
{
    public class TalentosCortesContext : DbContext
   {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Dashboard> Dashboards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
       {
           #region FluentAPI
           modelBuilder.AddConfiguration(new ClienteMapping());
           modelBuilder.AddConfiguration(new MasterUserMapping());
           modelBuilder.AddConfiguration(new StoredEventMap());
           modelBuilder.AddConfiguration(new EnderecoMapping());
           modelBuilder.AddConfiguration(new GeneroMapping());
           modelBuilder.AddConfiguration(new DashboardMapping());
            #endregion
            base.OnModelCreating(modelBuilder);
       }

       protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       {
           var config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

           optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
       }
   }
}
