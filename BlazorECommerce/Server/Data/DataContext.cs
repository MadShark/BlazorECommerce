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
            modelBuilder.Entity<ProductVariant>()
                .HasKey(p => new { p.ProductId, p.ProductTypeId });


            #region Categories

            modelBuilder.Entity<Category>().HasData(
                    new Category()
                    {
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

            #endregion


            #region Products

            modelBuilder.Entity<Product>().HasData(
                    new Product()
                    {
                        Id = 1,
                        Title = "Lord of the rings",
                        Description = @"is an epic high-fantasy novel[a] by English author and scholar J. R. R. Tolkien. Set in Middle-earth, 
                                   intended to be Earth at some distant time in the past, the story began as a sequel to Tolkien's 1937 
                                   children's book The Hobbit",
                        ImageUrl = "https://upload.wikimedia.org/wikipedia/en/e/e9/First_Single_Volume_Edition_of_The_Lord_of_the_Rings.gif",
                        CategoryId = 1
                    },
                    new Product()
                    {
                        Id = 2,
                        Title = "Foundation",
                        Description = @" is a science fiction novel by American writer Isaac Asimov. It is the first published in his Foundation Trilogy
                                         (later expanded into the Foundation series). Foundation is a cycle of five interrelated short stories",
                        ImageUrl = "https://upload.wikimedia.org/wikipedia/en/2/25/Foundation_gnome.jpg",
                        CategoryId = 1
                    },
                    new Product()
                    {
                        Id = 3,
                        Title = "Fahrenheit 451",
                        Description = @"is a 1953 dystopian novel by American writer Ray Bradbury. Often regarded as one of his best works,
                                        the novel presents a future American society where books are outlawed and 'firemen' burn any that are found.",
                        ImageUrl = "https://upload.wikimedia.org/wikipedia/en/d/db/Fahrenheit_451_1st_ed_cover.jpg",
                        CategoryId = 1
                    },
                    new Product()
                    {
                        Id = 4,
                        Title = "The Matrix",
                        Description = @" It depicts a dystopian future in which humanity is unknowingly trapped inside a simulated reality, the Matrix,
                                         which intelligent machines have created to distract humans while using their bodies as an energy source.",
                        ImageUrl = "https://upload.wikimedia.org/wikipedia/en/c/c1/The_Matrix_Poster.jpg",
                        CategoryId = 2
                    },
                    new Product()
                    {
                        Id = 5,
                        Title = "Interstellar",
                        Description = @"set in a dystopian future where humanity is struggling to survive, the film follows a group of astronauts who
                                        travel through a wormhole near Saturn in search of a new home for mankind.",
                        ImageUrl = "https://upload.wikimedia.org/wikipedia/en/b/bc/Interstellar_film_poster.jpg",
                        CategoryId = 2
                    },
                    new Product()
                    {
                        Id = 6,
                        Title = "Her",
                        Description = @"the film follows Theodore Twombly (Joaquin Phoenix), a man who develops a relationship with Samantha 
                                        (Scarlett Johansson), an artificially intelligent virtual assistant personified through a female voice.",
                        ImageUrl = "https://upload.wikimedia.org/wikipedia/en/4/44/Her2013Poster.jpg",
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
                        CategoryId = 3
                    },
                    new Product()
                    {
                        Id = 8,
                        Title = "Cyberpunk 2077",
                        Description = @"is an action role-playing video game developed and published by CD Projekt. The story takes place in Night City,
                                        an open world set in the Cyberpunk universe.",
                        ImageUrl = "https://upload.wikimedia.org/wikipedia/en/thumb/9/9f/Cyberpunk_2077_box_art.jpg/220px-Cyberpunk_2077_box_art.jpg",
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
                        CategoryId = 3
                    }
                );

            #endregion


            #region ProductTypes

            modelBuilder.Entity<ProductType>().HasData(
                    new ProductType { Id = 1, Name = "Default" },
                    new ProductType { Id = 2, Name = "Paperback" },
                    new ProductType { Id = 3, Name = "E-Book" },
                    new ProductType { Id = 4, Name = "Audiobook" },
                    new ProductType { Id = 5, Name = "Stream" },
                    new ProductType { Id = 6, Name = "Blu-ray" },
                    new ProductType { Id = 7, Name = "VHS" },
                    new ProductType { Id = 8, Name = "PC" },
                    new ProductType { Id = 9, Name = "PlayStation" },
                    new ProductType { Id = 10, Name = "Xbox" }
                );

            #endregion


            #region ProductVariants

            modelBuilder.Entity<ProductVariant>().HasData(
                new ProductVariant
                {
                    ProductId = 1,
                    ProductTypeId = 2,
                    Price = 9.99m,
                    OriginalPrice = 19.99m
                },
                new ProductVariant
                {
                    ProductId = 1,
                    ProductTypeId = 3,
                    Price = 7.99m
                },
                new ProductVariant
                {
                    ProductId = 1,
                    ProductTypeId = 4,
                    Price = 19.99m,
                    OriginalPrice = 29.99m
                },
                new ProductVariant
                {
                    ProductId = 2,
                    ProductTypeId = 2,
                    Price = 7.99m,
                    OriginalPrice = 14.99m
                },
                new ProductVariant
                {
                    ProductId = 3,
                    ProductTypeId = 2,
                    Price = 6.99m
                },
                new ProductVariant
                {
                    ProductId = 4,
                    ProductTypeId = 5,
                    Price = 3.99m
                },
                new ProductVariant
                {
                    ProductId = 4,
                    ProductTypeId = 6,
                    Price = 9.99m
                },
                new ProductVariant
                {
                    ProductId = 4,
                    ProductTypeId = 7,
                    Price = 19.99m
                },
                new ProductVariant
                {
                    ProductId = 5,
                    ProductTypeId = 5,
                    Price = 3.99m,
                },
                new ProductVariant
                {
                    ProductId = 6,
                    ProductTypeId = 5,
                    Price = 2.99m
                },
                new ProductVariant
                {
                    ProductId = 7,
                    ProductTypeId = 8,
                    Price = 19.99m,
                    OriginalPrice = 29.99m
                },
                new ProductVariant
                {
                    ProductId = 7,
                    ProductTypeId = 9,
                    Price = 69.99m
                },
                new ProductVariant
                {
                    ProductId = 7,
                    ProductTypeId = 10,
                    Price = 49.99m,
                    OriginalPrice = 59.99m
                },
                new ProductVariant
                {
                    ProductId = 8,
                    ProductTypeId = 8,
                    Price = 9.99m,
                    OriginalPrice = 24.99m,
                },
                new ProductVariant
                {
                    ProductId = 9,
                    ProductTypeId = 8,
                    Price = 14.99m
                },
                new ProductVariant
                {
                    ProductId = 9,
                    ProductTypeId = 9,
                    Price = 159.99m,
                    OriginalPrice = 299m
                },
                new ProductVariant
                {
                    ProductId = 9,
                    ProductTypeId = 10,
                    Price = 79.99m,
                    OriginalPrice = 399m
                }
            );

            #endregion
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
    }
}
