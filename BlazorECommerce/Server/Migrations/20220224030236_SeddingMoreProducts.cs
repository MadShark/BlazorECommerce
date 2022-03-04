using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorECommerce.Server.Migrations
{
    public partial class SeddingMoreProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Price", "Title" },
                values: new object[,]
                {
                    { 4, 2, " It depicts a dystopian future in which humanity is unknowingly trapped inside a simulated reality, the Matrix,\r\n                                         which intelligent machines have created to distract humans while using their bodies as an energy source.", "https://upload.wikimedia.org/wikipedia/en/c/c1/The_Matrix_Poster.jpg", 59.20m, "The Matrix" },
                    { 5, 2, "set in a dystopian future where humanity is struggling to survive, the film follows a group of astronauts who\r\n                                        travel through a wormhole near Saturn in search of a new home for mankind.", "https://upload.wikimedia.org/wikipedia/en/b/bc/Interstellar_film_poster.jpg", 10.60m, "Interstellar" },
                    { 6, 2, "the film follows Theodore Twombly (Joaquin Phoenix), a man who develops a relationship with Samantha \r\n                                        (Scarlett Johansson), an artificially intelligent virtual assistant personified through a female voice.", "https://upload.wikimedia.org/wikipedia/en/4/44/Her2013Poster.jpg", 23.45m, "Her" },
                    { 7, 3, "the game is set 200 years after the events of Oblivion, and takes place in Skyrim, the northernmost province\r\n                                        of Tamriel. Its main story focuses on the player's character, the Dragonborn, on their quest to defeat Alduin\r\n                                        the World-Eater, a dragon who is prophesied to destroy the world. Over the course of the game, the player\r\n                                        completes quests and develops the characterby improving skills.", "https://upload.wikimedia.org/wikipedia/en/thumb/1/15/The_Elder_Scrolls_V_Skyrim_cover.png/220px-The_Elder_Scrolls_V_Skyrim_cover.png", 9.95m, "TES: Skyrim" },
                    { 8, 3, "is an action role-playing video game developed and published by CD Projekt. The story takes place in Night City,\r\n                                        an open world set in the Cyberpunk universe.", "https://upload.wikimedia.org/wikipedia/en/thumb/9/9f/Cyberpunk_2077_box_art.jpg/220px-Cyberpunk_2077_box_art.jpg", 99.10m, "Cyberpunk 2077" },
                    { 9, 3, "is an action-adventure game developed and published by Nintendo for the Nintendo 64. It was released in Japan\r\n                                        and North America in November 1998, and in PAL regions the following month. Ocarina of Time is the fifth game \r\n                                        in The Legend of Zelda series, and the first with 3D graphics.", "https://upload.wikimedia.org/wikipedia/en/thumb/5/57/The_Legend_of_Zelda_Ocarina_of_Time.jpg/220px-The_Legend_of_Zelda_Ocarina_of_Time.jpg", 5.55m, "TLOZ: Ocarina of Time" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);
        }
    }
}
