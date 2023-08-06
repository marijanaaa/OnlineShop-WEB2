using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace online_selling.Migrations
{
    public partial class Migration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 21,
                column: "Password",
                value: "$2a$11$1mUbe381AdhQgHUYH0yAf.GVtbZzRxmEPM6/6rEjRuA3zO.5HRXwK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 21,
                column: "Password",
                value: "$2a$11$rWAGxr6WCvXBYBW6BOSRwufMAVWDEaUZ.rBinuKh0jTuhAxqCYhLe");
        }
    }
}
