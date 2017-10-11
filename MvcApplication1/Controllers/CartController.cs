using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;
using System.Web.Security;
using WebMatrix.WebData;
using MvcApplication1.Filters;

namespace MvcApplication1.Controllers
{
    public class CartController : Controller
    {
        UsersContext db = new UsersContext();
        int CUserId = WebSecurity.CurrentUserId;
        //
        // GET: /Cart/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddToCart(int? id)
        {
            int pid = (int)id;
            //  var CUserId = WebSecurity.CurrentUserId;

            var checkExisting = from b in db.ShoppingCarts
                                where b.UserId == CUserId
                                select b;

            var checkNotCheckedOut = from b in db.ShoppingCarts
                                     where b.UserId == CUserId & b.CheckedOut == false
                                     select b;

            if ((checkExisting.FirstOrDefault() == null) || (checkNotCheckedOut.FirstOrDefault() == null))
            {
                ShoppingCart cart = new ShoppingCart();
                cart.UserId = WebSecurity.CurrentUserId;
                cart.CheckedOut = false;
                db.ShoppingCarts.Add(cart);
                db.SaveChanges();
                CartItems item = new CartItems();

                var getThatCartId = from b in db.ShoppingCarts
                                    where b.UserId == CUserId && b.CheckedOut == false
                                    select b.Id;

                item.CartId = getThatCartId.First();
                item.ItemId = pid;
                item.Quantity = 1;
                db.CartItems.Add(item);
                db.SaveChanges();

            }
            else if (checkNotCheckedOut.FirstOrDefault() != null)
            {
                var getThatCartId = from b in db.ShoppingCarts
                                    where b.UserId == CUserId && b.CheckedOut == false
                                    select b.Id;

                CartItems item = new CartItems();
                var checkItem = from b in db.CartItems
                                where b.CartId == getThatCartId.FirstOrDefault() && b.ItemId == pid
                                select b;

                if (checkItem.FirstOrDefault() != null)
                {
                    foreach (var thing in checkItem)
                    {
                        thing.Quantity++;
                    }
                    db.SaveChanges();
                }
                else
                {
                    item.CartId = getThatCartId.First();
                    item.ItemId = pid;
                    item.Quantity = 1;
                    db.CartItems.Add(item);
                    db.SaveChanges();
                }
                /* item.CartId = getThatCartId.First();
                 // item.ItemId = ???
                 item.ItemId = pid;
                 item.Quantity = 1;
                 db.CartItems.Add(item);
                 db.SaveChanges();*/



            }

            return RedirectToAction("ShoppingCart", "Cart");

        }

        public class CartInfo
        {
            public int UserId { get; set; }
            public int CartId { get; set; }
            public int ItemId { get; set; }
            public string BookTitle { get; set; }
            public string BookAuthor { get; set; }
            public int BookPrice { get; set; }
            public int Quantity { get; set; }

        }


        public ActionResult ShoppingCart()
        {
            var checkNotCheckedOut = from b in db.ShoppingCarts
                                     where b.UserId == CUserId & b.CheckedOut == false
                                     select b;
            var getCurrentCartId =   from b in db.ShoppingCarts
                                     where b.UserId == CUserId & b.CheckedOut == false
                                     select b.Id;
            var checkItems =from b in db.CartItems
                            where b.CartId == getCurrentCartId.FirstOrDefault()
                            select b;


            if ((checkNotCheckedOut.FirstOrDefault() == null)||(checkItems.FirstOrDefault()==null)) {
                ViewBag.CartId = 0;

            }
            else
            {
            

                var result = (from cart in db.ShoppingCarts
                          join item in db.CartItems on cart.Id equals item.CartId
                          join book in db.Books on item.ItemId equals book.Id
                          where cart.UserId == WebSecurity.CurrentUserId & cart.CheckedOut == false
                          select new CartInfo
                          {
                              UserId = cart.UserId,
                              CartId = item.CartId,
                              ItemId = item.ItemId,
                              Quantity = item.Quantity,
                              BookAuthor = book.Author,
                              BookPrice = book.Price,
                              BookTitle = book.Title

                          }).ToList();

            var query = from b in db.ShoppingCarts
                        where b.CheckedOut == false & b.UserId == WebSecurity.CurrentUserId
                        select b.Id;
            var query1 = (from items in db.CartItems
                             where items.CartId == getCurrentCartId.FirstOrDefault()
                             select (int)items.Quantity * items.Books.Price).Sum();

                ViewBag.CartId = query.First();
                ViewBag.Total = query1;     
            ViewBag.Carts = result;
           
            }
            return View();
        }

