using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcApplication1.Models
{
    public class CartItems
    {
        [Key,Column(Order =0)]
        [ForeignKey("Books")]
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        [Key,Column(Order =1)]
        [ForeignKey("Carts")]
        public int CartId { get; set; }

        public virtual Books Books { get; set; }
        public virtual ShoppingCart Carts { get; set; }
    }
}