using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingWebApplication.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "provoles",
                columns: new[] { "CinemasID", "CONTENT_ADMIN_ID", "MOVIES_ID", "MOVIES_NAME", "ShowDateTime", "ID" },
                values: new object[,]
                {
                    { 1, 0, 0, "The Shawshank Redemption", new DateTime(2024, 1, 24, 20, 8, 56, 307, DateTimeKind.Local).AddTicks(8463), 2 },
                    { 0, 0, 0, "The Shawshank Redemption", new DateTime(2024, 1, 25, 13, 8, 56, 307, DateTimeKind.Local).AddTicks(8453), 0 },
                    { 0, 0, 0, "The Shawshank Redemption", new DateTime(2024, 1, 26, 20, 8, 56, 307, DateTimeKind.Local).AddTicks(8459), 1 },
                    { 2, 0, 1, "The Godfather", new DateTime(2024, 1, 25, 20, 8, 56, 307, DateTimeKind.Local).AddTicks(8465), 3 }
                });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "user_name",
                keyValue: "al",
                column: "create_time",
                value: new DateTime(2024, 1, 20, 13, 8, 56, 307, DateTimeKind.Local).AddTicks(8152));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "provoles",
                keyColumns: new[] { "CinemasID", "CONTENT_ADMIN_ID", "MOVIES_ID", "MOVIES_NAME", "ShowDateTime" },
                keyValues: new object[] { 1, 0, 0, "The Shawshank Redemption", new DateTime(2024, 1, 24, 20, 8, 56, 307, DateTimeKind.Local).AddTicks(8463) });

            migrationBuilder.DeleteData(
                table: "provoles",
                keyColumns: new[] { "CinemasID", "CONTENT_ADMIN_ID", "MOVIES_ID", "MOVIES_NAME", "ShowDateTime" },
                keyValues: new object[] { 0, 0, 0, "The Shawshank Redemption", new DateTime(2024, 1, 25, 13, 8, 56, 307, DateTimeKind.Local).AddTicks(8453) });

            migrationBuilder.DeleteData(
                table: "provoles",
                keyColumns: new[] { "CinemasID", "CONTENT_ADMIN_ID", "MOVIES_ID", "MOVIES_NAME", "ShowDateTime" },
                keyValues: new object[] { 0, 0, 0, "The Shawshank Redemption", new DateTime(2024, 1, 26, 20, 8, 56, 307, DateTimeKind.Local).AddTicks(8459) });

            migrationBuilder.DeleteData(
                table: "provoles",
                keyColumns: new[] { "CinemasID", "CONTENT_ADMIN_ID", "MOVIES_ID", "MOVIES_NAME", "ShowDateTime" },
                keyValues: new object[] { 2, 0, 1, "The Godfather", new DateTime(2024, 1, 25, 20, 8, 56, 307, DateTimeKind.Local).AddTicks(8465) });

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
        }
    }
}
