using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcApplication1.Models
{

    public class Orders
    {
        [Key]
        [ForeignKey("Carts")]
        public int Id { get; set; }
        [ForeignKey("UserProfile")]
        public int UserId { get; set; }
        public DateTime Added { get; set; }
        public string FIO { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Addr1 { get; set; }
        public string Addr2 { get; set; }
        public string Phone { get; set; }
        public bool Sent { get; set; }
        public DateTime DateSent { get; set; }
        public virtual ShoppingCart Carts { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}