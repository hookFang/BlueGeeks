﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlueGeeks.Models
{
	public class Player
	{
		public Player() { }

		public Player(int Player_Id, String FirstName, String LastName, String Position, short JerseyNumber)
		{
			this.Player_Id = Player_Id;
			this.FirstName = FirstName;
			this.LastName = LastName;
			this.Position = Position;
			this.JerseyNumber = JerseyNumber;
		}

		[Key]
		public virtual int Player_Id { get; set; }
		[Required]
		public virtual String FirstName { get; set; }
		[Required]
		public virtual String LastName { get; set; }

		public virtual String Position { get; set; }

		public virtual short JerseyNumber { get; set; }

		public virtual int TeamId { get; set; }
		
		public virtual Teams PlayerTeam { get; set; }

	}
}
