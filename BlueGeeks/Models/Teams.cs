using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlueGeeks.Models
{
    public class Teams
    {
        [Key]
        public virtual int Team_Id { get; set; }
        [Required]
        public virtual String Team_Name { get; set; }
        [Required]
        public virtual String Team_Mascot { get; set; }
        [Required]
        public virtual String Conference { get; set; }
        public virtual short Wins { get; set; }
        public virtual short Loses { get; set; }
        public virtual short Ties { get; set; }
        public virtual short Win_Streak { get; set; }

    }
}
