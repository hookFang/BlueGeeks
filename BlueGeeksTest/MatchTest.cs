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
    }
}
