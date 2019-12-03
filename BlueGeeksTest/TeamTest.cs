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


		/*Test is the delete page for a specific id loads*/
		[Fact]
		public async void DeletePageTest()
		{
			//Arrange           
			var db = MockDb.CreateMockDb();
			var c = new TeamsController(db);

			var teams = new Teams { Team_Name = "Everett Otters", Team_Mascot = "Otter", Team_Id = 2, Conference = "Eastern", Wins = 0, Loses = 0, Ties = 0, Win_Streak = 0 };
			//Act
			await c.Create(teams);

			var r = await c.Delete(db.Teams.Find(2).Team_Id);


			//Assert
			var result = Assert.IsType<ViewResult>(r);
			var model = Assert.IsAssignableFrom<Teams>(result.ViewData.Model);
			Assert.Equal(db.Teams.Find(2).Team_Id, model.Team_Id);
			Assert.Equal(db.Teams.Find(2).Team_Name, model.Team_Name);

		}

		/*Test for null value in ID to delete*/
		[Fact]
		public async void DeleteNullPageTest()
		{
			//Arrange           
			var db = MockDb.CreateMockDb();
			var c = new TeamsController(db);

			//Act
			var r = await c.Delete(null);


			//Assert
			Assert.IsType<NotFoundResult>(r);
		}


		/*Test for an id that is not present in database*/
		[Fact]
		public async void DeleteCoachNotFoundPageTest()
		{
			//Arrange           
			var db = MockDb.CreateMockDb();
			var c = new TeamsController(db);

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
			var c = new TeamsController(db);

			var teams = new Teams { Team_Name = "Everett Otters", Team_Mascot = "Otter", Team_Id = 2, Conference = "Eastern", Wins = 0, Loses = 0, Ties = 0, Win_Streak = 0 };
			//Act
			var r = await c.Create(teams);
			//Finds the second element stores in a tmp variable
			var elementToDelete = db.Teams.Find(2);

			await c.DeleteConfirmed(elementToDelete.Team_Id);

			Assert.DoesNotContain(elementToDelete, db.Teams);
		}

		/*Test is the details page for a specific id loads*/
		[Fact]
		public async void DetailPageTest()
		{
			//Arrange           
			var db = MockDb.CreateMockDb();
			var c = new TeamsController(db);

			var teams = new Teams { Team_Name = "Everett Otters", Team_Mascot = "Otter", Team_Id = 2, Conference = "Eastern", Wins = 0, Loses = 0, Ties = 0, Win_Streak = 0 };
			//Act
			await c.Create(teams);

			var r = await c.Details(db.Teams.Find(2).Team_Id);


			//Assert
			var result = Assert.IsType<ViewResult>(r);
			var model = Assert.IsAssignableFrom<Teams>(result.ViewData.Model);
			Assert.Equal(db.Teams.Find(2).Team_Id, model.Team_Id);
			Assert.Equal(db.Teams.Find(2).Team_Name, model.Team_Name);
		}

		/*Test for null value in ID to delete*/
		[Fact]
		public async void DetailsNullPageTest()
		{
			//Arrange           
			var db = MockDb.CreateMockDb();
			var c = new TeamsController(db);

			//Act
			var r = await c.Details(null);


			//Assert
			Assert.IsType<NotFoundResult>(r);
		}


		/*Test for an id that is not present in database*/
		[Fact]
		public async void DetailsCoachNotFoundPageTest()
		{
			//Arrange           
			var db = MockDb.CreateMockDb();
			var c = new TeamsController(db);

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
			var c = new TeamsController(db);

			var teams = new Teams { Team_Name = "Everett Otters", Team_Mascot = "Otter", Team_Id = 2, Conference = "Eastern", Wins = 0, Loses = 0, Ties = 0, Win_Streak = 0 };
			//Act
			await c.Create(teams);

			var r = await c.Edit(db.Teams.Find(2).Team_Id);


			//Assert
			var result = Assert.IsType<ViewResult>(r);
			var model = Assert.IsAssignableFrom<Teams>(result.ViewData.Model);
			Assert.Equal(db.Teams.Find(2).Team_Id, model.Team_Id);
			Assert.Equal(db.Teams.Find(2).Team_Name, model.Team_Name);
		}

		/*Test for null value in ID to delete*/
		[Fact]
		public async void EditNullPageTest()
		{
			//Arrange           
			var db = MockDb.CreateMockDb();
			var c = new TeamsController(db);

			//Act
			var r = await c.Edit(null);


			//Assert
			Assert.IsType<NotFoundResult>(r);
		}


		/*Test for an id that is not present in database*/
		[Fact]
		public async void EditCoachNotFoundPageTest()
		{
			//Arrange           
			var db = MockDb.CreateMockDb();
			var c = new TeamsController(db);

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
			var c = new TeamsController(db);
			string tempName = "North York";

			var teams = new Teams { Team_Name = "Everett Otters", Team_Mascot = "Otter", Team_Id = 2, Conference = "Eastern", Wins = 0, Loses = 0, Ties = 0, Win_Streak = 0 };
			//Act
			await c.Create(teams);

			teams.Team_Name = tempName;
			var r = await c.Edit(db.Teams.Find(2).Team_Id, teams);

			//Assert
			Assert.IsType<RedirectToActionResult>(r);
			Assert.Equal(db.Teams.Find(2).Team_Name, tempName);
		}

		/*Test is wrong id in edit*/
		[Fact]
		public async void EditWrongIdTest()
		{
			//Arrange           
			var db = MockDb.CreateMockDb();
			var c = new TeamsController(db);

			var teams = new Teams { Team_Name = "Everett Otters", Team_Mascot = "Otter", Team_Id = 2, Conference = "Eastern", Wins = 0, Loses = 0, Ties = 0, Win_Streak = 0 };
			//Act
			await c.Create(teams);

			var r = await c.Edit(db.Teams.Find(1).Team_Id, teams);

			//Assert
			Assert.IsType<NotFoundResult>(r);
		}

		/*Test for model state not valid in edit*/
		[Fact]
		public async void EditModelStateNotValidTest()
		{
			//Arrange           
			var db = MockDb.CreateMockDb();
			var c = new TeamsController(db);

			var teams = new Teams {Team_Mascot = "Otter", Team_Id = 2, Conference = "Eastern", Wins = 0, Loses = 0, Ties = 0, Win_Streak = 0 };


			//Act
			await c.Create(teams);

			c.ModelState.AddModelError("Team_Name", "Required");

			var r = await c.Edit(db.Teams.Find(2).Team_Id, teams);

			//Assert
			var result = Assert.IsType<ViewResult>(r);
			var model = Assert.IsAssignableFrom<Teams>(result.ViewData.Model);
			Assert.Equal(teams, model);
		}

		/*Test if Create Button works*/
		[Fact]
		public async void CreateButtonTest()
		{
			//Arrange           
			var db = MockDb.CreateMockDb();
			var c = new TeamsController(db);

			var teams = new Teams { Team_Name = "Everett Otters", Team_Mascot = "Otter", Team_Id = 2, Conference = "Eastern", Wins = 0, Loses = 0, Ties = 0, Win_Streak = 0 };

			//Act
			await c.Create(teams);

			var r = c.Create();

			//Assert
			Assert.IsType<ViewResult>(r);

		}
	}
}
