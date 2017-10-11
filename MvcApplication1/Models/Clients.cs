using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcApplication1.Models
{
      public class Clients
        {
            [Key]
            [ForeignKey("UserProfile")]
            public int UserId { get; set; }
            //public string Login { get; set; }
            public DateTime Added { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Sex { get; set; }
            public string Birthday { get; set; }
            public string Email { get; set; }
            public string Genres { get; set; }
            public string Preference { get; set; }

            public virtual UserProfile UserProfile { get; set; }
        
    }
}