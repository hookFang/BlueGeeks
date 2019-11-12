using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueGeeks.Models
{
    public class Coaches
    {
        
        public Coaches() { }
        public Coaches(int Coaches_Id, String FirstName, String LastName, String Title )
        {
            this.Coaches_Id = Coaches_Id;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Title = Title;

        }

        [Key]
        public virtual int Coaches_Id { get; set; }
        [Required]
        public virtual String FirstName { get; set; }
        [Required]
        public virtual String LastName { get; set; }
        [Required]
        public virtual String Title { get; set; }
        public virtual int Team_Id { get; set; }
        public virtual Teams Team { get; set; }
    }
}

