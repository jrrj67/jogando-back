using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class Roles_Users_Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { 1, new DateTime(2021, 11, 27, 18, 2, 54, 714, DateTimeKind.Utc).AddTicks(426), "admin", new DateTime(2021, 11, 27, 18, 2, 54, 714, DateTimeKind.Utc).AddTicks(435) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Name", "Password", "RoleId", "UpdatedAt" },
                values: new object[] { 1, new DateTime(2021, 11, 27, 18, 2, 54, 541, DateTimeKind.Utc).AddTicks(1680), "ricardojunior.6767@gmail.com", "Admin", "$2a$11$cFy/SFwB56IpyclV.3y08e3vOcWas2FTSZRXdIbv4/vaM5z4d/2lS", 1, new DateTime(2021, 11, 27, 18, 2, 54, 711, DateTimeKind.Utc).AddTicks(7742) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
