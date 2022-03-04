using Microsoft.EntityFrameworkCore;

namespace BlazorECommerce.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                    new Category() { 
                        Id = 1,
                        Name = "Books",
                        Url = "books"
                    },
                    new Category()
                    {
                        Id = 2,
                        Name = "Movies",
                        Url = "movies"
                    },
                    new Category()
                    {
                        Id = 3,
                        Name = "Video Games",
                        Url = "video-games"
                    }
                );

            modelBuilder.Entity<Product>().HasData(
                    new Product()
                    {
                        Id = 1,
                        Title = "Lord of the rings",
                        Description = @"is an epic high-fantasy novel[a] by English author and scholar J. R. R. Tolkien. Set in Middle-earth, 
                                   intended to be Earth at some distant time in the past, the story began as a sequel to Tolkien's 1937 
                                   children's book The Hobbit",
                        ImageUrl = "https://upload.wikimedia.org/wikipedia/en/e/e9/First_Single_Volume_Edition_of_The_Lord_of_the_Rings.gif",
                        Price = 19.95m,
                        CategoryId = 1
                    },
                    new Product()
                    {
                        Id = 2,
                        Title = "Foundation",
                        Description = @" is a science fiction novel by American writer Isaac Asimov. It is the first published in his Foundation Trilogy
                                         (later expanded into the Foundation series). Foundation is a cycle of five interrelated short stories",
                        ImageUrl = "https://upload.wikimedia.org/wikipedia/en/2/25/Foundation_gnome.jpg",
                        Price = 22.50m,
                        CategoryId = 1
                    },
                    new Product()
                    {
                        Id = 3,
                        Title = "Fahrenheit 451",
                        Description = @"is a 1953 dystopian novel by American writer Ray Bradbury. Often regarded as one of his best works,
                                        the novel presents a future American society where books are outlawed and 'firemen' burn any that are found.",
                        ImageUrl = "https://upload.wikimedia.org/wikipedia/en/d/db/Fahrenheit_451_1st_ed_cover.jpg",
                        Price = 49.15m,
                        CategoryId = 1
                    },
                    new Product()
                    {
                        Id = 4,
                        Title = "The Matrix",
                        Description = @" It depicts a dystopian future in which humanity is unknowingly trapped inside a simulated reality, the Matrix,
                                         which intelligent machines have created to distract humans while using their bodies as an energy source.",
                        ImageUrl = "https://upload.wikimedia.org/wikipedia/en/c/c1/The_Matrix_Poster.jpg",
                        Price = 59.20m,
                        CategoryId = 2
                    },
                    new Product()
                    {
                        Id = 5,
                        Title = "Interstellar",
                        Description = @"set in a dystopian future where humanity is struggling to survive, the film follows a group of astronauts who
                                        travel through a wormhole near Saturn in search of a new home for mankind.",
                        ImageUrl = "https://upload.wikimedia.org/wikipedia/en/b/bc/Interstellar_film_poster.jpg",
                        Price = 10.60m,
                        CategoryId = 2
                    },
                    new Product()
                    {
                        Id = 6,
                        Title = "Her",
                        Description = @"the film follows Theodore Twombly (Joaquin Phoenix), a man who develops a relationship with Samantha 
                                        (Scarlett Johansson), an artificially intelligent virtual assistant personified through a female voice.",
                        ImageUrl = "https://upload.wikimedia.org/wikipedia/en/4/44/Her2013Poster.jpg",
                        Price = 23.45m,
                        CategoryId = 2
                    },
                    new Product()
                    {
                        Id = 7,
                        Title = "TES: Skyrim",
                        Description = @"the game is set 200 years after the events of Oblivion, and takes place in Skyrim, the northernmost province
                                        of Tamriel. Its main story focuses on the player's character, the Dragonborn, on their quest to defeat Alduin
                                        the World-Eater, a dragon who is prophesied to destroy the world. Over the course of the game, the player
                                        completes quests and develops the characterby improving skills.",
                        ImageUrl = "https://upload.wikimedia.org/wikipedia/en/thumb/1/15/The_Elder_Scrolls_V_Skyrim_cover.png/220px-The_Elder_Scrolls_V_Skyrim_cover.png",
                        Price = 9.95m,
                        CategoryId = 3
                    },
                    new Product()
                    {
                        Id = 8,
                        Title = "Cyberpunk 2077",
                        Description = @"is an action role-playing video game developed and published by CD Projekt. The story takes place in Night City,
                                        an open world set in the Cyberpunk universe.",
                        ImageUrl = "https://upload.wikimedia.org/wikipedia/en/thumb/9/9f/Cyberpunk_2077_box_art.jpg/220px-Cyberpunk_2077_box_art.jpg",
                        Price = 99.10m,
                        CategoryId = 3
                    },
                    new Product()
                    {
                        Id = 9,
                        Title = "TLOZ: Ocarina of Time",
                        Description = @"is an action-adventure game developed and published by Nintendo for the Nintendo 64. It was released in Japan
                                        and North America in November 1998, and in PAL regions the following month. Ocarina of Time is the fifth game 
                                        in The Legend of Zelda series, and the first with 3D graphics.",
                        ImageUrl = "https://upload.wikimedia.org/wikipedia/en/thumb/5/57/The_Legend_of_Zelda_Ocarina_of_Time.jpg/220px-The_Legend_of_Zelda_Ocarina_of_Time.jpg",
                        Price = 5.55m,
                        CategoryId = 3
                    }
                );
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
