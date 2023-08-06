using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace online_selling.Migrations
{
    public partial class Migration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Orders",
                newName: "Price");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 21,
                column: "Password",
                value: "$2a$11$rWAGxr6WCvXBYBW6BOSRwufMAVWDEaUZ.rBinuKh0jTuhAxqCYhLe");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Orders",
                newName: "Amount");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 21,
                column: "Password",
                value: "$2a$11$U6EPwKFXmaYEfnI8nj.a/eyP5oMZSnjb7fzZJG/.LM.mqdjjQBCuC");
        }
    }
}
