using BlueGeeks.Controllers;
using BlueGeeks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BlueGeeksTest
{
    public class CoachTest
    {
		/*Test to add a new coach*/
        [Fact]
        public async void TestAddCoach()
        {
            //Arrange           
            var db = MockDb.CreateMockDb();
            var c = new CoachesController(db);


            var coach = new Coaches { Coaches_Id = 2, FirstName = "Mike", LastName = "Mar", Title = "Head", Team_Id = 2 };
            //Act
            var r = await c.Create(coach);
            //Assert
            var result = Assert.IsType<RedirectToActionResult>(r);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal(1, db.Coaches.Where(x => x.Coaches_Id == coach.Coaches_Id && x.FirstName == coach.FirstName && x.LastName == coach.LastName && x.Title == coach.Title && x.Team_Id == coach.Team_Id).Count());
        }

		/*Test if invalid coach gets added to the database*/
        [Fact]
        public async void TestAddInvalidCoach()
        {
            //Arrange           
            var db = MockDb.CreateMockDb();
            var c = new CoachesController(db);


            var coach = new Coaches { Coaches_Id = 2, LastName = "Mar", Title = "Head", Team_Id = 2 };
            c.ModelState.AddModelError("FirstName", "Required");
            //Act
            var r = await c.Create(coach);
            //Assert
            var result = Assert.IsType<ViewResult>(r);
            var model = Assert.IsAssignableFrom<Coaches>(result.ViewData.Model);
            Assert.Equal(coach, model);
            Assert.IsType<SelectList>(result.ViewData["Team_Id"]);
        }

		/*Test for the Index page*/
        [Fact]
        public async void IndexTest()
        {
            //Arrange
            var dbContext = MockDb.CreateMockDb();
            CoachesController sc = new CoachesController(dbContext);
            //Act
            var r = await sc.Index();
            //Assert
            var result = Assert.IsType<ViewResult>(r);
            var model = Assert.IsAssignableFrom<List<Coaches>>(result.ViewData.Model);
            Assert.Equal(1, model.Count());
        }


		/*Test is the delete page for a specific id loads*/
		[Fact]
		public async void DeletePageTest()
		{
			//Arrange           
			var db = MockDb.CreateMockDb();
			var c = new CoachesController(db);

			var coach = new Coaches { Coaches_Id = 2, LastName = "Mar", Title = "Head", Team_Id = 2 };
			//Act
			await c.Create(coach);

			var r = await c.Delete(db.Coaches.Find(2).Coaches_Id);


			//Assert
			var result = Assert.IsType<ViewResult>(r);
			var model = Assert.IsAssignableFrom<Coaches>(result.ViewData.Model);
			Assert.Equal(db.Coaches.Find(2).Coaches_Id, model.Coaches_Id);
			Assert.Equal(db.Coaches.Find(2).FirstName + db.Coaches.Find(2).LastName, model.FirstName + model.LastName);

		}

		/*Test for null value in ID to delete*/
		[Fact]
		public async void DeleteNullPageTest()
		{
			//Arrange           
			var db = MockDb.CreateMockDb();
			var c = new CoachesController(db);

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
			var c = new CoachesController(db);

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
			var c = new CoachesController(db);

			var coach = new Coaches { Coaches_Id = 2, LastName = "Mar", Title = "Head", Team_Id = 2 };
			//Act
			var r = await c.Create(coach);
			//Finds the second element stores in a tmp variable
			var elementToDelete = db.Coaches.Find(2);

			await c.DeleteConfirmed(elementToDelete.Coaches_Id);

			Assert.DoesNotContain(elementToDelete, db.Coaches);
		}

		/*Test is the details page for a specific id loads*/
		[Fact]
		public async void DetailPageTest()
		{
			//Arrange           
			var db = MockDb.CreateMockDb();
			var c = new CoachesController(db);

			var coach = new Coaches { Coaches_Id = 2, LastName = "Mar", Title = "Head", Team_Id = 1 };
			//Act
			await c.Create(coach);

			var r = await c.Details(db.Coaches.Find(2).Coaches_Id);


			//Assert
			var result = Assert.IsType<ViewResult>(r);
			var model = Assert.IsAssignableFrom<Coaches>(result.ViewData.Model);
			Assert.Equal(db.Coaches.Find(2).Coaches_Id, model.Coaches_Id);
			Assert.Equal(db.Coaches.Find(2).FirstName + db.Coaches.Find(2).LastName, model.FirstName + model.LastName);

		}

		/*Test for null value in ID to delete*/
		[Fact]
		public async void DetailsNullPageTest()
		{
			//Arrange           
			var db = MockDb.CreateMockDb();
			var c = new CoachesController(db);

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
			var c = new CoachesController(db);

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
			var c = new CoachesController(db);

			var coach = new Coaches { Coaches_Id = 2, LastName = "Mar", Title = "Head", Team_Id = 2 };
			//Act
			await c.Create(coach);

			var r = await c.Edit(db.Coaches.Find(2).Coaches_Id);


			//Assert
			var result = Assert.IsType<ViewResult>(r);
			var model = Assert.IsAssignableFrom<Coaches>(result.ViewData.Model);
			Assert.Equal(db.Coaches.Find(2).Coaches_Id, model.Coaches_Id);
			Assert.Equal(db.Coaches.Find(2).FirstName + db.Coaches.Find(2).LastName, model.FirstName + model.LastName);

		}

		/*Test for null value in ID to delete*/
		[Fact]
		public async void EditNullPageTest()
		{
			//Arrange           
			var db = MockDb.CreateMockDb();
			var c = new CoachesController(db);

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
			var c = new CoachesController(db);

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
			var c = new CoachesController(db);
			string tempName = "Micheal";

			var coach = new Coaches { Coaches_Id = 2, FirstName="Edwin", LastName = "Mar", Title = "Head", Team_Id = 2 };
			//Act
			await c.Create(coach);

			coach.FirstName = tempName;
			var r = await c.Edit(db.Coaches.Find(2).Coaches_Id, coach);

			//Assert
			Assert.IsType<RedirectToActionResult>(r);
			Assert.Equal(db.Coaches.Find(2).FirstName, tempName);
		}

		/*Test is wrong id in edit*/
		[Fact]
		public async void EditWrongIdTest()
		{
			//Arrange           
			var db = MockDb.CreateMockDb();
			var c = new CoachesController(db);

			var coach = new Coaches { Coaches_Id = 2, FirstName = "Edwin", LastName = "Mar", Title = "Head", Team_Id = 2 };
			//Act
			await c.Create(coach);

			var r = await c.Edit(db.Coaches.Find(1).Coaches_Id, coach);

			//Assert
			Assert.IsType<NotFoundResult>(r);
		}

		/*Test for model state not valid in edit*/
		[Fact]
		public async void EditModelStateNotValidTest()
		{
			//Arrange           
			var db = MockDb.CreateMockDb();
			var c = new CoachesController(db);

			var coach = new Coaches { Coaches_Id = 2, LastName = "Mar", Title = "Head", Team_Id = 2 };
			

			//Act
			await c.Create(coach);

			c.ModelState.AddModelError("FirstName", "Required");

			var r = await c.Edit(db.Coaches.Find(2).Coaches_Id, coach);

			//Assert
			var result = Assert.IsType<ViewResult>(r);
			var model = Assert.IsAssignableFrom<Coaches>(result.ViewData.Model);
			Assert.Equal(coach, model);
		}

		/*Test if Create Button works*/
		[Fact]
		public async void CreateButtonTest()
		{
			//Arrange           
			var db = MockDb.CreateMockDb();
			var c = new CoachesController(db);

			var coach = new Coaches { Coaches_Id = 2, FirstName = "Edwin", LastName = "Mar", Title = "Head", Team_Id = 2 };

			//Act
			await c.Create(coach);

			var r = c.Create();

			//Assert
			Assert.IsType<ViewResult>(r);

		}


		/*Test for DB concurrency in edit
		[Fact]
		public async void EditDbConcurrencyErrorTest()
		{
			//Arrange           
			var db = MockDb.CreateMockDb();
			var c = new CoachesController(db);

			var coach = new Coaches { Coaches_Id = 2, LastName = "Mar", Title = "Head", Team_Id = 2 };

			//Act
			await c.Create(coach);

			coach.FirstName = " micheal";

			var r = await c.Edit(db.Coaches.Find(2).Coaches_Id, coach);

			//Assert
			var result = Assert.IsType<ViewResult>(r);
			var model = Assert.IsAssignableFrom<Coaches>(result.ViewData.Model);
			Assert.Equal(coach, model);
		}*/
	}
}
