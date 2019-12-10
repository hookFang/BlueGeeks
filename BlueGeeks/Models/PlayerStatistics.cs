using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlueGeeks.Models
{
    public class PlayerStatistics
    {
            public PlayerStatistics() { }
            public PlayerStatistics(int Player_Statistics_Id, float FgPercent, float FtPercent, short ThreePointersMade, short PointsMade, short Rebounds, short Assists, short Steals, short Blocks, short TurnOvers)
            {
                this.Player_Statistics_Id = Player_Statistics_Id;
                this.FgPercent = FgPercent;
                this.FtPercent = FtPercent;
                this.ThreePointersMade = ThreePointersMade;
                this.PointsMade = PointsMade;
                this.Rebounds = Rebounds;
                this.Assists = Assists;
                this.Steals = Steals;
                this.Blocks = Blocks;
                this.TurnOvers = TurnOvers;
            }

            [Key]
            public virtual int Player_Statistics_Id { get; set; }
			[Display(Name = "FG%")]
			public virtual float FgPercent { get; set; }
			[Display(Name = "FT%")]
			public virtual float FtPercent { get; set; }
			[Display(Name = "Three Pointers")]
			public virtual short ThreePointersMade { get; set; }
			[Display(Name = "Total Points")]
			public virtual short PointsMade { get; set; }
            public virtual short Rebounds { get; set; }
            public virtual short Assists { get; set; }
            public virtual short Steals { get; set; }
            public virtual short Blocks { get; set; }
            public virtual short TurnOvers { get; set; }
			[Display(Name = "Player")]
			public virtual int Player_Id{ get; set; }

            [JsonIgnore]
			[Display(Name = "Player")]
			public virtual Player Player_ { get; set; }
        
    }
}
