using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcApplication1.Models
{
    [Table("Genres")]
    public class Genres
    {
       [Key]
        public int GenreId { get; set; }
        public string GenreTitle { get; set; }
        
        public virtual ICollection<Books> Book { get; set; }

    }
}