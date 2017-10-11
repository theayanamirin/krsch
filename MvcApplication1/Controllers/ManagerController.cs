using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;
using System.Web.Security;
using WebMatrix.WebData;
using MvcApplication1.Filters;
using System.Threading.Tasks;
using System.Net.Mail;

namespace MvcApplication1.Controllers
{
    
    public class ManagerController : Controller
    {
        UsersContext db = new UsersContext();
        //
        // GET: /Manager/

        public ActionResult Index()
        {
            var query = from b in db.Orders
                        orderby b.Added
                        select b;

            ViewBag.AllOrders = query;
            return View();
        }

         public ActionResult SendNotification(int? id)
        {
            int oid = (int)id;
            var query = from b in db.Orders
                        where b.Id == oid
                        select b;
            if (query.FirstOrDefault() != null) { 
                     foreach (var item in query)
                        {
                              item.DateSent = DateTime.Now;
                              item.Sent = true;
                         }
                     db.SaveChanges();

            }
            return RedirectToAction("Index", "Manager");


        }

        public class ManOrderInfo
        {
            public int OrderId { get; set; }
            public int ItemId { get; set; }
            public string BookTitle { get; set; }
            public string BookAuthor { get; set; }
            public int BookPrice { get; set; }
            public int Quantity { get; set; }

        }
        public ActionResult OrderDetails(int? id)
        {
            int oid = (int)id;
            ViewBag.Id = oid;
            var result = (from order in db.Orders
                          join item in db.CartItems on order.Id equals item.CartId
                          join book in db.Books on item.ItemId equals book.Id
                          where order.Id == oid
                          select new ManOrderInfo
                          {
                              OrderId = order.Id,
                              ItemId = item.ItemId,
                              Quantity = item.Quantity,
                              BookAuthor = book.Author,
                              BookPrice = book.Price,
                              BookTitle = book.Title

                          }).ToList();

            var query = (from items in db.CartItems
                         where items.CartId == oid
                         select (int)items.Quantity * items.Books.Price).Sum();
            ViewBag.Detail = result;
            ViewBag.Total = query;
            return View();
        }

    }
}
