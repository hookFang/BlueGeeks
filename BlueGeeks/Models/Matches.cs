using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlueGeeks.Models
{
    public class Matches
    {

        public Matches() { }
        public Matches(int Matche_Id, DateTime MatchDate)
        {
            this.Matche_Id = Matche_Id;
            this.MatchDate = MatchDate;
        }

        [Key]
        public virtual int Matche_Id { get; set; }
        [Required]
		[Display(Name = "Home Team")]
		public virtual int HomeTeam_Id { get; set; }
        [JsonIgnore]
        public virtual Teams HomeTeam_ { get; set; }
        [Required]
		[Display(Name = "Away Team")]
		public virtual int AwayTeam_Id { get; set; }
        [JsonIgnore]
        public virtual Teams AwayTeam_ { get; set; }
        [Required]
		[Display(Name = "Match Date")]
		public virtual DateTime MatchDate { get; set; }
		[Display(Name = "Stadium")]
		public virtual int Stadium_Id { get; set; }
        [JsonIgnore]
        public virtual Stadium Stadium_ { get; set; }
    }
}
