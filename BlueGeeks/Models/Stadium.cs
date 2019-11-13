using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueGeeks.Models
{
    public class Stadium
    {
        public Stadium() { }
        public Stadium(int Stadium_Id, String StadiumName, String City)
        {
            this.Stadium_Id = Stadium_Id;
            this.StadiumName = StadiumName;
            this.City = City;

        }

        [Key]
        public virtual int Stadium_Id { get; set; }
        [Required]
        public virtual String StadiumName { get; set; }
        [Required]
        public virtual String City { get; set; }
        public virtual int Team_Id { get; set; }
        public virtual Teams Team { get; set; }
    }



}
    
    

