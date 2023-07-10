using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace app.Migrations
{
    public partial class StringChar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 70, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 70, nullable: true),
                    Telefone = table.Column<string>(maxLength: 70, nullable: true),
                    Email = table.Column<string>(maxLength: 70, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Titulo = table.Column<string>(maxLength: 70, nullable: true),
                    Rota = table.Column<string>(maxLength: 70, nullable: true),
                    Icone = table.Column<string>(maxLength: 70, nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    Perfil = table.Column<string>(maxLength: 70, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuRoles",
                columns: table => new
                {
                    MenuId = table.Column<int>(nullable: false),
                    RolesId = table.Column<string>(maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuRoles", x => new { x.MenuId, x.RolesId });
                });

            migrationBuilder.CreateTable(
                name: "Periodos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    DataFinal = table.Column<DateTime>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false),
                    referencia = table.Column<string>(maxLength: 70, nullable: true),
                    status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Periodos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movimentacaos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Data = table.Column<DateTime>(nullable: false),
                    Descricao = table.Column<string>(maxLength: 70, nullable: true),
                    Valor = table.Column<decimal>(nullable: false),
                    CategoriaId = table.Column<int>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimentacaos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movimentacaos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Pessoal" },
                    { 2, "Casal" }
                });

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "Id", "Icone", "Perfil", "Rota", "Status", "Titulo" },
                values: new object[,]
                {
                    { 1, "fas fa-cogs", null, "/settings", true, "Configurações" },
                    { 2, "fas fa-calendar-alt", null, "/periodo", true, "Periodo" },
                    { 3, "fas fa-money-bill-wave", null, "/movimentos", true, "Registros" },
                    { 4, "fas fa-user-friends", null, "/categoria", true, "Categoria" },
                    { 5, "fas fa-file-upload", null, "/upload", true, "Carregar Arquivos" }
                });

            migrationBuilder.InsertData(
                table: "MenuRoles",
                columns: new[] { "MenuId", "RolesId" },
                values: new object[,]
                {
                    { 1, "a18be9c0-aa65-4af8-bd17-00bd9344e575" },
                    { 2, "a18be9c0-aa65-4af8-bd17-00bd9344e575" },
                    { 3, "a18be9c0-aa65-4af8-bd17-00bd9344e575" },
                    { 4, "a18be9c0-aa65-4af8-bd17-00bd9344e575" },
                    { 5, "a18be9c0-aa65-4af8-bd17-00bd9344e575" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movimentacaos_CategoriaId",
                table: "Movimentacaos",
                column: "CategoriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "MenuRoles");

            migrationBuilder.DropTable(
                name: "Movimentacaos");

            migrationBuilder.DropTable(
                name: "Periodos");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
