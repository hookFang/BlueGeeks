using BlueGeeks.Data;
using BlueGeeks.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace BlueGeeksTest
{
    public static class EntityExtensions
    {
        public static void Clear<T>(this DbSet<T> dbSet) where T : class
        {
            dbSet.RemoveRange(dbSet);
        }
    }
    public class MockDb
    {
        public static ApplicationDbContext CreateMockDb()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().
                UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Player.Add(new Player { FirstName = "Mike", LastName = "Martins", Player_Id = 1, JerseyNumber = 23, Position = "PG", TeamId = 1 });
                context.Teams.Add(new Teams { Team_Name = "Everett Otters", Team_Mascot = "Otter", Team_Id = 1, Conference = "Eastern", Wins = 0, Loses = 0, Ties = 0, Win_Streak = 0 });
                context.Stadium.Add(new Stadium { Stadium_Id = 1, StadiumName = "Ever After", City = "Everett", Team_Id = 1 });
                context.Coaches.Add(new Coaches { Coaches_Id = 1, FirstName = "Scott", LastName = "Pilgrim", Title = "Head Coach", Team_Id = 1 });
                context.Matches.Add(new Matches { Matche_Id = 1, HomeTeam_Id = 1, AwayTeam_Id = 1, Stadium_Id = 1, MatchDate = DateTime.Now });
                context.PlayerStatistics.Add(new PlayerStatistics { Player_Statistics_Id = 1, Player_Id = 1, Assists = 0, Blocks = 0, Steals = 0, Rebounds = 0, ThreePointersMade = 0, PointsMade = 0, TurnOvers = 0, FgPercent = 0, FtPercent = 0 });
                context.SaveChanges();
            }
            return new ApplicationDbContext(options);
        }
    }
}