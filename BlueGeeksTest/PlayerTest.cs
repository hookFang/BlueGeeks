using BlueGeeks.Controllers;
using BlueGeeks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BlueGeeksTest
{
    public class PlayerTest
    {
        [Fact]
        public async void TestAddPlayer()
        {
            //Arrange           
            var db = MockDb.CreateMockDb();
            var c = new PlayersController(db);


            var player = new Player { Player_Id = 2, FirstName = "Mike", LastName = "Mar", JerseyNumber = 23, Position = "SG", TeamId = 2 };
            //Act
            var r = await c.Create(player);
            //Assert
            var result = Assert.IsType<RedirectToActionResult>(r);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal(1, db.Player.Where(x => x.Player_Id == player.Player_Id && x.FirstName == player.FirstName && x.LastName == player.LastName && x.JerseyNumber == player.JerseyNumber && x.Position == player.Position && x.TeamId == player.TeamId).Count());
        }

        [Fact]
        public async void TestAddInvalidPlayer()
        {
            //Arrange           
            var db = MockDb.CreateMockDb();
            var c = new PlayersController(db);


            var player = new Player { Player_Id = 2, LastName = "Mar", JerseyNumber = 23, Position = "SG", TeamId = 2 };
            c.ModelState.AddModelError("FirstName", "Required");
            //Act
            var r = await c.Create(player);
            //Assert
            var result = Assert.IsType<ViewResult>(r);
            var model = Assert.IsAssignableFrom<Player>(result.ViewData.Model);
            Assert.Equal(player, model);
            Assert.IsType<SelectList>(result.ViewData["TeamId"]);
        }

        [Fact]
        public async void IndexTest()
        {
            //Arrange
            var dbContext = MockDb.CreateMockDb();
            PlayersController sc = new PlayersController(dbContext);
            //Act
            var r = await sc.Index();
            //Assert
            var result = Assert.IsType<ViewResult>(r);
            var model = Assert.IsAssignableFrom<List<Player>>(result.ViewData.Model);
            Assert.Equal(1, model.Count());
        }
    }
}
