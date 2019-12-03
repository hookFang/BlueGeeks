using BlueGeeks.Controllers;
using BlueGeeks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BlueGeeksTest
{
    public class MatchTest
    {
        [Fact]
        public async void TestAddMatch()
        {
            //Arrange           
            var db = MockDb.CreateMockDb();
            var c = new MatchesController(db);


            var match = new Matches { Matche_Id = 2, HomeTeam_Id = 1, AwayTeam_Id = 2, MatchDate = DateTime.Now, Stadium_Id = 1};
            //Act
            var r = await c.Create(match);
            //Assert
            var result = Assert.IsType<RedirectToActionResult>(r);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal(1, db.Matches.Where(x => x.Matche_Id == match.Matche_Id && x.HomeTeam_Id == match.HomeTeam_Id && x.AwayTeam_Id == match.AwayTeam_Id && x.MatchDate == match.MatchDate && x.Stadium_Id == match.Stadium_Id).Count());
        }

        [Fact]
        public async void TestAddInvalidMatch()
        {
            //Arrange           
            var db = MockDb.CreateMockDb();
            var c = new MatchesController(db);


            var match = new Matches { Matche_Id = 2, AwayTeam_Id = 2, MatchDate = DateTime.Now, Stadium_Id = 1 };
            c.ModelState.AddModelError("HomeTeam_Id", "Required");
            //Act
            var r = await c.Create(match);
            //Assert
            var result = Assert.IsType<ViewResult>(r);
            var model = Assert.IsAssignableFrom<Matches>(result.ViewData.Model);
            Assert.Equal(match, model);
            Assert.IsType<SelectList>(result.ViewData["AwayTeam_Id"]);
            Assert.IsType<SelectList>(result.ViewData["HomeTeam_Id"]);
            Assert.IsType<SelectList>(result.ViewData["Stadium_Id"]);
        }

        [Fact]
        public async void IndexTest()
        {
            //Arrange
            var dbContext = MockDb.CreateMockDb();
            MatchesController sc = new MatchesController(dbContext);
            //Act
            var r = await sc.Index();
            //Assert
            var result = Assert.IsType<ViewResult>(r);
            var model = Assert.IsAssignableFrom<List<Matches>>(result.ViewData.Model);
            Assert.Equal(1, model.Count());
        }


		/*Test is the delete page for a specific id loads*/
		[Fact]
		public async void DeletePageTest()
		{
			//Arrange           
			var db = MockDb.CreateMockDb();
			var c = new MatchesController(db);

			var match = new Matches { Matche_Id = 2, AwayTeam_Id = 2, MatchDate = DateTime.Now, Stadium_Id = 1 };
			//Act
			await c.Create(match);

			var r = await c.Delete(db.Matches.Find(2).Matche_Id);


			//Assert
			var result = Assert.IsType<ViewResult>(r);
			var model = Assert.IsAssignableFrom<Matches>(result.ViewData.Model);
			Assert.Equal(db.Matches.Find(2).Matche_Id, model.Matche_Id);
			Assert.Equal(db.Matches.Find(2).AwayTeam_Id, model.AwayTeam_Id);

		}

		/*Test for null value in ID to delete*/
		[Fact]
		public async void DeleteNullPageTest()
		{
			//Arrange           
			var db = MockDb.CreateMockDb();
			var c = new MatchesController(db);

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
			var c = new MatchesController(db);

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
			var c = new MatchesController(db);

			var match = new Matches { Matche_Id = 2, AwayTeam_Id = 2, MatchDate = DateTime.Now, Stadium_Id = 1 };
			//Act
			var r = await c.Create(match);
			//Finds the second element stores in a tmp variable
			var elementToDelete = db.Matches.Find(2);

			await c.DeleteConfirmed(elementToDelete.Matche_Id);

			Assert.DoesNotContain(elementToDelete, db.Matches);
		}

		/*Test is the details page for a specific id loads*/
		[Fact]
		public async void DetailPageTest()
		{
			//Arrange           
			var db = MockDb.CreateMockDb();
			var c = new MatchesController(db);

			var match = new Matches { Matche_Id = 2, AwayTeam_Id = 1, HomeTeam_Id=1, MatchDate = DateTime.Now, Stadium_Id = 1 };
			//Act
			await c.Create(match);

			var r = await c.Details(db.Matches.Find(2).Matche_Id);


			//Assert
			var result = Assert.IsType<ViewResult>(r);
			var model = Assert.IsAssignableFrom<Matches>(result.ViewData.Model);
			Assert.Equal(db.Matches.Find(2).Matche_Id, model.Matche_Id);
			Assert.Equal(db.Matches.Find(2).AwayTeam_Id, model.AwayTeam_Id);
		}

		/*Test for null value in ID to delete*/
		[Fact]
		public async void DetailsNullPageTest()
		{
			//Arrange           
			var db = MockDb.CreateMockDb();
			var c = new MatchesController(db);

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
			var c = new MatchesController(db);

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
			var c = new MatchesController(db);

			var match = new Matches { Matche_Id = 2, AwayTeam_Id = 2, MatchDate = DateTime.Now, Stadium_Id = 1 };
			//Act
			await c.Create(match);

			var r = await c.Edit(db.Matches.Find(2).Matche_Id);


			//Assert
			var result = Assert.IsType<ViewResult>(r);
			var model = Assert.IsAssignableFrom<Matches>(result.ViewData.Model);
			Assert.Equal(db.Matches.Find(2).Matche_Id, model.Matche_Id);
			Assert.Equal(db.Matches.Find(2).AwayTeam_Id, model.AwayTeam_Id);
		}

		/*Test for null value in ID to delete*/
		[Fact]
		public async void EditNullPageTest()
		{
			//Arrange           
			var db = MockDb.CreateMockDb();
			var c = new MatchesController(db);

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
			var c = new MatchesController(db);

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
			var c = new MatchesController(db);
			int tempId = 1;

			var match = new Matches { Matche_Id = 2, AwayTeam_Id = 2, MatchDate = DateTime.Now, Stadium_Id = 1 };
			//Act
			await c.Create(match);

			match.AwayTeam_Id = tempId;
			var r = await c.Edit(db.Matches.Find(2).Matche_Id, match);

			//Assert
			Assert.IsType<RedirectToActionResult>(r);
			Assert.Equal(db.Matches.Find(2).AwayTeam_Id, tempId);
		}

		/*Test is wrong id in edit*/
		[Fact]
		public async void EditWrongIdTest()
		{
			//Arrange           
			var db = MockDb.CreateMockDb();
			var c = new MatchesController(db);

			var match = new Matches { Matche_Id = 2, AwayTeam_Id = 2, MatchDate = DateTime.Now, Stadium_Id = 1 };
			//Act
			await c.Create(match);

			var r = await c.Edit(db.Matches.Find(1).Matche_Id, match);

			//Assert
			Assert.IsType<NotFoundResult>(r);
		}

		/*Test for model state not valid in edit*/
		[Fact]
		public async void EditModelStateNotValidTest()
		{
			//Arrange           
			var db = MockDb.CreateMockDb();
			var c = new MatchesController(db);

			var match = new Matches { Matche_Id = 2, MatchDate = DateTime.Now, Stadium_Id = 1 };


			//Act
			await c.Create(match);

			c.ModelState.AddModelError("AwayTeam_Id", "Required");

			var r = await c.Edit(db.Matches.Find(2).Matche_Id, match);

			//Assert
			var result = Assert.IsType<ViewResult>(r);
			var model = Assert.IsAssignableFrom<Matches>(result.ViewData.Model);
			Assert.Equal(match, model);
		}

		/*Test if Create Button works*/
		[Fact]
		public async void CreateButtonTest()
		{
			//Arrange           
			var db = MockDb.CreateMockDb();
			var c = new MatchesController(db);

			var match = new Matches { Matche_Id = 2, AwayTeam_Id = 2, MatchDate = DateTime.Now, Stadium_Id = 1 };

			//Act
			await c.Create(match);

			var r = c.Create();

			//Assert
			Assert.IsType<ViewResult>(r);

		}
	}
}
