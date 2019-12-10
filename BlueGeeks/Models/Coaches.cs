using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
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
		[Display(Name = "First Name")]
		public virtual String FirstName { get; set; }
        [Required]
		[Display(Name = "Last Name")]
		public virtual String LastName { get; set; }
        [Required]
        public virtual String Title { get; set; }
        
        public virtual int Team_Id { get; set; }
        [JsonIgnore]
		[Display(Name = "Team")]
		public virtual Teams Team_ { get; set; }
    }
}

