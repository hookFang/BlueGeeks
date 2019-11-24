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


            var stadiums = new Stadium { Stadium_Id = 2, StadiumName = "Ever After", City = "Everett", Team_Id = 2 };
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
    }

    
}
