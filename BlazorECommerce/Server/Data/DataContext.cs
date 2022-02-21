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
            modelBuilder.Entity<Product>().HasData(
                    new Product()
                    {
                        Id = 1,
                        Title = "Lord of the rings",
                        Description = @"is an epic high-fantasy novel[a] by English author and scholar J. R. R. Tolkien. Set in Middle-earth, 
                                   intended to be Earth at some distant time in the past, the story began as a sequel to Tolkien's 1937 
                                   children's book The Hobbit",
                        ImageUrl = "https://upload.wikimedia.org/wikipedia/en/e/e9/First_Single_Volume_Edition_of_The_Lord_of_the_Rings.gif",
                        Price = 19.95m
                    },
                    new Product()
                    {
                        Id = 2,
                        Title = "Foundation",
                        Description = @" is a science fiction novel by American writer Isaac Asimov. It is the first published in his Foundation Trilogy
                                         (later expanded into the Foundation series). Foundation is a cycle of five interrelated short stories",
                        ImageUrl = "https://upload.wikimedia.org/wikipedia/en/2/25/Foundation_gnome.jpg",
                        Price = 22.50m
                    },
                    new Product()
                    {
                        Id = 3,
                        Title = "Fahrenheit 451",
                        Description = @"is a 1953 dystopian novel by American writer Ray Bradbury. Often regarded as one of his best works,
                                        the novel presents a future American society where books are outlawed and 'firemen' burn any that are found.",
                        ImageUrl = "https://upload.wikimedia.org/wikipedia/en/d/db/Fahrenheit_451_1st_ed_cover.jpg",
                        Price = 49.15m
                    }
                );
        }

        public DbSet<Product> Products { get; set; }
    }
}
