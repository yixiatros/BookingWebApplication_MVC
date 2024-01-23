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
            migrationBuilder.CreateTable(
                name: "cinemas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nchar(45)", fixedLength: true, maxLength: 45, nullable: false),
                    seats = table.Column<int>(type: "int", fixedLength: true, nullable: false),
                    _3D = table.Column<string>(name: "3D", type: "nchar(45)", fixedLength: true, maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cinemas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_name = table.Column<string>(type: "nchar(32)", fixedLength: true, maxLength: 32, nullable: false),
                    email = table.Column<string>(type: "nchar(35)", fixedLength: true, maxLength: 35, nullable: false),
                    password = table.Column<string>(type: "nchar(45)", fixedLength: true, maxLength: 45, nullable: false),
                    create_time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    salt = table.Column<string>(type: "nchar(45)", fixedLength: true, maxLength: 45, nullable: true),
                    role = table.Column<string>(type: "nchar(45)", fixedLength: true, maxLength: 45, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.user_name);
                });

            migrationBuilder.CreateTable(
                name: "admins",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nchar(45)", fixedLength: true, maxLength: 45, nullable: true),
                    UserName = table.Column<string>(type: "nchar(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admins", x => x.id);
                    table.ForeignKey(
                        name: "FK_admins_users_UserName",
                        column: x => x.UserName,
                        principalTable: "users",
                        principalColumn: "user_name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "content_admins",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nchar(45)", fixedLength: true, maxLength: 45, nullable: true),
                    UserName = table.Column<string>(type: "nchar(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_content_admins", x => x.id);
                    table.ForeignKey(
                        name: "FK_content_admins_users_UserName",
                        column: x => x.UserName,
                        principalTable: "users",
                        principalColumn: "user_name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nchar(45)", fixedLength: true, maxLength: 45, nullable: true),
                    UserName = table.Column<string>(type: "nchar(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.id);
                    table.ForeignKey(
                        name: "FK_customers_users_UserName",
                        column: x => x.UserName,
                        principalTable: "users",
                        principalColumn: "user_name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "movies",
                columns: table => new
                {
                    movie_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    movie_name = table.Column<string>(type: "nchar(45)", fixedLength: true, maxLength: 45, nullable: false),
                    movie_content = table.Column<string>(type: "nchar(45)", fixedLength: true, maxLength: 45, nullable: true),
                    movie_length = table.Column<int>(type: "int", nullable: false),
                    movie_type = table.Column<string>(type: "nchar(45)", fixedLength: true, maxLength: 45, nullable: true),
                    movie_summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    movie_director = table.Column<string>(type: "nchar(45)", fixedLength: true, maxLength: 45, nullable: true),
                    ContentAdminId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movies", x => new { x.movie_id, x.movie_name });
                    table.ForeignKey(
                        name: "FK_movies_content_admins_ContentAdminId",
                        column: x => x.ContentAdminId,
                        principalTable: "content_admins",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "provoles",
                columns: table => new
                {
                    MOVIES_ID = table.Column<int>(type: "int", nullable: false),
                    MOVIES_NAME = table.Column<string>(type: "nchar(45)", maxLength: 45, nullable: false),
                    CinemasID = table.Column<int>(type: "int", nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CONTENT_ADMIN_ID = table.Column<int>(type: "int", nullable: false),
                    ShowDateTime = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_provoles", x => new { x.MOVIES_ID, x.MOVIES_NAME, x.ID, x.CinemasID, x.CONTENT_ADMIN_ID });
                    table.ForeignKey(
                        name: "FK_provoles_cinemas_CinemasID",
                        column: x => x.CinemasID,
                        principalTable: "cinemas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_provoles_content_admins_CONTENT_ADMIN_ID",
                        column: x => x.CONTENT_ADMIN_ID,
                        principalTable: "content_admins",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_provoles_movies_MOVIES_ID_MOVIES_NAME",
                        columns: x => new { x.MOVIES_ID, x.MOVIES_NAME },
                        principalTable: "movies",
                        principalColumns: new[] { "movie_id", "movie_name" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "reservations",
                columns: table => new
                {
                    PROVOLES_MOVIES_ID = table.Column<int>(type: "int", nullable: false),
                    PROVOLES_MOVIES_NAME = table.Column<string>(type: "nchar(45)", maxLength: 45, nullable: false),
                    PROVOLES_CINEMAS_ID = table.Column<int>(type: "int", nullable: false),
                    PROVOLES_ID = table.Column<int>(type: "int", nullable: false),
                    CUSTOMERS_ID = table.Column<int>(type: "int", nullable: false),
                    ProvolesContentAdminId = table.Column<int>(type: "int", nullable: false),
                    NUMBER_OF_SEATS = table.Column<int>(type: "int", nullable: false),
                    Seats = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservations", x => new { x.PROVOLES_MOVIES_ID, x.PROVOLES_MOVIES_NAME, x.PROVOLES_CINEMAS_ID, x.PROVOLES_ID, x.CUSTOMERS_ID });
                    table.ForeignKey(
                        name: "FK_reservations_customers_CUSTOMERS_ID",
                        column: x => x.CUSTOMERS_ID,
                        principalTable: "customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reservations_provoles_PROVOLES_MOVIES_ID_PROVOLES_MOVIES_NAME_PROVOLES_ID_PROVOLES_CINEMAS_ID_ProvolesContentAdminId",
                        columns: x => new { x.PROVOLES_MOVIES_ID, x.PROVOLES_MOVIES_NAME, x.PROVOLES_ID, x.PROVOLES_CINEMAS_ID, x.ProvolesContentAdminId },
                        principalTable: "provoles",
                        principalColumns: new[] { "MOVIES_ID", "MOVIES_NAME", "ID", "CinemasID", "CONTENT_ADMIN_ID" });
                });

            migrationBuilder.InsertData(
                table: "cinemas",
                columns: new[] { "id", "3D", "name", "seats" },
                values: new object[,]
                {
                    { 0, "Yes", "Village Cinemas Thessaloniki", 300 },
                    { 1, "No", "Options Cinemas Glyfada", 200 },
                    { 2, "No", "Άστορ", 150 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_admins_UserName",
                table: "admins",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_content_admins_UserName",
                table: "content_admins",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_customers_UserName",
                table: "customers",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_movies_ContentAdminId",
                table: "movies",
                column: "ContentAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_provoles_CinemasID",
                table: "provoles",
                column: "CinemasID");

            migrationBuilder.CreateIndex(
                name: "IX_provoles_CONTENT_ADMIN_ID",
                table: "provoles",
                column: "CONTENT_ADMIN_ID");

            migrationBuilder.CreateIndex(
                name: "IX_reservations_CUSTOMERS_ID",
                table: "reservations",
                column: "CUSTOMERS_ID");

            migrationBuilder.CreateIndex(
                name: "IX_reservations_PROVOLES_MOVIES_ID_PROVOLES_MOVIES_NAME_PROVOLES_ID_PROVOLES_CINEMAS_ID_ProvolesContentAdminId",
                table: "reservations",
                columns: new[] { "PROVOLES_MOVIES_ID", "PROVOLES_MOVIES_NAME", "PROVOLES_ID", "PROVOLES_CINEMAS_ID", "ProvolesContentAdminId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admins");

            migrationBuilder.DropTable(
                name: "reservations");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "provoles");

            migrationBuilder.DropTable(
                name: "cinemas");

            migrationBuilder.DropTable(
                name: "movies");

            migrationBuilder.DropTable(
                name: "content_admins");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
