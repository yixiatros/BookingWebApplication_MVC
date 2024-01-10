using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingWebApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddBasicModelsTodb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "admins",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nchar(45)", fixedLength: true, maxLength: 45, nullable: false),
                    UserName = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admins", x => x.id);
                    table.UniqueConstraint("AK_admins_UserName", x => x.UserName);
                });

            migrationBuilder.CreateTable(
                name: "cinemas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nchar(45)", fixedLength: true, maxLength: 45, nullable: false),
                    seats = table.Column<string>(type: "nchar(45)", fixedLength: true, maxLength: 45, nullable: false),
                    _3D = table.Column<string>(name: "3D", type: "nchar(45)", fixedLength: true, maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cinemas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "content_admins",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nchar(45)", fixedLength: true, maxLength: 45, nullable: false),
                    UserName = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_content_admins", x => x.id);
                    table.UniqueConstraint("AK_content_admins_UserName", x => x.UserName);
                });

            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nchar(45)", fixedLength: true, maxLength: 45, nullable: false),
                    UserName = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.id);
                    table.UniqueConstraint("AK_customers_UserName", x => x.UserName);
                });

            migrationBuilder.CreateTable(
                name: "movies",
                columns: table => new
                {
                    movie_id = table.Column<int>(type: "int", nullable: false),
                    movie_name = table.Column<string>(type: "nchar(45)", fixedLength: true, maxLength: 45, nullable: false),
                    movie_description = table.Column<string>(type: "nchar(45)", fixedLength: true, maxLength: 45, nullable: false),
                    movie_length = table.Column<int>(type: "int", nullable: false),
                    movie_type = table.Column<string>(type: "nchar(45)", fixedLength: true, maxLength: 45, nullable: false),
                    movie_summary = table.Column<string>(type: "nchar(45)", fixedLength: true, maxLength: 45, nullable: false),
                    movie_director = table.Column<string>(type: "nchar(45)", fixedLength: true, maxLength: 45, nullable: false),
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
                name: "users",
                columns: table => new
                {
                    user_name = table.Column<string>(type: "nchar(32)", fixedLength: true, maxLength: 32, nullable: false),
                    email = table.Column<string>(type: "nchar(35)", fixedLength: true, maxLength: 35, nullable: false),
                    password = table.Column<string>(type: "nchar(45)", fixedLength: true, maxLength: 45, nullable: false),
                    create_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    salt = table.Column<string>(type: "nchar(45)", fixedLength: true, maxLength: 45, nullable: false),
                    role = table.Column<string>(type: "nchar(45)", fixedLength: true, maxLength: 45, nullable: false),
                    AdminUserName = table.Column<int>(type: "int", nullable: true),
                    ContentAdminUserName = table.Column<int>(type: "int", nullable: true),
                    CustomerUserName = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.user_name);
                    table.ForeignKey(
                        name: "FK_users_admins_AdminUserName",
                        column: x => x.AdminUserName,
                        principalTable: "admins",
                        principalColumn: "UserName");
                    table.ForeignKey(
                        name: "FK_users_content_admins_ContentAdminUserName",
                        column: x => x.ContentAdminUserName,
                        principalTable: "content_admins",
                        principalColumn: "UserName");
                    table.ForeignKey(
                        name: "FK_users_customers_CustomerUserName",
                        column: x => x.CustomerUserName,
                        principalTable: "customers",
                        principalColumn: "UserName");
                });

            migrationBuilder.CreateTable(
                name: "provoles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    MoviesId = table.Column<int>(type: "int", nullable: false),
                    MoviesName = table.Column<string>(type: "nchar(45)", maxLength: 45, nullable: false),
                    CinemasID = table.Column<int>(type: "int", nullable: true),
                    ContentAdminId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_provoles", x => x.id);
                    table.ForeignKey(
                        name: "FK_provoles_cinemas_CinemasID",
                        column: x => x.CinemasID,
                        principalTable: "cinemas",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_provoles_content_admins_ContentAdminId",
                        column: x => x.ContentAdminId,
                        principalTable: "content_admins",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_provoles_movies_MoviesId_MoviesName",
                        columns: x => new { x.MoviesId, x.MoviesName },
                        principalTable: "movies",
                        principalColumns: new[] { "movie_id", "movie_name" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_movies_ContentAdminId",
                table: "movies",
                column: "ContentAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_provoles_CinemasID",
                table: "provoles",
                column: "CinemasID");

            migrationBuilder.CreateIndex(
                name: "IX_provoles_ContentAdminId",
                table: "provoles",
                column: "ContentAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_provoles_MoviesId_MoviesName",
                table: "provoles",
                columns: new[] { "MoviesId", "MoviesName" });

            migrationBuilder.CreateIndex(
                name: "IX_users_AdminUserName",
                table: "users",
                column: "AdminUserName",
                unique: true,
                filter: "[AdminUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_users_ContentAdminUserName",
                table: "users",
                column: "ContentAdminUserName",
                unique: true,
                filter: "[ContentAdminUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_users_CustomerUserName",
                table: "users",
                column: "CustomerUserName",
                unique: true,
                filter: "[CustomerUserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "provoles");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "cinemas");

            migrationBuilder.DropTable(
                name: "movies");

            migrationBuilder.DropTable(
                name: "admins");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "content_admins");
        }
    }
}
