using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
        public virtual int HomeTeam_Id { get; set; }
        public virtual Teams HomeTeam_ { get; set; }
        [Required]
        public virtual int AwayTeam_Id { get; set; }
        public virtual Teams AwayTeam_ { get; set; }
        [Required]
        public virtual DateTime MatchDate { get; set; }
        public virtual int Stadium_Id { get; set; }
        public virtual Stadium Stadium_ { get; set; }
    }
}
