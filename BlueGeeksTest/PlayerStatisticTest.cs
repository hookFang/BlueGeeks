using BlueGeeks.Controllers;
using BlueGeeks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace BlueGeeksTest
{
	public class PlayerStatisticTest
	{
		[Fact]
		public async void TestAddPlayerStatistic()
		{
			//Arrange           
			var db = MockDb.CreateMockDb();
			var c = new PlayerStatisticsController(db);

			var playerStatistic = new PlayerStatistics { Player_Statistics_Id = 2, FgPercent = 0.379F, FtPercent = 1.000F, ThreePointersMade = 3, PointsMade = 15, Rebounds = 2, Assists = 6, Steals = 6, Blocks = 5, TurnOvers = 3, Player_Id = 1 };
			//Act
			var r = await c.Create(playerStatistic);
			//Assert
			var result = Assert.IsType<RedirectToActionResult>(r);
			Assert.Equal("Index", result.ActionName);
			Assert.Equal(1, db.PlayerStatistics.Where(x => x.Player_Statistics_Id == playerStatistic.Player_Statistics_Id && x.FgPercent == playerStatistic.FgPercent && x.FtPercent == playerStatistic.FtPercent
			&& x.ThreePointersMade == playerStatistic.ThreePointersMade && x.PointsMade == playerStatistic.PointsMade && x.Rebounds == playerStatistic.Rebounds && x.Assists == playerStatistic.Assists && x.Blocks == playerStatistic.Blocks 
			&& x.TurnOvers == playerStatistic.TurnOvers && x.Player_Id == playerStatistic.Player_Id).Count());
		}

        [Fact]
        public async void TestAddInvalidPlayerStatistic()
        {
            //Arrange           
            var db = MockDb.CreateMockDb();
            var c = new PlayerStatisticsController(db);


            var playerStatistic = new PlayerStatistics { Player_Statistics_Id = 2, FgPercent = 0.379F, FtPercent = 1.000F, ThreePointersMade = 3, PointsMade = 15, Assists = 6, Steals = 6, Blocks = 5, TurnOvers = 3, Player_Id = 1 };
            c.ModelState.AddModelError("Rebounds", "Required");
            //Act
            var r = await c.Create(playerStatistic);
            //Assert
            var result = Assert.IsType<ViewResult>(r);
            var model = Assert.IsAssignableFrom<PlayerStatistics>(result.ViewData.Model);
            Assert.Equal(playerStatistic, model);
            Assert.IsType<SelectList>(result.ViewData["Player_Id"]);
        }

        [Fact]
		public async void IndexTest()
		{
			//Arrange
			var dbContext = MockDb.CreateMockDb();
			PlayerStatisticsController sc = new PlayerStatisticsController(dbContext);
			//Act
			var r = await sc.Index();
			//Assert
			var result = Assert.IsType<ViewResult>(r);
			var model = Assert.IsAssignableFrom<List<PlayerStatistics>>(result.ViewData.Model);
			Assert.Equal(1, model.Count());
		}

        /*Test is the delete page for a specific id loads*/
        [Fact]
        public async void DeletePageTest()
        {
            //Arrange           
            var db = MockDb.CreateMockDb();
            var c = new PlayerStatisticsController(db);

            var playerStatistic = new PlayerStatistics { Player_Statistics_Id = 2, FgPercent = 0.379F, FtPercent = 1.000F, ThreePointersMade = 3, PointsMade = 15, Rebounds = 2, Assists = 6, Steals = 6, Blocks = 5, TurnOvers = 3, Player_Id = 1 };
            //Act
            await c.Create(playerStatistic);

            var r = await c.Delete(db.PlayerStatistics.Find(2).Player_Statistics_Id);


            //Assert
            var result = Assert.IsType<ViewResult>(r);
            var model = Assert.IsAssignableFrom<PlayerStatistics>(result.ViewData.Model);
            Assert.Equal(db.PlayerStatistics.Find(2).Player_Statistics_Id, model.Player_Statistics_Id);
            Assert.Equal(db.PlayerStatistics.Find(2).Rebounds + db.PlayerStatistics.Find(2).Steals, model.Rebounds + model.Steals);

        }

        /*Test for null value in ID to delete*/
        [Fact]
        public async void DeleteNullPageTest()
        {
            //Arrange           
            var db = MockDb.CreateMockDb();
            var c = new PlayerStatisticsController(db);

            //Act
            var r = await c.Delete(null);


            //Assert
            Assert.IsType<NotFoundResult>(r);
        }


        /*Test for an id that is not present in database*/
        [Fact]
        public async void DeletePlayerStatisticsNotFoundPageTest()
        {
            //Arrange           
            var db = MockDb.CreateMockDb();
            var c = new PlayerStatisticsController(db);

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
            var c = new PlayerStatisticsController(db);

            var playerStatistic = new PlayerStatistics { Player_Statistics_Id = 2, FgPercent = 0.379F, FtPercent = 1.000F, ThreePointersMade = 3, PointsMade = 15, Rebounds = 2, Assists = 6, Steals = 6, Blocks = 5, TurnOvers = 3, Player_Id = 1 };
            //Act
            var r = await c.Create(playerStatistic);
            //Finds the second element stores in a tmp variable
            var elementToDelete = db.PlayerStatistics.Find(2);

            await c.DeleteConfirmed(elementToDelete.Player_Statistics_Id);

            Assert.DoesNotContain(elementToDelete, db.PlayerStatistics);
        }

        /*Test is the details page for a specific id loads*/
        [Fact]
        public async void DetailPageTest()
        {
            //Arrange           
            var db = MockDb.CreateMockDb();
            var c = new PlayerStatisticsController(db);

            var playerStatistic = new PlayerStatistics { Player_Statistics_Id = 2, FgPercent = 0.379F, FtPercent = 1.000F, ThreePointersMade = 3, PointsMade = 15, Rebounds = 2, Assists = 6, Steals = 6, Blocks = 5, TurnOvers = 3, Player_Id = 1 };
            //Act
            await c.Create(playerStatistic);

            var r = await c.Details(db.PlayerStatistics.Find(2).Player_Statistics_Id);


            //Assert
            var result = Assert.IsType<ViewResult>(r);
            var model = Assert.IsAssignableFrom<PlayerStatistics>(result.ViewData.Model);
            Assert.Equal(db.PlayerStatistics.Find(2).Player_Statistics_Id, model.Player_Statistics_Id);
            Assert.Equal(db.PlayerStatistics.Find(2).Rebounds + db.PlayerStatistics.Find(2).Steals, model.Rebounds + model.Steals);

        }

        /*Test for null value in ID to delete*/
        [Fact]
        public async void DetailsNullPageTest()
        {
            //Arrange           
            var db = MockDb.CreateMockDb();
            var c = new PlayerStatisticsController(db);

            //Act
            var r = await c.Details(null);


            //Assert
            Assert.IsType<NotFoundResult>(r);
        }


        /*Test for an id that is not present in database*/
        [Fact]
        public async void DetailsPlayerStatisticsNotFoundPageTest()
        {
            //Arrange           
            var db = MockDb.CreateMockDb();
            var c = new PlayerStatisticsController(db);

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
            var c = new PlayerStatisticsController(db);

            var playerStatistic = new PlayerStatistics { Player_Statistics_Id = 2, FgPercent = 0.379F, FtPercent = 1.000F, ThreePointersMade = 3, PointsMade = 15, Rebounds = 2, Assists = 6, Steals = 6, Blocks = 5, TurnOvers = 3, Player_Id = 1 };
            //Act
            await c.Create(playerStatistic);

            var r = await c.Edit(db.PlayerStatistics.Find(2).Player_Statistics_Id);


            //Assert
            var result = Assert.IsType<ViewResult>(r);
            var model = Assert.IsAssignableFrom<PlayerStatistics>(result.ViewData.Model);
            Assert.Equal(db.PlayerStatistics.Find(2).Player_Statistics_Id, model.Player_Statistics_Id);
            Assert.Equal(db.PlayerStatistics.Find(2).Rebounds + db.PlayerStatistics.Find(2).Steals, model.Rebounds + model.Steals);

        }

        /*Test for null value in ID to delete*/
        [Fact]
        public async void EditNullPageTest()
        {
            //Arrange           
            var db = MockDb.CreateMockDb();
            var c = new PlayerStatisticsController(db);

            //Act
            var r = await c.Edit(null);


            //Assert
            Assert.IsType<NotFoundResult>(r);
        }


        /*Test for an id that is not present in database*/
        [Fact]
        public async void EditPlayerStatisticsNotFoundPageTest()
        {
            //Arrange           
            var db = MockDb.CreateMockDb();
            var c = new PlayerStatisticsController(db);

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
            var c = new PlayerStatisticsController(db);
            short tempRebound = 4;

            var playerStatistic = new PlayerStatistics { Player_Statistics_Id = 2, FgPercent = 0.379F, FtPercent = 1.000F, ThreePointersMade = 3, PointsMade = 15, Rebounds = 2, Assists = 6, Steals = 6, Blocks = 5, TurnOvers = 3, Player_Id = 1 };
            //Act
            await c.Create(playerStatistic);

            playerStatistic.Rebounds = tempRebound;
            var r = await c.Edit(db.PlayerStatistics.Find(2).Player_Statistics_Id, playerStatistic);

            //Assert
            Assert.IsType<RedirectToActionResult>(r);
            Assert.Equal(db.PlayerStatistics.Find(2).Rebounds, tempRebound);
        }

        /*Test is wrong id in edit*/
        [Fact]
        public async void EditWrongIdTest()
        {
            //Arrange           
            var db = MockDb.CreateMockDb();
            var c = new PlayerStatisticsController(db);

            var playerStatistic = new PlayerStatistics { Player_Statistics_Id = 2, FgPercent = 0.379F, FtPercent = 1.000F, ThreePointersMade = 3, PointsMade = 15, Rebounds = 2, Assists = 6, Steals = 6, Blocks = 5, TurnOvers = 3, Player_Id = 1 };
            //Act
            await c.Create(playerStatistic);

            var r = await c.Edit(db.PlayerStatistics.Find(1).Player_Statistics_Id, playerStatistic);


            //Assert
            Assert.IsType<NotFoundResult>(r);
        }

        /*Test for model state not valid in edit*/
        [Fact]
        public async void EditModelStateNotValidTest()
        {
            //Arrange           
            var db = MockDb.CreateMockDb();
            var c = new PlayerStatisticsController(db);

            var playerStatistic = new PlayerStatistics { Player_Statistics_Id = 2, FgPercent = 0.379F, FtPercent = 1.000F, ThreePointersMade = 3, PointsMade = 15, Assists = 6, Steals = 6, Blocks = 5, TurnOvers = 3, Player_Id = 1 };


            //Act
            await c.Create(playerStatistic);

            c.ModelState.AddModelError("Rebounds", "Required");

            var r = await c.Edit(db.PlayerStatistics.Find(2).Player_Statistics_Id, playerStatistic);

            //Assert
            var result = Assert.IsType<ViewResult>(r);
            var model = Assert.IsAssignableFrom<PlayerStatistics>(result.ViewData.Model);
            Assert.Equal(playerStatistic, model);
        }

        /*Test if Create Button works*/
        [Fact]
        public async void CreateButtonTest()
        {
            //Arrange           
            var db = MockDb.CreateMockDb();
            var c = new PlayerStatisticsController(db);


            var playerStatistic = new PlayerStatistics { Player_Statistics_Id = 2, FgPercent = 0.379F, FtPercent = 1.000F, ThreePointersMade = 3, PointsMade = 15, Rebounds = 2, Assists = 6, Steals = 6, Blocks = 5, TurnOvers = 3, Player_Id = 1 };

            //Act
            await c.Create(playerStatistic);

            var r = c.Create();

            //Assert
            Assert.IsType<ViewResult>(r);

        }
    }
}
