using System.Linq;
using app.Models;
using app.Models.Financa;
using app.Models.Settings;
using Microsoft.EntityFrameworkCore;

namespace app.Contexto
{
    public partial class DemoDbContext : DbContext
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Menu>(x => x.ToTable("Menu"));

            modelBuilder.Entity<MenuRoles>().HasKey(al => new { al.MenuId, al.RolesId });

            /* foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            } */

            //Setar todo atributo string com lengh 70
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(string)))
            {
                if (property.GetMaxLength() == null)
                    property.SetMaxLength(70);
            }


            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                // EF Core 1 & 2
                property.Relational().ColumnType = "decimal(12, 2)";

                // EF Core 3
                //property.SetColumnType("decimal(18, 6)");

                // EF Core 5
                //property.SetPrecision(18);
                //property.SetScale(6);
            }
            modelBuilder.Entity<Menu>().HasData(new Menu
            {
                Icone = "fas fa-home",
                Rota = "/principal",
                Status = true,
                Titulo = "Início",
                Id = 1
            });
            modelBuilder.Entity<Menu>().HasData(new Menu
            {
                Icone = "fas fa-calendar-alt",
                Rota = "/periodo",
                Status = true,
                Titulo = "Periodo",
                Id = 2
            });
            modelBuilder.Entity<Menu>().HasData(new Menu
            {
                Icone = "fas fa-money-bill-wave",
                Rota = "/movimentos",
                Status = true,
                Titulo = "Registros",
                Id = 3
            });
            modelBuilder.Entity<Menu>().HasData(new Menu
            {
                Icone = "fas fa-user-friends",
                Rota = "/categoria",
                Status = true,
                Titulo = "Categoria",
                Id = 4
            });
            modelBuilder.Entity<Menu>().HasData(new Menu
            {
                Icone = "fas fa-cogs",
                Rota = "/settings",
                Status = true,
                Titulo = "Configurações",
                Id = 5
            });

            modelBuilder.Entity<MenuRoles>().HasData(new MenuRoles
            {
                MenuId = 1,
                RolesId = "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                Status = true
            });
            modelBuilder.Entity<MenuRoles>().HasData(new MenuRoles
            {
                MenuId = 2,
                RolesId = "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                Status = true
            });
            modelBuilder.Entity<MenuRoles>().HasData(new MenuRoles
            {
                MenuId = 3,
                RolesId = "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                Status = true
            });
            modelBuilder.Entity<MenuRoles>().HasData(new MenuRoles
            {
                MenuId = 4,
                RolesId = "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                Status = true
            });
            modelBuilder.Entity<MenuRoles>().HasData(new MenuRoles
            {
                MenuId = 5,
                RolesId = "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                Status = true
            });

            modelBuilder.Entity<Categoria>().HasData(new Categoria
            {
                Id = 1,
                Nome = "Pessoal"
            });
            modelBuilder.Entity<Categoria>().HasData(new Categoria
            {
                Id = 2,
                Nome = "Casal"
            });
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Periodo> Periodos { get; set; }
        public DbSet<Movimentacao> Movimentacaos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuRoles> MenuRoles { get; set; }
    }
}
