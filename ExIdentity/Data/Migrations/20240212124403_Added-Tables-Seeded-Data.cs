using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExIdentity.Data.Migrations
{
    public partial class AddedTablesSeededData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Board identification")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "Board name")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.Id);
                },
                comment: "TaskReal board");

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "TaskReal identification")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false, comment: "TaskReal title"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, comment: "TaskReal Description"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "TaskReal creation time"),
                    BoardId = table.Column<int>(type: "int", nullable: false, comment: "Board identification"),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Owner identification")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "TaskReal Table");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "c33ad2ef-0edc-486f-9559-0ff6c2ceb622", 0, "c677e38f-d616-4c1d-b846-e9a1f8935da4", null, false, false, null, null, "test@gmail.com", "AQAAAAEAACcQAAAAEItEVLLqpoLH5Fj13i1Cdb1F0595ayiiW+Qardj2A5A5brij49AI9WT2JmT+4QGcCA==", null, false, "9530ea42-5ef9-4800-a5e6-5e5d19683d14", false, "Test@gmail.com" });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Open" },
                    { 2, "In progress" },
                    { 3, "Done" }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "BoardId", "CreatedOn", "Description", "OwnerId", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 7, 27, 14, 44, 3, 17, DateTimeKind.Local).AddTicks(8046), "im noting", "c33ad2ef-0edc-486f-9559-0ff6c2ceb622", "OK TEST" },
                    { 2, 1, new DateTime(2024, 1, 23, 14, 44, 3, 17, DateTimeKind.Local).AddTicks(8082), "im noting", "c33ad2ef-0edc-486f-9559-0ff6c2ceb622", "OK TEST" },
                    { 3, 2, new DateTime(2024, 8, 30, 14, 44, 3, 17, DateTimeKind.Local).AddTicks(8085), "im noting", "c33ad2ef-0edc-486f-9559-0ff6c2ceb622", "OK TEST" },
                    { 4, 3, new DateTime(2022, 9, 30, 14, 44, 3, 17, DateTimeKind.Local).AddTicks(8087), "im noting", "c33ad2ef-0edc-486f-9559-0ff6c2ceb622", "OK TEST" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_BoardId",
                table: "Tasks",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_OwnerId",
                table: "Tasks",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Boards");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c33ad2ef-0edc-486f-9559-0ff6c2ceb622");
        }
    }
}
