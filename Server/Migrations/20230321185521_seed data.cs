using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class seeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Terms",
                columns: new[] { "Id", "Answer", "Explanation", "Question", "SetId" },
                values: new object[,]
                {
                    { 1, "answer 1", "explanation 1", "question 1", 1 },
                    { 2, "answer 2", "explanation 2", "question 2", 1 },
                    { 3, "answer 3", "explanation 1", "question 3", 1 },
                    { 4, "answer 4", "explanation 4", "question 4", 1 },
                    { 5, "answer 1", "explanation 1", "question 1", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Terms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Terms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Terms",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Terms",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Terms",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
