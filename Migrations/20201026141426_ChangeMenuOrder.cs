using Microsoft.EntityFrameworkCore.Migrations;

namespace app.Migrations
{
    public partial class ChangeMenuOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Icone", "Rota", "Titulo" },
                values: new object[] { "fas fa-home", "/principal", "Início" });

            migrationBuilder.UpdateData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Icone", "Rota", "Titulo" },
                values: new object[] { "fas fa-cogs", "/settings", "Configurações" });

            /* migrationBuilder.AddForeignKey(
                name: "FK_Movimentacaos_Categorias_CategoriaId",
                table: "Movimentacaos",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict); */
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /* migrationBuilder.DropForeignKey(
                name: "FK_Movimentacaos_Categorias_CategoriaId",
                table: "Movimentacaos"); */

            migrationBuilder.UpdateData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Icone", "Rota", "Titulo" },
                values: new object[] { "fas fa-cogs", "/settings", "Configurações" });

            migrationBuilder.UpdateData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Icone", "Rota", "Titulo" },
                values: new object[] { "fas fa-file-upload", "/upload", "Carregar Arquivos" });
        }
    }
}