        public ActionResult GotoCheckout(int? id)
        {
            int pid = (int)id;
            return RedirectToAction("Checkout", "Cart");
        }

        public ActionResult Checkout(int? id)
        {
            int cid = (int)id;
            ViewBag.Cart = cid;
            return View();
        }

        public ActionResult EditQuantityPlus(int? id)
        {
            int cid = (int)id;
            var getThatCartId = from b in db.ShoppingCarts
                                where b.UserId == CUserId && b.CheckedOut == false
                                select b.Id;

            CartItems item = new CartItems();
            var checkItem = from b in db.CartItems
                            where b.CartId == getThatCartId.FirstOrDefault() && b.ItemId == cid
                            select b;

            if (checkItem.FirstOrDefault() != null)
            {
                foreach (var thing in checkItem)
                {
                    thing.Quantity++;
                }
                db.SaveChanges();
            }
            return RedirectToAction("ShoppingCart", "Cart");
        }

        public ActionResult EditQuantityMinus(int? id)
        {
            int cid = (int)id;
            var getThatCartId = from b in db.ShoppingCarts
                                where b.UserId == CUserId && b.CheckedOut == false
                                select b.Id;

            var checkItem = from b in db.CartItems
                            where b.CartId == getThatCartId.FirstOrDefault() && b.ItemId == cid
                            select b;

            if (checkItem.FirstOrDefault() != null)
            {
                foreach (var thing in checkItem)
                {
                    thing.Quantity--;
                }
                db.SaveChanges();
            }
            return RedirectToAction("ShoppingCart", "Cart");
        }
        public ActionResult RemoveFromCart(int? id)
        {
            int cid = (int)id;
            var getThatCartId = from b in db.ShoppingCarts
                                where b.UserId == CUserId && b.CheckedOut == false
                                select b.Id;
            int x = getThatCartId.FirstOrDefault();
            CartItems item = (from b in db.CartItems
                             where b.CartId == x && b.ItemId == cid
                             select b).FirstOrDefault();

            db.CartItems.Remove(item);
            db.SaveChanges();


            return RedirectToAction("ShoppingCart", "Cart");
        }
    

        public ActionResult Thanks(int? id)
          {
              int pid = (int)id;
              var fio = Request.Params["FIO"];
              var fmail = Request.Params["Email"];
              var fcountry = Request.Params["Country"];
              var fcity = Request.Params["City"];
              var faddr1 = Request.Params["Addr1"];
              var faddr2 = Request.Params["Addr2"];
              var fphone = Request.Params["Phone"];

              if (fio != null)
              {
                  ViewBag.Fio = fio.ToString() ;
                  ViewBag.Email = fmail.ToString();
                  ViewBag.City = fcity.ToString();
                  ViewBag.Country = fcountry.ToString();
                  ViewBag.Address1 = faddr1.ToString();
                  ViewBag.Address2 = faddr2.ToString();
                  ViewBag.Phone = fphone.ToString();

                  Orders order = new Orders();
                order.Id = pid;
                order.Added = DateTime.Now;
                order.FIO = ViewBag.Fio;
                order.Email = ViewBag.Email;
                order.City = ViewBag.City;
                order.Country = ViewBag.Country;
                order.Addr1 = ViewBag.Address1;
                order.Addr2 = ViewBag.Address2;
                order.Phone = ViewBag.Phone;
                order.UserId = WebSecurity.CurrentUserId;
                order.Sent = false;
                order.DateSent = DateTime.Now;
                  db.Orders.Add(order);
                  db.SaveChanges();

                var result = from b in db.ShoppingCarts
                             where b.Id == pid
                             select b;
                if (result.FirstOrDefault() != null)
                {
                    foreach (var thing in result)
                    {
                        thing.CheckedOut = true;
                    }
                    db.SaveChanges();
                }

            }

              return View();


      }
    }
}
