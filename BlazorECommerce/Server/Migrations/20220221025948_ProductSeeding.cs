using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorECommerce.Server.Migrations
{
    public partial class ProductSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ImageUrl", "Price", "Title" },
                values: new object[] { 1, "is an epic high-fantasy novel[a] by English author and scholar J. R. R. Tolkien. Set in Middle-earth, \r\n                                   intended to be Earth at some distant time in the past, the story began as a sequel to Tolkien's 1937 \r\n                                   children's book The Hobbit", "https://upload.wikimedia.org/wikipedia/en/e/e9/First_Single_Volume_Edition_of_The_Lord_of_the_Rings.gif", 19.95m, "Lord of the rings" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ImageUrl", "Price", "Title" },
                values: new object[] { 2, " is a science fiction novel by American writer Isaac Asimov. It is the first published in his Foundation Trilogy\r\n                                         (later expanded into the Foundation series). Foundation is a cycle of five interrelated short stories", "https://upload.wikimedia.org/wikipedia/en/2/25/Foundation_gnome.jpg", 22.50m, "Foundation" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ImageUrl", "Price", "Title" },
                values: new object[] { 3, "is a 1953 dystopian novel by American writer Ray Bradbury. Often regarded as one of his best works,\r\n                                        the novel presents a future American society where books are outlawed and 'firemen' burn any that are found.", "https://upload.wikimedia.org/wikipedia/en/d/db/Fahrenheit_451_1st_ed_cover.jpg", 49.15m, "Fahrenheit 451" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
