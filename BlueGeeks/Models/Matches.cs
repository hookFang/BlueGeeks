using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueGeeks.Models
{
    public class Matches
    {

        public Matches() { }
        public Matches(int Matche_Id, String HomeTeam, String AwayTeam, DateTime MatchDate)
        {
            this.Matche_Id = Coaches_Id;
            this.HomeTeam = HomeTeam;
            this.AwayTeam = AwayTeam;
            this.MatchDate = MatchDate;

        }

        [Key]
        public virtual int Matche_Id { get; set; }
        [Required]
        public virtual String HomeTeam { get; set; }
        [Required]
        public virtual String AwayTeam { get; set; }
        [Required]
        public virtual DateTime MatchDate { get; set; }
        public virtual int AwayTeam_Id { get; set; }
        public virtual Stadium AwayTeam { get; set; }
        public virtual int Stadium_Id { get; set; }
        public virtual Stadium AwayTeam { get; set; }
    }
}
