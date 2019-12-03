using BlueGeeks.Controllers;
using BlueGeeks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BlueGeeksTest
{
    public class StadiumTest
    {
        [Fact]
        public async void TestAddStadium()
        {
            //Arrange           
            var db = MockDb.CreateMockDb();
            var c = new StadiumsController(db);


            var stadiums = new Stadium { Stadium_Id = 2, StadiumName = "Ever After", City = "Everett", Team_Id = 1 };
            //Act
            var r = await c.Create(stadiums);
            //Assert
            var result = Assert.IsType<RedirectToActionResult>(r);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal(1, db.Stadium.Where(x => x.Stadium_Id == stadiums.Stadium_Id && x.StadiumName == stadiums.StadiumName && x.City == stadiums.City && x.Team_Id == stadiums.Team_Id).Count());
        }

        [Fact]
        public async void TestAddInvalidStadium()
        {
            //Arrange           
            var db = MockDb.CreateMockDb();
            var c = new StadiumsController(db);


            var stadiums = new Stadium { Stadium_Id = 2, City = "Everett", Team_Id = 2 };
            c.ModelState.AddModelError("StadiumName", "Required");
            //Act
            var r = await c.Create(stadiums);
            //Assert
            var result = Assert.IsType<ViewResult>(r);
            var model = Assert.IsAssignableFrom<Stadium>(result.ViewData.Model);
            Assert.Equal(stadiums, model);
            Assert.IsType<SelectList>(result.ViewData["Team_Id"]);
        }

        [Fact]
        public async void IndexTest()
        {
            //Arrange
            var dbContext = MockDb.CreateMockDb();
            StadiumsController sc = new StadiumsController(dbContext);
            //Act
            var r = await sc.Index();
            //Assert
            var result = Assert.IsType<ViewResult>(r);
            var model = Assert.IsAssignableFrom<List<Stadium>>(result.ViewData.Model);
            Assert.Equal(1, model.Count());
        }

		/*Test is the delete page for a specific id loads*/
		[Fact]
		public async void DeletePageTest()
		{
			//Arrange           
			var db = MockDb.CreateMockDb();
			var c = new StadiumsController(db);

			var stadiums = new Stadium { Stadium_Id = 2, StadiumName = "Ever After", City = "Everett", Team_Id = 1 };
			//Act
			await c.Create(stadiums);

			var r = await c.Delete(db.Stadium.Find(2).Stadium_Id);


			//Assert
			var result = Assert.IsType<ViewResult>(r);
			var model = Assert.IsAssignableFrom<Stadium>(result.ViewData.Model);
			Assert.Equal(db.Stadium.Find(2).Stadium_Id, model.Stadium_Id);
			Assert.Equal(db.Stadium.Find(2).StadiumName, model.StadiumName);

		}

		/*Test for null value in ID to delete*/
		[Fact]
		public async void DeleteNullPageTest()
		{
			//Arrange           
			var db = MockDb.CreateMockDb();
			var c = new StadiumsController(db);

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
			var c = new StadiumsController(db);

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
			var c = new StadiumsController(db);

			var stadiums = new Stadium { Stadium_Id = 2, StadiumName = "Ever After", City = "Everett", Team_Id = 1 };
			//Act
			var r = await c.Create(stadiums);
			//Finds the second element stores in a tmp variable
			var elementToDelete = db.Stadium.Find(2);

			await c.DeleteConfirmed(elementToDelete.Stadium_Id);

			Assert.DoesNotContain(elementToDelete, db.Stadium);
		}

		/*Test is the details page for a specific id loads*/
		[Fact]
		public async void DetailPageTest()
		{
			//Arrange           
			var db = MockDb.CreateMockDb();
			var c = new StadiumsController(db);

			var stadiums = new Stadium { Stadium_Id = 2, StadiumName = "Ever After", City = "Everett", Team_Id = 1 };
			//Act
			await c.Create(stadiums);

			var r = await c.Details(db.Stadium.Find(2).Stadium_Id);


			//Assert
			var result = Assert.IsType<ViewResult>(r);
			var model = Assert.IsAssignableFrom<Stadium>(result.ViewData.Model);
			Assert.Equal(db.Stadium.Find(2).Stadium_Id, model.Stadium_Id);
			Assert.Equal(db.Stadium.Find(2).StadiumName, model.StadiumName);

		}

		/*Test for null value in ID to delete*/
		[Fact]
		public async void DetailsNullPageTest()
		{
			//Arrange           
			var db = MockDb.CreateMockDb();
			var c = new StadiumsController(db);

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
			var c = new StadiumsController(db);

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
			var c = new StadiumsController(db);

			var stadiums = new Stadium { Stadium_Id = 2, StadiumName = "Ever After", City = "Everett", Team_Id = 1 };
			//Act
			await c.Create(stadiums);

			var r = await c.Edit(db.Stadium.Find(2).Stadium_Id);


			//Assert
			var result = Assert.IsType<ViewResult>(r);
			var model = Assert.IsAssignableFrom<Stadium>(result.ViewData.Model);
			Assert.Equal(db.Stadium.Find(2).Stadium_Id, model.Stadium_Id);
			Assert.Equal(db.Stadium.Find(2).StadiumName, model.StadiumName);

		}

		/*Test for null value in ID to delete*/
		[Fact]
		public async void EditNullPageTest()
		{
			//Arrange           
			var db = MockDb.CreateMockDb();
			var c = new StadiumsController(db);

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
			var c = new StadiumsController(db);

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
			var c = new StadiumsController(db);
			string tempName = "ScotiaBank Arena";

			var stadiums = new Stadium { Stadium_Id = 2, StadiumName = "Ever After", City = "Everett", Team_Id = 1 };
			//Act
			await c.Create(stadiums);

			stadiums.StadiumName = tempName;
			var r = await c.Edit(db.Stadium.Find(2).Stadium_Id, stadiums);

			//Assert
			Assert.IsType<RedirectToActionResult>(r);
			Assert.Equal(db.Stadium.Find(2).StadiumName, tempName);
		}

		/*Test is wrong id in edit*/
		[Fact]
		public async void EditWrongIdTest()
		{
			//Arrange           
			var db = MockDb.CreateMockDb();
			var c = new StadiumsController(db);

			var stadiums = new Stadium { Stadium_Id = 2, StadiumName = "Ever After", City = "Everett", Team_Id = 1 };
			//Act
			await c.Create(stadiums);

			var r = await c.Edit(db.Stadium.Find(1).Stadium_Id, stadiums);

			//Assert
			Assert.IsType<NotFoundResult>(r);
		}

		/*Test for model state not valid in edit*/
		[Fact]
		public async void EditModelStateNotValidTest()
		{
			//Arrange           
			var db = MockDb.CreateMockDb();
			var c = new StadiumsController(db);

			var stadiums = new Stadium { Stadium_Id = 2, City = "Everett", Team_Id = 1 };


			//Act
			await c.Create(stadiums);

			c.ModelState.AddModelError("StadiumName", "Required");

			var r = await c.Edit(db.Stadium.Find(2).Stadium_Id, stadiums);

			//Assert
			var result = Assert.IsType<ViewResult>(r);
			var model = Assert.IsAssignableFrom<Stadium>(result.ViewData.Model);
			Assert.Equal(stadiums, model);
		}


		/*Test if Create Button works*/
		[Fact]
		public async void CreateButtonTest()
		{
			//Arrange           
			var db = MockDb.CreateMockDb();
			var c = new StadiumsController(db);

			var stadiums = new Stadium { Stadium_Id = 2, StadiumName = "Ever After", City = "Everett", Team_Id = 1 };

			//Act
			await c.Create(stadiums);

			var r = c.Create();

			//Assert
			Assert.IsType<ViewResult>(r);

		}
	}

    
}
