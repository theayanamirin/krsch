using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcApplication1.Models
{
    public class ShoppingCart
    {
        [Key]
        [ForeignKey("Order")]
        public int Id { get; set; }
        public bool CheckedOut { get; set; }
       
        [ForeignKey("UserProfile")]
        public int UserId { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public virtual ICollection<CartItems> CartItems { get; set; }
        public virtual Orders Order { get; set; }
    }
}