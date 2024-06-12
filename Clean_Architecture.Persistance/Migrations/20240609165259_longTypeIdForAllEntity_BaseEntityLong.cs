using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Clean_Architecture.Persistance.Migrations
{
    public partial class longTypeIdForAllEntity_BaseEntityLong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.UpdateData(
                table: "DisCounts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateTime",
                value: new DateTime(2024, 6, 9, 21, 22, 58, 773, DateTimeKind.Local).AddTicks(7629));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateTime",
                value: new DateTime(2024, 6, 9, 21, 22, 58, 767, DateTimeKind.Local).AddTicks(1373));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateTime",
                value: new DateTime(2024, 6, 9, 21, 22, 58, 773, DateTimeKind.Local).AddTicks(5468));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateTime",
                value: new DateTime(2024, 6, 9, 21, 22, 58, 773, DateTimeKind.Local).AddTicks(5893));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CostToPay",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "DisCounts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateTime",
                value: new DateTime(2024, 6, 2, 0, 26, 12, 708, DateTimeKind.Local).AddTicks(565));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateTime",
                value: new DateTime(2024, 6, 2, 0, 26, 12, 702, DateTimeKind.Local).AddTicks(8460));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateTime",
                value: new DateTime(2024, 6, 2, 0, 26, 12, 707, DateTimeKind.Local).AddTicks(9437));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateTime",
                value: new DateTime(2024, 6, 2, 0, 26, 12, 707, DateTimeKind.Local).AddTicks(9680));
        }
    }
}
