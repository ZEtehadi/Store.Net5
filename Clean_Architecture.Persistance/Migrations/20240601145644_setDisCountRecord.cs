using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Clean_Architecture.Persistance.Migrations
{
    public partial class setDisCountRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DisCounts",
                columns: new[] { "Id", "DateTime", "DisCountCode", "IsActive", "IsRemoved", "Percent", "RemovedTime", "UpdateTime" },
                values: new object[] { 1L, new DateTime(2024, 6, 1, 19, 26, 43, 569, DateTimeKind.Local).AddTicks(2510), "NULL DisCount", true, false, 0f,null,null});

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateTime",
                value: new DateTime(2024, 6, 1, 19, 26, 43, 562, DateTimeKind.Local).AddTicks(9016));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateTime",
                value: new DateTime(2024, 6, 1, 19, 26, 43, 569, DateTimeKind.Local).AddTicks(1214));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateTime",
                value: new DateTime(2024, 6, 1, 19, 26, 43, 569, DateTimeKind.Local).AddTicks(1492));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DisCounts",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateTime",
                value: new DateTime(2024, 5, 30, 1, 34, 23, 343, DateTimeKind.Local).AddTicks(5639));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateTime",
                value: new DateTime(2024, 5, 30, 1, 34, 23, 348, DateTimeKind.Local).AddTicks(7518));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateTime",
                value: new DateTime(2024, 5, 30, 1, 34, 23, 348, DateTimeKind.Local).AddTicks(7930));
        }
    }
}
