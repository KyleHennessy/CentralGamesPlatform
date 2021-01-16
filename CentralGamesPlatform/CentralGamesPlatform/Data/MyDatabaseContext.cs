using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CentralGamesPlatform.Models
{
    public class MyDatabaseContext : DbContext
    {
        public MyDatabaseContext (DbContextOptions<MyDatabaseContext> options) : 
            base(options)
        {
        }
        public DbSet<Game> Games { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 1, CategoryName = "Casino Game PC"});
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 2, CategoryName = "Casino Game Mobile" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 3, CategoryName = "Casino Game Both Device" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 4, CategoryName = "Browser Game PC" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 5, CategoryName = "Browser Game Mobile" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 6, CategoryName = "Browser Game Both Device" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 7, CategoryName = "Download Game PC" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 8, CategoryName = "Download Game Mobile" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 9, CategoryName = "Download Game Both Device" });

            modelBuilder.Entity<Game>().HasData(new Game
            {
                GameId = 1,
                Name = "Poker",
                Price = 5.00M,
                Description = "The card game poker where you can potentially win money",
                CategoryId = 1,
                ImageUrl ="\\images\\gameimages\\pokerLarge.jpg",
                ImageThumbnailUrl ="\\images\\gamethumbnails\\poker.jpg",
                IsOnSale=true
            });

            modelBuilder.Entity<Game>().HasData(new Game
            {
                GameId = 2,
                Name = "Spin the wheel",
                Price = 5.00M,
                Description = "Spin the wheel for the chance of winning awesome prizes",
                CategoryId = 2,
                ImageUrl = "\\images\\gameimages\\placeholder.jpg",
                ImageThumbnailUrl = "\\images\\gamethumbnails\\placeholder.gif",
                IsOnSale = true
            });

            modelBuilder.Entity<Game>().HasData(new Game
            {
                GameId = 3,
                Name = "Scratch card",
                Price = 5.00M,
                Description = "Scratch card where any prizes you reveal are yours to keep",
                CategoryId = 3,
                ImageUrl = "\\images\\gameimages\\placeholder.jpg",
                ImageThumbnailUrl = "\\images\\gamethumbnails\\placeholder.gif",
                IsOnSale = true
            });

            modelBuilder.Entity<Game>().HasData(new Game
            {
                GameId = 4,
                Name = "Happy Wheels",
                Price = 10.00M,
                Description = "Ride a bike through obstacles to get to the end",
                CategoryId = 4,
                ImageUrl = "\\images\\gameimages\\placeholder.jpg",
                ImageThumbnailUrl = "\\images\\gamethumbnails\\placeholder.gif",
                IsOnSale = true
            });

            modelBuilder.Entity<Game>().HasData(new Game
            {
                GameId = 5,
                Name = "Tetris",
                Price = 10.00M,
                Description = "Stack blocks neatly to increase your score",
                CategoryId = 5,
                ImageUrl = "\\images\\gameimages\\placeholder.jpg",
                ImageThumbnailUrl = "\\images\\gamethumbnails\\placeholder.gif",
                IsOnSale = true
            });

            modelBuilder.Entity<Game>().HasData(new Game
            {
                GameId = 6,
                Name = "8 Ball Pool",
                Price = 0.00M,
                Description = "Play Pool against your friends",
                CategoryId = 6,
                ImageUrl = "\\images\\gameimages\\placeholder.jpg",
                ImageThumbnailUrl = "\\images\\gamethumbnails\\placeholder.gif",
                IsOnSale = true
            });

            modelBuilder.Entity<Game>().HasData(new Game
            {
                GameId = 7,
                Name = "Call of duty",
                Price = 60.00M,
                Description = "Use weapons to defeat your enemies",
                CategoryId = 7,
                ImageUrl = "\\images\\gameimages\\placeholder.jpg",
                ImageThumbnailUrl = "\\images\\gamethumbnails\\placeholder.gif",
                IsOnSale = true
            });

            modelBuilder.Entity<Game>().HasData(new Game
            {
                GameId = 8,
                Name = "Fruit Ninja",
                Price = 5.00M,
                Description = "Slice the falling fruit to increase your score",
                CategoryId = 8,
                ImageUrl = "\\images\\gameimages\\placeholder.jpg",
                ImageThumbnailUrl = "\\images\\gamethumbnails\\placeholder.gif",
                IsOnSale = false
            });

            modelBuilder.Entity<Game>().HasData(new Game
            {
                GameId = 9,
                Name = "Among us",
                Price = 5.00M,
                Description = "Defeat all enemies as the imposter to win",
                CategoryId = 9,
                ImageUrl = "\\images\\gameimages\\placeholder.jpg",
                ImageThumbnailUrl = "\\images\\gamethumbnails\\placeholder.gif",
                IsOnSale = true
            });
        }
    }
}
