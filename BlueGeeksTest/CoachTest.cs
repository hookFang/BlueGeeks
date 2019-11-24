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
    }
}
