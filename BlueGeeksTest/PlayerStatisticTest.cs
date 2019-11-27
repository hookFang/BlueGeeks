using BlueGeeks.Controllers;
using BlueGeeks.Models;
using Microsoft.AspNetCore.Mvc;
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
	}
}
