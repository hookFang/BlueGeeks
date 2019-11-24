using BlueGeeks.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using BlueGeeks.Models;

namespace BlueGeeksTest
{
    public class TeamTest
    {
        [Fact]
        public async void TestAddTeam()
        {
            //Arrange           
            var db = MockDb.CreateMockDb();
            var c = new TeamsController(db);


            var teams = new Teams { Team_Name = "Everett Otters", Team_Mascot = "Otter", Team_Id = 2, Conference = "Eastern", Wins = 0, Loses = 0, Ties = 0, Win_Streak = 0 };
            //Act
            var r = await c.Create(teams);
            //Assert
            var result = Assert.IsType<RedirectToActionResult>(r);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal(1, db.Teams.Where(x => x.Team_Name == teams.Team_Name && x.Team_Mascot == teams.Team_Mascot && x.Team_Id == teams.Team_Id && x.Conference == teams.Conference && x.Wins == teams.Wins && x.Loses == teams.Loses && x.Ties == teams.Ties && x.Win_Streak == teams.Win_Streak).Count());
        }

        [Fact]
        public async void TestAddInvalidTeam()
        {
            //Arrange
            var db = MockDb.CreateMockDb();
            var c = new TeamsController(db);

            var teams = new Teams {Team_Mascot = "Otter", Team_Id = 2, Conference = "Eastern", Wins = 0, Loses = 0, Ties = 0, Win_Streak = 0 };
            c.ModelState.AddModelError("Team_Name", "Required");

            //Act

            var r = await c.Create(teams);
            //Assert
            var result = Assert.IsType<ViewResult>(r);
            var model = Assert.IsAssignableFrom<Teams>(result.ViewData.Model);
            Assert.Equal(teams, model);
        }

        [Fact]
        public async void IndexTest()
        {
            //Arrange
            var dbContext = MockDb.CreateMockDb();
            TeamsController sc = new TeamsController(dbContext);
            //Act
            var r = await sc.Index();
            //Assert
            var result = Assert.IsType<ViewResult>(r);
            var model = Assert.IsAssignableFrom<List<Teams>>(result.ViewData.Model);
            Assert.Equal(1, model.Count());
        }
    }
}
