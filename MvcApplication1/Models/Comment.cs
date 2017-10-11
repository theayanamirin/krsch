using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class Comment
    {
        public int Id { get; set; }
        //public string UserName { get; set; }
        public DateTime CommentDate { get; set; }
        public string CommentText { get; set; }
        public bool Hidden { get; set; }

        public int UserId { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}