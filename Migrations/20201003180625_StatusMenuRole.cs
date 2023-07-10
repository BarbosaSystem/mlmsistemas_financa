using Microsoft.EntityFrameworkCore.Migrations;

namespace app.Migrations
{
    public partial class StatusMenuRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "MenuRoles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumns: new[] { "MenuId", "RolesId" },
                keyValues: new object[] { 1, "a18be9c0-aa65-4af8-bd17-00bd9344e575" },
                column: "Status",
                value: true);

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumns: new[] { "MenuId", "RolesId" },
                keyValues: new object[] { 2, "a18be9c0-aa65-4af8-bd17-00bd9344e575" },
                column: "Status",
                value: true);

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumns: new[] { "MenuId", "RolesId" },
                keyValues: new object[] { 3, "a18be9c0-aa65-4af8-bd17-00bd9344e575" },
                column: "Status",
                value: true);

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumns: new[] { "MenuId", "RolesId" },
                keyValues: new object[] { 4, "a18be9c0-aa65-4af8-bd17-00bd9344e575" },
                column: "Status",
                value: true);

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumns: new[] { "MenuId", "RolesId" },
                keyValues: new object[] { 5, "a18be9c0-aa65-4af8-bd17-00bd9344e575" },
                column: "Status",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "MenuRoles");
        }
    }
}
