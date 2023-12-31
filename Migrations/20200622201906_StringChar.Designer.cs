﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using app.Contexto;

namespace app.Migrations
{
    [DbContext(typeof(DemoDbContext))]
    [Migration("20200622201906_StringChar")]
    partial class StringChar
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("app.Models.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasMaxLength(70);

                    b.Property<string>("Nome")
                        .HasMaxLength(70);

                    b.Property<string>("Telefone")
                        .HasMaxLength(70);

                    b.HasKey("Id");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("app.Models.Financa.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .HasMaxLength(70);

                    b.HasKey("Id");

                    b.ToTable("Categorias");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nome = "Pessoal"
                        },
                        new
                        {
                            Id = 2,
                            Nome = "Casal"
                        });
                });

            modelBuilder.Entity("app.Models.Financa.Movimentacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoriaId");

                    b.Property<DateTime>("Data");

                    b.Property<string>("Descricao")
                        .HasMaxLength(70);

                    b.Property<bool>("Status");

                    b.Property<decimal>("Valor");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.ToTable("Movimentacaos");
                });

            modelBuilder.Entity("app.Models.Financa.Periodo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataFinal");

                    b.Property<DateTime>("DataInicio");

                    b.Property<decimal>("Valor");

                    b.Property<string>("referencia")
                        .HasMaxLength(70);

                    b.Property<bool>("status");

                    b.HasKey("Id");

                    b.ToTable("Periodos");
                });

            modelBuilder.Entity("app.Models.Settings.Menu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Icone")
                        .HasMaxLength(70);

                    b.Property<string>("Perfil")
                        .HasMaxLength(70);

                    b.Property<string>("Rota")
                        .HasMaxLength(70);

                    b.Property<bool>("Status");

                    b.Property<string>("Titulo")
                        .HasMaxLength(70);

                    b.HasKey("Id");

                    b.ToTable("Menu");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Icone = "fas fa-cogs",
                            Rota = "/settings",
                            Status = true,
                            Titulo = "Configurações"
                        },
                        new
                        {
                            Id = 2,
                            Icone = "fas fa-calendar-alt",
                            Rota = "/periodo",
                            Status = true,
                            Titulo = "Periodo"
                        },
                        new
                        {
                            Id = 3,
                            Icone = "fas fa-money-bill-wave",
                            Rota = "/movimentos",
                            Status = true,
                            Titulo = "Registros"
                        },
                        new
                        {
                            Id = 4,
                            Icone = "fas fa-user-friends",
                            Rota = "/categoria",
                            Status = true,
                            Titulo = "Categoria"
                        },
                        new
                        {
                            Id = 5,
                            Icone = "fas fa-file-upload",
                            Rota = "/upload",
                            Status = true,
                            Titulo = "Carregar Arquivos"
                        });
                });

            modelBuilder.Entity("app.Models.Settings.MenuRoles", b =>
                {
                    b.Property<int>("MenuId");

                    b.Property<string>("RolesId")
                        .HasMaxLength(70);

                    b.HasKey("MenuId", "RolesId");

                    b.ToTable("MenuRoles");

                    b.HasData(
                        new
                        {
                            MenuId = 1,
                            RolesId = "a18be9c0-aa65-4af8-bd17-00bd9344e575"
                        },
                        new
                        {
                            MenuId = 2,
                            RolesId = "a18be9c0-aa65-4af8-bd17-00bd9344e575"
                        },
                        new
                        {
                            MenuId = 3,
                            RolesId = "a18be9c0-aa65-4af8-bd17-00bd9344e575"
                        },
                        new
                        {
                            MenuId = 4,
                            RolesId = "a18be9c0-aa65-4af8-bd17-00bd9344e575"
                        },
                        new
                        {
                            MenuId = 5,
                            RolesId = "a18be9c0-aa65-4af8-bd17-00bd9344e575"
                        });
                });

            modelBuilder.Entity("app.Models.Financa.Movimentacao", b =>
                {
                    b.HasOne("app.Models.Financa.Categoria", "Categoria")
                        .WithMany("Movimentacao")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
