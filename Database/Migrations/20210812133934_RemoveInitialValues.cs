using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PracticumTask.Migrations
{
    public partial class RemoveInitialValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Genres_Name",
                table: "Genres");

            migrationBuilder.DropIndex(
                name: "IX_Books_Title",
                table: "Books");

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Birthdate", "FirstName", "LastName", "Patronymic" },
                values: new object[,]
                {
                    { 1, new DateTime(1954, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Иван", "Иванов", "Иванович" },
                    { 2, new DateTime(1968, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Пётр", "Петров", null },
                    { 3, new DateTime(1943, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Тумба Юмба", null, null }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Novel" },
                    { 2, "Fiction" },
                    { 3, "Adventure" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "GenreId", "Title" },
                values: new object[,]
                {
                    { 2, 2, 1, "Плачут ли программисты" },
                    { 1, 3, 3, "На волнах галоперидола" },
                    { 3, 1, 3, "Подебажим?" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Genres_Name",
                table: "Genres",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_Title",
                table: "Books",
                column: "Title",
                unique: true);
        }
    }
}
