using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskTracker.DAL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 32, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 32, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sprints",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 64, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sprints", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(nullable: false),
                    Username = table.Column<string>(maxLength: 128, nullable: true),
                    Password = table.Column<string>(maxLength: 128, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SprintId = table.Column<int>(nullable: false),
                    ClassificationId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 64, nullable: true),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    ExpectedHours = table.Column<int>(nullable: false),
                    ActualHours = table.Column<int>(nullable: false),
                    IsCompleted = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Classifications_ClassificationId",
                        column: x => x.ClassificationId,
                        principalTable: "Classifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Sprints_SprintId",
                        column: x => x.SprintId,
                        principalTable: "Sprints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Classifications",
                columns: new[] { "Id", "CreatedBy", "DateCreated", "DateModified", "IsActive", "ModifiedBy", "Name" },
                values: new object[,]
                {
                    { 1, 0, new DateTime(2020, 11, 6, 3, 11, 28, 372, DateTimeKind.Local).AddTicks(8138), new DateTime(2020, 11, 6, 3, 11, 28, 372, DateTimeKind.Local).AddTicks(8138), true, 0, "Backlog" },
                    { 2, 0, new DateTime(2020, 11, 6, 3, 11, 28, 372, DateTimeKind.Local).AddTicks(8138), new DateTime(2020, 11, 6, 3, 11, 28, 372, DateTimeKind.Local).AddTicks(8138), true, 0, "Active" },
                    { 3, 0, new DateTime(2020, 11, 6, 3, 11, 28, 372, DateTimeKind.Local).AddTicks(8138), new DateTime(2020, 11, 6, 3, 11, 28, 372, DateTimeKind.Local).AddTicks(8138), true, 0, "Closed" },
                    { 4, 0, new DateTime(2020, 11, 6, 3, 11, 28, 372, DateTimeKind.Local).AddTicks(8138), new DateTime(2020, 11, 6, 3, 11, 28, 372, DateTimeKind.Local).AddTicks(8138), true, 0, "Archived" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedBy", "DateCreated", "DateModified", "IsActive", "ModifiedBy", "Name" },
                values: new object[,]
                {
                    { 1, 0, new DateTime(2020, 11, 6, 3, 11, 28, 372, DateTimeKind.Local).AddTicks(8138), new DateTime(2020, 11, 6, 3, 11, 28, 372, DateTimeKind.Local).AddTicks(8138), true, 0, "Admin" },
                    { 2, 0, new DateTime(2020, 11, 6, 3, 11, 28, 372, DateTimeKind.Local).AddTicks(8138), new DateTime(2020, 11, 6, 3, 11, 28, 372, DateTimeKind.Local).AddTicks(8138), true, 0, "User" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "DateCreated", "DateModified", "IsActive", "ModifiedBy", "Password", "RoleId", "Username" },
                values: new object[] { 1, 0, new DateTime(2020, 11, 6, 3, 11, 28, 372, DateTimeKind.Local).AddTicks(8138), new DateTime(2020, 11, 6, 3, 11, 28, 372, DateTimeKind.Local).AddTicks(8138), true, 0, "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918", 1, "admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "DateCreated", "DateModified", "IsActive", "ModifiedBy", "Password", "RoleId", "Username" },
                values: new object[] { 2, 0, new DateTime(2020, 11, 6, 3, 11, 28, 372, DateTimeKind.Local).AddTicks(8138), new DateTime(2020, 11, 6, 3, 11, 28, 372, DateTimeKind.Local).AddTicks(8138), true, 0, "04f8996da763b7a969b1028ee3007569eaf3a635486ddab211d512c85b9df8fb", 2, "user" });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ClassificationId",
                table: "Tasks",
                column: "ClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_SprintId",
                table: "Tasks",
                column: "SprintId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Classifications");

            migrationBuilder.DropTable(
                name: "Sprints");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
