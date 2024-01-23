using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingWebApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddModelsTodb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reservations_provoles_PROVOLES_MOVIES_ID_PROVOLES_MOVIES_NAME_PROVOLES_Date_Time_PROVOLES_CINEMAS_ID_ProvolesContentAdminId",
                table: "reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_reservations",
                table: "reservations");

            migrationBuilder.DropIndex(
                name: "IX_reservations_PROVOLES_MOVIES_ID_PROVOLES_MOVIES_NAME_PROVOLES_Date_Time_PROVOLES_CINEMAS_ID_ProvolesContentAdminId",
                table: "reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_provoles",
                table: "provoles");

            migrationBuilder.DeleteData(
                table: "provoles",
                keyColumns: new[] { "CinemasID", "CONTENT_ADMIN_ID", "MOVIES_ID", "MOVIES_NAME", "ShowDateTime" },
                keyValues: new object[] { 1, 0, 0, "The Shawshank Redemption", new DateTime(2024, 1, 24, 19, 57, 48, 360, DateTimeKind.Local).AddTicks(7522) });

            migrationBuilder.DeleteData(
                table: "provoles",
                keyColumns: new[] { "CinemasID", "CONTENT_ADMIN_ID", "MOVIES_ID", "MOVIES_NAME", "ShowDateTime" },
                keyValues: new object[] { 0, 0, 0, "The Shawshank Redemption", new DateTime(2024, 1, 25, 12, 57, 48, 360, DateTimeKind.Local).AddTicks(7515) });

            migrationBuilder.DeleteData(
                table: "provoles",
                keyColumns: new[] { "CinemasID", "CONTENT_ADMIN_ID", "MOVIES_ID", "MOVIES_NAME", "ShowDateTime" },
                keyValues: new object[] { 0, 0, 0, "The Shawshank Redemption", new DateTime(2024, 1, 26, 19, 57, 48, 360, DateTimeKind.Local).AddTicks(7520) });

            migrationBuilder.DeleteData(
                table: "provoles",
                keyColumns: new[] { "CinemasID", "CONTENT_ADMIN_ID", "MOVIES_ID", "MOVIES_NAME", "ShowDateTime" },
                keyValues: new object[] { 2, 0, 1, "The Godfather", new DateTime(2024, 1, 25, 19, 57, 48, 360, DateTimeKind.Local).AddTicks(7525) });

            migrationBuilder.AlterColumn<DateTime>(
                name: "PROVOLES_Date_Time",
                table: "reservations",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ShowDateTime",
                table: "provoles",
                type: "datetime2(0)",
                precision: 0,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldPrecision: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_reservations",
                table: "reservations",
                columns: new[] { "PROVOLES_MOVIES_ID", "PROVOLES_MOVIES_NAME", "PROVOLES_CINEMAS_ID", "PROVOLES_ID", "CUSTOMERS_ID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_provoles",
                table: "provoles",
                columns: new[] { "MOVIES_ID", "MOVIES_NAME", "ID", "CinemasID", "CONTENT_ADMIN_ID" });

            migrationBuilder.InsertData(
                table: "provoles",
                columns: new[] { "CinemasID", "CONTENT_ADMIN_ID", "ID", "MOVIES_ID", "MOVIES_NAME", "ShowDateTime" },
                values: new object[,]
                {
                    { 0, 0, 0, 0, "The Shawshank Redemption", new DateTime(2024, 1, 28, 11, 11, 21, 997, DateTimeKind.Local).AddTicks(5605) },
                    { 0, 0, 1, 0, "The Shawshank Redemption", new DateTime(2024, 1, 29, 18, 11, 21, 997, DateTimeKind.Local).AddTicks(5613) },
                    { 1, 0, 2, 0, "The Shawshank Redemption", new DateTime(2024, 1, 27, 18, 11, 21, 997, DateTimeKind.Local).AddTicks(5616) },
                    { 2, 0, 3, 1, "The Godfather", new DateTime(2024, 1, 28, 18, 11, 21, 997, DateTimeKind.Local).AddTicks(5619) }
                });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "user_name",
                keyValue: "al",
                column: "create_time",
                value: new DateTime(2024, 1, 23, 11, 11, 21, 997, DateTimeKind.Local).AddTicks(5281));

            migrationBuilder.CreateIndex(
                name: "IX_reservations_PROVOLES_MOVIES_ID_PROVOLES_MOVIES_NAME_PROVOLES_ID_PROVOLES_CINEMAS_ID_ProvolesContentAdminId",
                table: "reservations",
                columns: new[] { "PROVOLES_MOVIES_ID", "PROVOLES_MOVIES_NAME", "PROVOLES_ID", "PROVOLES_CINEMAS_ID", "ProvolesContentAdminId" });

            migrationBuilder.AddForeignKey(
                name: "FK_reservations_provoles_PROVOLES_MOVIES_ID_PROVOLES_MOVIES_NAME_PROVOLES_ID_PROVOLES_CINEMAS_ID_ProvolesContentAdminId",
                table: "reservations",
                columns: new[] { "PROVOLES_MOVIES_ID", "PROVOLES_MOVIES_NAME", "PROVOLES_ID", "PROVOLES_CINEMAS_ID", "ProvolesContentAdminId" },
                principalTable: "provoles",
                principalColumns: new[] { "MOVIES_ID", "MOVIES_NAME", "ID", "CinemasID", "CONTENT_ADMIN_ID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reservations_provoles_PROVOLES_MOVIES_ID_PROVOLES_MOVIES_NAME_PROVOLES_ID_PROVOLES_CINEMAS_ID_ProvolesContentAdminId",
                table: "reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_reservations",
                table: "reservations");

            migrationBuilder.DropIndex(
                name: "IX_reservations_PROVOLES_MOVIES_ID_PROVOLES_MOVIES_NAME_PROVOLES_ID_PROVOLES_CINEMAS_ID_ProvolesContentAdminId",
                table: "reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_provoles",
                table: "provoles");

            migrationBuilder.DeleteData(
                table: "provoles",
                keyColumns: new[] { "CinemasID", "CONTENT_ADMIN_ID", "ID", "MOVIES_ID", "MOVIES_NAME" },
                keyValues: new object[] { 0, 0, 0, 0, "The Shawshank Redemption" });

            migrationBuilder.DeleteData(
                table: "provoles",
                keyColumns: new[] { "CinemasID", "CONTENT_ADMIN_ID", "ID", "MOVIES_ID", "MOVIES_NAME" },
                keyValues: new object[] { 0, 0, 1, 0, "The Shawshank Redemption" });

            migrationBuilder.DeleteData(
                table: "provoles",
                keyColumns: new[] { "CinemasID", "CONTENT_ADMIN_ID", "ID", "MOVIES_ID", "MOVIES_NAME" },
                keyValues: new object[] { 1, 0, 2, 0, "The Shawshank Redemption" });

            migrationBuilder.DeleteData(
                table: "provoles",
                keyColumns: new[] { "CinemasID", "CONTENT_ADMIN_ID", "ID", "MOVIES_ID", "MOVIES_NAME" },
                keyValues: new object[] { 2, 0, 3, 1, "The Godfather" });

            migrationBuilder.AlterColumn<DateTime>(
                name: "PROVOLES_Date_Time",
                table: "reservations",
                type: "datetime2(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ShowDateTime",
                table: "provoles",
                type: "datetime2(0)",
                precision: 0,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldPrecision: 0,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_reservations",
                table: "reservations",
                columns: new[] { "PROVOLES_MOVIES_ID", "PROVOLES_MOVIES_NAME", "PROVOLES_CINEMAS_ID", "PROVOLES_Date_Time", "CUSTOMERS_ID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_provoles",
                table: "provoles",
                columns: new[] { "MOVIES_ID", "MOVIES_NAME", "ShowDateTime", "CinemasID", "CONTENT_ADMIN_ID" });

            migrationBuilder.InsertData(
                table: "provoles",
                columns: new[] { "CinemasID", "CONTENT_ADMIN_ID", "MOVIES_ID", "MOVIES_NAME", "ShowDateTime", "ID" },
                values: new object[,]
                {
                    { 1, 0, 0, "The Shawshank Redemption", new DateTime(2024, 1, 24, 19, 57, 48, 360, DateTimeKind.Local).AddTicks(7522), 2 },
                    { 0, 0, 0, "The Shawshank Redemption", new DateTime(2024, 1, 25, 12, 57, 48, 360, DateTimeKind.Local).AddTicks(7515), 0 },
                    { 0, 0, 0, "The Shawshank Redemption", new DateTime(2024, 1, 26, 19, 57, 48, 360, DateTimeKind.Local).AddTicks(7520), 1 },
                    { 2, 0, 1, "The Godfather", new DateTime(2024, 1, 25, 19, 57, 48, 360, DateTimeKind.Local).AddTicks(7525), 3 }
                });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "user_name",
                keyValue: "al",
                column: "create_time",
                value: new DateTime(2024, 1, 20, 12, 57, 48, 360, DateTimeKind.Local).AddTicks(7265));

            migrationBuilder.CreateIndex(
                name: "IX_reservations_PROVOLES_MOVIES_ID_PROVOLES_MOVIES_NAME_PROVOLES_Date_Time_PROVOLES_CINEMAS_ID_ProvolesContentAdminId",
                table: "reservations",
                columns: new[] { "PROVOLES_MOVIES_ID", "PROVOLES_MOVIES_NAME", "PROVOLES_Date_Time", "PROVOLES_CINEMAS_ID", "ProvolesContentAdminId" });

            migrationBuilder.AddForeignKey(
                name: "FK_reservations_provoles_PROVOLES_MOVIES_ID_PROVOLES_MOVIES_NAME_PROVOLES_Date_Time_PROVOLES_CINEMAS_ID_ProvolesContentAdminId",
                table: "reservations",
                columns: new[] { "PROVOLES_MOVIES_ID", "PROVOLES_MOVIES_NAME", "PROVOLES_Date_Time", "PROVOLES_CINEMAS_ID", "ProvolesContentAdminId" },
                principalTable: "provoles",
                principalColumns: new[] { "MOVIES_ID", "MOVIES_NAME", "ShowDateTime", "CinemasID", "CONTENT_ADMIN_ID" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
