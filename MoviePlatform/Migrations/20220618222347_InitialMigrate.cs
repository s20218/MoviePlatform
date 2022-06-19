using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviePlatform.Migrations
{
    public partial class InitialMigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    IdPerson = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Types = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasDegree = table.Column<bool>(type: "bit", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.IdPerson);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProfileDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.IdUser);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    IdMovie = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdWriter = table.Column<int>(type: "int", nullable: false),
                    IdDirector = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.IdMovie);
                    table.ForeignKey(
                        name: "FK_Movies_People_IdDirector",
                        column: x => x.IdDirector,
                        principalTable: "People",
                        principalColumn: "IdPerson",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movies_People_IdWriter",
                        column: x => x.IdWriter,
                        principalTable: "People",
                        principalColumn: "IdPerson",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    IdReport = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdReportedUser = table.Column<int>(type: "int", nullable: false),
                    ReportDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Action = table.Column<int>(type: "int", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: true),
                    IdModerator = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.IdReport);
                    table.ForeignKey(
                        name: "FK_Reports_Users_IdModerator",
                        column: x => x.IdModerator,
                        principalTable: "Users",
                        principalColumn: "IdUser");
                    table.ForeignKey(
                        name: "FK_Reports_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "IdUser");
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    IdMovie = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => new { x.IdMovie, x.IdUser });
                    table.ForeignKey(
                        name: "FK_Favorites_Movies_IdMovie",
                        column: x => x.IdMovie,
                        principalTable: "Movies",
                        principalColumn: "IdMovie",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favorites_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    IdReview = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Grade = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    IdMovie = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.IdReview);
                    table.ForeignKey(
                        name: "FK_Reviews_Movies_IdMovie",
                        column: x => x.IdMovie,
                        principalTable: "Movies",
                        principalColumn: "IdMovie",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StarsIns",
                columns: table => new
                {
                    IdMovie = table.Column<int>(type: "int", nullable: false),
                    IdPerson = table.Column<int>(type: "int", nullable: false),
                    Salary = table.Column<double>(type: "float", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StarsIns", x => new { x.IdMovie, x.IdPerson });
                    table.ForeignKey(
                        name: "FK_StarsIns_Movies_IdMovie",
                        column: x => x.IdMovie,
                        principalTable: "Movies",
                        principalColumn: "IdMovie",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StarsIns_People_IdPerson",
                        column: x => x.IdPerson,
                        principalTable: "People",
                        principalColumn: "IdPerson",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "IdPerson", "DateOfBirth", "FirstName", "FullName", "HasDegree", "LastName", "Types" },
                values: new object[,]
                {
                    { 1, new DateTime(1990, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tom", "Tom Smith", null, "Smith", "Writer,Actor" },
                    { 2, null, "John", "John Thompson", true, "Thompson", "Director" },
                    { 3, new DateTime(1997, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Anne", "Anne Reddle", false, "Reddle", "Writer,Actor,Director" },
                    { 4, new DateTime(1975, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brue", "Brue Kaminski", null, "Kaminski", "Actor" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "IdUser", "Email", "Password", "ProfileDescription", "UserName", "UserType" },
                values: new object[,]
                {
                    { 1, "jkowal@gmail.com", "12345", "Standard user profile", "jkowal", 0 },
                    { 2, "ann@gmail.com", "12345", "Standard user profile", "ann", 0 },
                    { 3, "stom@gmail.com", "12345", "Standard user profile", "stom", 0 },
                    { 4, "joe2199@gmail.com", "12345", "Standard user profile", "joe2199", 0 },
                    { 5, "kkarpin@gmail.com", "12345", "Standard user profile", "kkarpin", 0 },
                    { 6, "jdoe@gmail.com", "12345", null, "jdoe", 1 }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "IdMovie", "Description", "Genres", "IdDirector", "IdWriter", "ImageUrl", "Title" },
                values: new object[] { 1, "The movie is about the 1912 sinking of the RMS Titanic.", "Drama", 3, 3, "C:\\Images\\drStrange.jpg", "Titanic" });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "IdMovie", "Description", "Genres", "IdDirector", "IdWriter", "ImageUrl", "Title" },
                values: new object[] { 2, "Doctor Strange is a 2016 American superhero film based on the Marvel Comics character of the same name.", "Action,Fantasy", 2, 1, "C:\\Images\\Titanic.jpg", "Doctor Strange" });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "IdMovie", "Description", "Genres", "IdDirector", "IdWriter", "ImageUrl", "Title" },
                values: new object[] { 3, " The original film told the true story of the Akita dog named Hachiko who lived in Japan in the 1920s. ", "Drama", 3, 3, "C:\\Images\\Hachi.jpg", "Hachi" });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "IdReview", "CreationDate", "Grade", "IdMovie", "IdUser", "Text" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 6, 18, 22, 23, 46, 722, DateTimeKind.Utc).AddTicks(3504), 5, 2, 1, "Very interesting movie." },
                    { 2, new DateTime(2022, 6, 18, 22, 23, 46, 722, DateTimeKind.Utc).AddTicks(3506), 3, 2, 2, "A bit boring." },
                    { 3, new DateTime(2022, 6, 18, 22, 23, 46, 722, DateTimeKind.Utc).AddTicks(3507), 5, 2, 3, "Impressive." },
                    { 4, new DateTime(2022, 6, 18, 22, 23, 46, 722, DateTimeKind.Utc).AddTicks(3508), 4, 2, 4, "Nice graphics." }
                });

            migrationBuilder.InsertData(
                table: "StarsIns",
                columns: new[] { "IdMovie", "IdPerson", "Duration", "EndDate", "Salary", "StartDate" },
                values: new object[,]
                {
                    { 2, 1, null, new DateTime(2022, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2500.0, new DateTime(2021, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 3, null, new DateTime(2022, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2500.0, new DateTime(2021, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 4, null, new DateTime(2022, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2500.0, new DateTime(2021, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_IdUser",
                table: "Favorites",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_IdDirector",
                table: "Movies",
                column: "IdDirector");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_IdWriter",
                table: "Movies",
                column: "IdWriter");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_IdModerator",
                table: "Reports",
                column: "IdModerator");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_IdUser",
                table: "Reports",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_IdMovie",
                table: "Reviews",
                column: "IdMovie");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_IdUser",
                table: "Reviews",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_StarsIns_IdPerson",
                table: "StarsIns",
                column: "IdPerson");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "StarsIns");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
