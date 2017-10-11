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
    public class HomeController : Controller
    {
        UsersContext db = new UsersContext();
            
        

        public ActionResult Index()
        {

            var query = (from b in db.News
                        orderby b.Added descending
                        select b).Take(3);

            ViewBag.NewsForIndex = query;

            ViewBag.Message = "Нажмите это для регистрации.";

            return View();
        }

        public ActionResult News()
        {
            ViewBag.Message = "Страница описания приложения.";
            var query = from n in db.News
                        orderby n.Added descending
                        select n;
            ViewBag.News = query;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Страница контактов.";

            return View();
        }

        public ActionResult Genres()
        {
            ViewBag.Message = "Выбор жанров";

            return View();
        }

        public ActionResult Authors()
        {
            ViewBag.Message = "Выбор автора по алфавиту";

            return View();
        }
       

        public ActionResult Links()
        {
            ViewBag.Message = "Полезные ссылки";

            return View();
        }

        [Authorize(Roles = "Client")]
        public ActionResult Form()
        {
            ViewBag.Message = "Анкета пользователя";
            var flogin = WebSecurity.CurrentUserId;
            var fname = Request.Params["Firstname"];
            var flastname = Request.Params["Lastname"];
            var fmail = Request.Params["Email"];
            var fbirth = Request.Params["Bthd"];
            var fsex = Request.Params["Sex"];
            var fgenre = Request.Params["Genres"];
            var flike = Request.Params["Books"];
            var fself = Request.Params["Talk"];
            if (fname != null)
            {
                ViewBag.Flogin = flogin;
                ViewBag.Firstname = fname.ToString();
                ViewBag.Lastname = flastname.ToString();
                ViewBag.Email = fmail.ToString();
                ViewBag.Bthd = fbirth.ToString();
                ViewBag.Sex = fsex.ToString();
                ViewBag.Genres = fgenre.ToString();
                ViewBag.Talk = fself.ToString();
                ViewBag.Books = flike.ToString();
               
                Clients client = new Clients();
                client.Added = DateTime.Now;
                client.UserId = ViewBag.Flogin;
                client.FirstName = ViewBag.Firstname;
                client.LastName = ViewBag.LastName;
                client.Sex = ViewBag.Sex;
                client.Birthday = ViewBag.Bthd;
                client.Email = ViewBag.Email;
                client.Genres = ViewBag.Genres;
                client.Preference = ViewBag.Books;
                db.Clients.Add(client);
                db.SaveChanges();
                
            }

            var query = from b in db.Clients
                        where b.UserId == WebSecurity.CurrentUserId
                        select b;

            ViewBag.ClientForm = query;
    
            //    ViewBag.Client = db.Clients;
            return View();
        }

      
        [HttpGet]
        public ActionResult Comment() //для вывода
        {
            ViewBag.IsAdmin = (WebSecurity.CurrentUserName == "Admin");
            ViewBag.Message = "Отзывы";
            var query = from b in db.Comments
                        orderby b.CommentDate descending
                        select b;
            foreach (var item in query)
                db.Entry(item).Reference(p => p.UserProfile).Load();

            ViewBag.Comments = query;

            return View();


        }

   
        [HttpPost]
        public ActionResult Comment(Comment comment) {

            Comment c = new Comment();
            var fcommenttext = Request.Params["Comment"];
            if (fcommenttext != null)
            {
                c.CommentDate = DateTime.Now;
                c.UserId = WebSecurity.CurrentUserId;
                c.CommentText = fcommenttext.ToString();
                c.Hidden = false;
                db.Comments.Add(c);
                db.SaveChanges();
            }
            var query = from b in db.Comments
                        orderby b.CommentDate descending
                        select b;
            foreach (var item in query)
                db.Entry(item).Reference(p => p.UserProfile).Load();

            ViewBag.Comments = query;
            return View();
        }


       
        public ActionResult EditComment(long ticks)
          
        {
            
            DateTime time = new DateTime(ticks);
            var db = new UsersContext();
            var comment = db.Comments.FirstOrDefault(p => p.CommentDate
           == time);
            if (comment != null)
            {
                comment.Hidden = !comment.Hidden;
                db.SaveChanges();
            }
            return RedirectToAction("Comment", "Home");
        }

        [Authorize(Users = "Admin")]
        public ActionResult DeleteComment(int Id)

        {

            Comment comment = db.Comments.Find(Id);
            db.Comments.Remove(comment);
            db.SaveChanges();

            return RedirectToAction("Comment", "Home");
        }

      
        public ActionResult MyOrders()
        {
            var query = from b in db.Orders
                        where b.UserId == WebSecurity.CurrentUserId
                        select b;
            if (query.FirstOrDefault() == null)
            {
                ViewBag.MyOrders = null;
            }
            else { ViewBag.MyOrders = query; }
           
           
            return View();
        }
        public class OrderInfo
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
                          where order.UserId == WebSecurity.CurrentUserId & order.Id == oid
                          select new OrderInfo
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