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

        /*Test is the delete page for a specific id loads*/
        [Fact]
        public async void DeletePageTest()
        {
            //Arrange           
            var db = MockDb.CreateMockDb();
            var c = new PlayersController(db);

            var player = new Player { Player_Id = 2, FirstName = "Mar", LastName = "Kal", JerseyNumber = 3, Position = "Head", TeamId = 2 };
            //Act
            await c.Create(player);

            var r = await c.Delete(db.Player.Find(2).Player_Id);


            //Assert
            var result = Assert.IsType<ViewResult>(r);
            var model = Assert.IsAssignableFrom<Player>(result.ViewData.Model);
            Assert.Equal(db.Player.Find(2).Player_Id, model.Player_Id);
            Assert.Equal(db.Player.Find(2).FirstName + db.Player.Find(2).LastName, model.FirstName + model.LastName);

        }

        /*Test for null value in ID to delete*/
        [Fact]
        public async void DeleteNullPageTest()
        {
            //Arrange           
            var db = MockDb.CreateMockDb();
            var c = new PlayersController(db);

            //Act
            var r = await c.Delete(null);


            //Assert
            Assert.IsType<NotFoundResult>(r);
        }


        /*Test for an id that is not present in database*/
        [Fact]
        public async void DeletePlayerNotFoundPageTest()
        {
            //Arrange           
            var db = MockDb.CreateMockDb();
            var c = new PlayersController(db);

            //Act
            var r = await c.Delete(2);


            //Assert
            Assert.IsType<NotFoundResult>(r);
        }

        /*Test if deleting an element actually works*/
        [Fact]
        public async void DeleteConfirmTest()
        {
            //Arrange           
            var db = MockDb.CreateMockDb();
            var c = new PlayersController(db);

            var player = new Player { Player_Id = 2, FirstName = "Mar", LastName = "dar", Position = "Head", JerseyNumber = 8 , TeamId = 2 };
            //Act
            var r = await c.Create(player);
            //Finds the second element stores in a tmp variable
            var elementToDelete = db.Player.Find(2);

            await c.DeleteConfirmed(elementToDelete.Player_Id);

            Assert.DoesNotContain(elementToDelete, db.Player);
        }

        /*Test is the details page for a specific id loads*/
        [Fact]
        public async void DetailPageTest()
        {
            //Arrange           
            var db = MockDb.CreateMockDb();
            var c = new PlayersController(db);

            var player = new Player { Player_Id = 2, FirstName = "Mar", LastName = "dar", Position = "Head", JerseyNumber = 8, TeamId = 1 };
            //Act
            await c.Create(player);

            var r = await c.Details(db.Player.Find(2).Player_Id);


            //Assert
            var result = Assert.IsType<ViewResult>(r);
            var model = Assert.IsAssignableFrom<Player>(result.ViewData.Model);
            Assert.Equal(db.Player.Find(2).Player_Id, model.Player_Id);
            Assert.Equal(db.Player.Find(2).FirstName + db.Player.Find(2).LastName, model.FirstName + model.LastName);

        }

        /*Test for null value in ID to delete*/
        [Fact]
        public async void DetailsNullPageTest()
        {
            //Arrange           
            var db = MockDb.CreateMockDb();
            var c = new PlayersController(db);

            //Act
            var r = await c.Details(null);


            //Assert
            Assert.IsType<NotFoundResult>(r);
        }


        /*Test for an id that is not present in database*/
        [Fact]
        public async void DetailsPlayerNotFoundPageTest()
        {
            //Arrange           
            var db = MockDb.CreateMockDb();
            var c = new PlayersController(db);

            //Act
            var r = await c.Details(2);


            //Assert
            Assert.IsType<NotFoundResult>(r);
        }

        /*Test is the delete page for a specific id loads*/
        [Fact]
        public async void EditPageTest()
        {
            //Arrange           
            var db = MockDb.CreateMockDb();
            var c = new PlayersController(db);

            var player = new Player { Player_Id = 2, FirstName = "Mar", LastName = "dar", Position = "Head", JerseyNumber = 8, TeamId = 2 };
            //Act
            await c.Create(player);

            var r = await c.Edit(db.Player.Find(2).Player_Id);


            //Assert
            var result = Assert.IsType<ViewResult>(r);
            var model = Assert.IsAssignableFrom<Player>(result.ViewData.Model);
            Assert.Equal(db.Player.Find(2).Player_Id, model.Player_Id);
            Assert.Equal(db.Player.Find(2).FirstName + db.Player.Find(2).LastName, model.FirstName + model.LastName);

        }

        /*Test for null value in ID to delete*/
        [Fact]
        public async void EditNullPageTest()
        {
            //Arrange           
            var db = MockDb.CreateMockDb();
            var c = new PlayersController(db);

            //Act
            var r = await c.Edit(null);


            //Assert
            Assert.IsType<NotFoundResult>(r);
        }


        /*Test for an id that is not present in database*/
        [Fact]
        public async void EditPlayerNotFoundPageTest()
        {
            //Arrange           
            var db = MockDb.CreateMockDb();
            var c = new PlayersController(db);

            //Act
            var r = await c.Edit(2);


            //Assert
            Assert.IsType<NotFoundResult>(r);
        }

        /*Test is theediting actually changes values*/
        [Fact]
        public async void EditTest()
        {
            //Arrange           
            var db = MockDb.CreateMockDb();
            var c = new PlayersController(db);
            string tempName = "Micheal";

            var player = new Player { Player_Id = 2, FirstName = "Mar", LastName = "dar", Position = "Head", JerseyNumber = 8, TeamId = 2 };
            //Act
            await c.Create(player);

            player.FirstName = tempName;
            var r = await c.Edit(db.Player.Find(2).Player_Id, player);

            //Assert
            Assert.IsType<RedirectToActionResult>(r);
            Assert.Equal(db.Player.Find(2).FirstName, tempName);
        }

        /*Test is wrong id in edit*/
        [Fact]
        public async void EditWrongIdTest()
        {
            //Arrange           
            var db = MockDb.CreateMockDb();
            var c = new PlayersController(db);

            var player = new Player { Player_Id = 2, FirstName = "Mar", LastName = "dar", Position = "Head", JerseyNumber = 8, TeamId = 2 };
            //Act
            await c.Create(player);

            var r = await c.Edit(db.Player.Find(1).Player_Id, player);

            //Assert
            Assert.IsType<NotFoundResult>(r);
        }

        /*Test for model state not valid in edit*/
        [Fact]
        public async void EditModelStateNotValidTest()
        {
            //Arrange           
            var db = MockDb.CreateMockDb();
            var c = new PlayersController(db);

            var player = new Player { Player_Id = 2, FirstName = "Mar", LastName = "dar", Position = "Head", JerseyNumber = 8, TeamId = 2 };


            //Act
            await c.Create(player);

            c.ModelState.AddModelError("FirstName", "Required");

            var r = await c.Edit(db.Player.Find(2).Player_Id, player);

            //Assert
            var result = Assert.IsType<ViewResult>(r);
            var model = Assert.IsAssignableFrom<Player>(result.ViewData.Model);
            Assert.Equal(player, model);
        }

        /*Test if Create Button works*/
        [Fact]
        public async void CreateButtonTest()
        {
            //Arrange           
            var db = MockDb.CreateMockDb();
            var c = new PlayersController(db);

            var player = new Player { Player_Id = 2, FirstName = "Mar", LastName = "dar", Position = "Head", JerseyNumber = 8, TeamId = 2 };

            //Act
            await c.Create(player);

            var r = c.Create();

            //Assert
            Assert.IsType<ViewResult>(r);

        }
    }
}
