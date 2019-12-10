using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlueGeeks.Models
{
    public class Teams
    {
        public Teams() { }
        public Teams(int Team_Id, String Team_Name, String Team_Mascot, String Conference, short Wins, short Loses, short Ties, short Win_Streak)
        {
            this.Team_Id = Team_Id;
            this.Team_Name = Team_Name;
            this.Team_Mascot = Team_Mascot;
            this.Conference = Conference;
            this.Wins = Wins;
            this.Loses = Loses;
            this.Ties = Ties;
            this.Win_Streak = Win_Streak;
        }

        [Key]
        public virtual int Team_Id { get; set; }
        [Required]
		[Display(Name = "Team")]
		public virtual String Team_Name { get; set; }
        [Required]
		[Display(Name = "Mascot")]
		public virtual String Team_Mascot { get; set; }
        [Required]
        public virtual String Conference { get; set; }
        public virtual short Wins { get; set; }
        public virtual short Loses { get; set; }
        public virtual short Ties { get; set; }
		[Display(Name = "Win Streak")]
		public virtual short Win_Streak { get; set; }

    }
}
