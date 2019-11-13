using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BlueGeeks.Models;

namespace BlueGeeks.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
		public DbSet<BlueGeeks.Models.Coaches> Coaches { get; set; }
		public DbSet<BlueGeeks.Models.Matches> Matches { get; set; }
		public DbSet<BlueGeeks.Models.Player> Player { get; set; }
		public DbSet<BlueGeeks.Models.PlayerStatistics> PlayerStatistics { get; set; }
	}
}
