using Microsoft.EntityFrameworkCore.Migrations;

namespace app.Migrations
{
    public partial class decimalType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Valor",
                table: "Periodos",
                type: "decimal(12, 2)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "Valor",
                table: "Movimentacaos",
                type: "decimal(12, 2)",
                nullable: false,
                oldClrType: typeof(decimal));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Valor",
                table: "Periodos",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(12, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Valor",
                table: "Movimentacaos",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(12, 2)");
        }
    }
}
