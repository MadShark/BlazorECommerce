﻿// <auto-generated />
using BlazorECommerce.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BlazorECommerce.Server.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220224030236_SeddingMoreProducts")]
    partial class SeddingMoreProducts
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BlazorECommerce.Shared.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Books",
                            Url = "books"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Movies",
                            Url = "movies"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Video Games",
                            Url = "video-games"
                        });
                });

            modelBuilder.Entity("BlazorECommerce.Shared.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            Description = "is an epic high-fantasy novel[a] by English author and scholar J. R. R. Tolkien. Set in Middle-earth, \r\n                                   intended to be Earth at some distant time in the past, the story began as a sequel to Tolkien's 1937 \r\n                                   children's book The Hobbit",
                            ImageUrl = "https://upload.wikimedia.org/wikipedia/en/e/e9/First_Single_Volume_Edition_of_The_Lord_of_the_Rings.gif",
                            Price = 19.95m,
                            Title = "Lord of the rings"
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 1,
                            Description = " is a science fiction novel by American writer Isaac Asimov. It is the first published in his Foundation Trilogy\r\n                                         (later expanded into the Foundation series). Foundation is a cycle of five interrelated short stories",
                            ImageUrl = "https://upload.wikimedia.org/wikipedia/en/2/25/Foundation_gnome.jpg",
                            Price = 22.50m,
                            Title = "Foundation"
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 1,
                            Description = "is a 1953 dystopian novel by American writer Ray Bradbury. Often regarded as one of his best works,\r\n                                        the novel presents a future American society where books are outlawed and 'firemen' burn any that are found.",
                            ImageUrl = "https://upload.wikimedia.org/wikipedia/en/d/db/Fahrenheit_451_1st_ed_cover.jpg",
                            Price = 49.15m,
                            Title = "Fahrenheit 451"
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 2,
                            Description = " It depicts a dystopian future in which humanity is unknowingly trapped inside a simulated reality, the Matrix,\r\n                                         which intelligent machines have created to distract humans while using their bodies as an energy source.",
                            ImageUrl = "https://upload.wikimedia.org/wikipedia/en/c/c1/The_Matrix_Poster.jpg",
                            Price = 59.20m,
                            Title = "The Matrix"
                        },
                        new
                        {
                            Id = 5,
                            CategoryId = 2,
                            Description = "set in a dystopian future where humanity is struggling to survive, the film follows a group of astronauts who\r\n                                        travel through a wormhole near Saturn in search of a new home for mankind.",
                            ImageUrl = "https://upload.wikimedia.org/wikipedia/en/b/bc/Interstellar_film_poster.jpg",
                            Price = 10.60m,
                            Title = "Interstellar"
                        },
                        new
                        {
                            Id = 6,
                            CategoryId = 2,
                            Description = "the film follows Theodore Twombly (Joaquin Phoenix), a man who develops a relationship with Samantha \r\n                                        (Scarlett Johansson), an artificially intelligent virtual assistant personified through a female voice.",
                            ImageUrl = "https://upload.wikimedia.org/wikipedia/en/4/44/Her2013Poster.jpg",
                            Price = 23.45m,
                            Title = "Her"
                        },
                        new
                        {
                            Id = 7,
                            CategoryId = 3,
                            Description = "the game is set 200 years after the events of Oblivion, and takes place in Skyrim, the northernmost province\r\n                                        of Tamriel. Its main story focuses on the player's character, the Dragonborn, on their quest to defeat Alduin\r\n                                        the World-Eater, a dragon who is prophesied to destroy the world. Over the course of the game, the player\r\n                                        completes quests and develops the characterby improving skills.",
                            ImageUrl = "https://upload.wikimedia.org/wikipedia/en/thumb/1/15/The_Elder_Scrolls_V_Skyrim_cover.png/220px-The_Elder_Scrolls_V_Skyrim_cover.png",
                            Price = 9.95m,
                            Title = "TES: Skyrim"
                        },
                        new
                        {
                            Id = 8,
                            CategoryId = 3,
                            Description = "is an action role-playing video game developed and published by CD Projekt. The story takes place in Night City,\r\n                                        an open world set in the Cyberpunk universe.",
                            ImageUrl = "https://upload.wikimedia.org/wikipedia/en/thumb/9/9f/Cyberpunk_2077_box_art.jpg/220px-Cyberpunk_2077_box_art.jpg",
                            Price = 99.10m,
                            Title = "Cyberpunk 2077"
                        },
                        new
                        {
                            Id = 9,
                            CategoryId = 3,
                            Description = "is an action-adventure game developed and published by Nintendo for the Nintendo 64. It was released in Japan\r\n                                        and North America in November 1998, and in PAL regions the following month. Ocarina of Time is the fifth game \r\n                                        in The Legend of Zelda series, and the first with 3D graphics.",
                            ImageUrl = "https://upload.wikimedia.org/wikipedia/en/thumb/5/57/The_Legend_of_Zelda_Ocarina_of_Time.jpg/220px-The_Legend_of_Zelda_Ocarina_of_Time.jpg",
                            Price = 5.55m,
                            Title = "TLOZ: Ocarina of Time"
                        });
                });

            modelBuilder.Entity("BlazorECommerce.Shared.Product", b =>
                {
                    b.HasOne("BlazorECommerce.Shared.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}