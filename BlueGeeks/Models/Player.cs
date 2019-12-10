using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
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
		[Display(Name = "First Name")]
		public virtual String FirstName { get; set; }
		[Required]
		[Display(Name = "Last Name")]
		public virtual String LastName { get; set; }
		[Required]
		[Display(Name = "Position")]
		public virtual String Position { get; set; }
		[Required]
		[Display(Name = "Jersey Number")]
		public virtual short JerseyNumber { get; set; }

		public virtual int TeamId { get; set; }

        [JsonIgnore]
		[Display(Name = "Team")]
		public virtual Teams Team { get; set; }

	}
}
