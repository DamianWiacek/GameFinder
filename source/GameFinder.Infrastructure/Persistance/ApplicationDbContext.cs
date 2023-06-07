using GameFinder.Application.Data;
using GameFinder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFinder.Infrastructure.Persistance
{
    public class ApplicationDbContext : DbContext, IDbContext
    {
        public DbSet<Address> Address { get; set; }
        public DbSet<Court> Court { get; set; }
        public DbSet<Game> Game { get; set; }
        public DbSet<GameDetails> Game_Details { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Sport> Sport { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<EMail> EMail { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("server=DESKTOP-2UA5DVQ;database=GameFinderDb;trusted_connection=true;Encrypt=False;Trust Server Certificate=true");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Role>()
                .HasData(
                new Role(1, "Player"),    
                new Role(2, "Administrator"));

            modelBuilder.Entity<Sport>()
                .HasData(
                new Sport(1, "Soccer"),
                new Sport(2, "Basketball"),
                new Sport(3, "Volleyball"),
                new Sport(4, "Tennis"));

           
                

            /*
            modelBuilder.Entity<EMail>().ToSqlQuery(
                @"CREATE TRIGGER trg_User_Insert
                  ON User
                  AFTER INSERT
                  AS
                  BEGIN
                      SET NOCOUNT ON;
                  
                      INSERT INTO Email (Email, Subject, Body)
                      SELECT i.Email, 'WELCOME TO GameFINDER', 'Greetings from GameFINDER Team'
                      FROM inserted i;
                  END;");
            */
        }
    }
}
