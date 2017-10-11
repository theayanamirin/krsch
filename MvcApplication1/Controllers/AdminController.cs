using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;
using System.Web.Security;
using WebMatrix.WebData;
using MvcApplication1.Filters;
using System.Data;
namespace MvcApplication1.Controllers
{
    public class AdminController : Controller
    {

        UsersContext db = new UsersContext();
        //
        // GET: /Admin/


        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Users = "Admin")]
        public ActionResult ManageBooks()
        {
            var ftitle = Request.Params["Title"];
            var fauthor = Request.Params["Author"];
            var fprice = Request.Params["Price"];
            var fgenre = Request.Params["Genres"];
            var fdescr = Request.Params["Description"];
            if (ftitle != null)
            {
                ViewBag.Title = ftitle.ToString();
                ViewBag.Author = fauthor.ToString();
                ViewBag.Price = Convert.ToInt32(fprice);
                ViewBag.Genres = Convert.ToInt32(fgenre);


                Books book = new Books();
                book.Title = ViewBag.Title;
                book.Author = ViewBag.Author;
                book.Price = ViewBag.Price;
                book.GenreId = ViewBag.Genres;
                book.Description = fdescr.ToString();
                ;
                db.Books.Add(book);
                db.SaveChanges();

            }

            var query = from b in db.Books
                        orderby b.Title descending
                        select b;
            var queryg = from b in db.Genres
                         select b;
            foreach (var item in query)
                db.Entry(item).Reference(p => p.Genre).Load();
            ViewBag.AllBooks = query;
            ViewBag.AllGenres = queryg;

            return View();
        }

        public ActionResult ToEditBook(int? id)
        {
           int pid = (int)id;
           return RedirectToAction("EditBook","Admin",new { id = pid });
        }
        [HttpGet]
        public ActionResult EditBook(int? id)
        {
            int pid = (int)id;
            Books book = db.Books.Find(pid);
            if (book != null)
            {
                return View(book);
            }
            return RedirectToAction("ManageBooks", "Admin");
        }
        [HttpPost]
        public ActionResult EditBook(Books book)
        {
            db.Entry(book).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ManageBooks", "Admin");
        }

        public ActionResult DeleteBook(int Id)

        {

            Books book = db.Books.Find(Id);
            db.Books.Remove(book);
            db.SaveChanges();

            return RedirectToAction("ManageBooks", "Admin");
        }

        [Authorize(Users = "Admin")]
        public ActionResult ManageGenres()
        {
            var fgenre = Request.Params["Genre"];
            if (fgenre != null)
            {
                ViewBag.Genre = fgenre.ToString();

                Genres genre = new Genres();
                genre.GenreTitle = ViewBag.Genre;
                ;
                db.Genres.Add(genre);
                db.SaveChanges();

            }

            var query = from b in db.Genres
                        orderby b.GenreTitle descending
                        select b;
            ViewBag.AllGenres = query;

            return View();
        }

        public ActionResult DeleteGenre(int Id)

        {

            Genres genre = db.Genres.Find(Id);
            db.Genres.Remove(genre);
            db.SaveChanges();

            return RedirectToAction("ManageGenres", "Admin");
        }


        [HttpGet]
        public ActionResult EditGenre(int? id)
        {
            int pid = (int)id;
            Genres genre = db.Genres.Find(pid);
            if (genre != null)
            {
                return View(genre);
            }
            return RedirectToAction("ManageGenres", "Admin");
        }
        [HttpPost]
        public ActionResult EditGenre(Genres genre)
        {
            db.Entry(genre).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ManageGenres", "Admin");
        }




        public class CustomerInfo
        {
            public int UserId { get; set; }
            public string UserName { get; set; }
            public int RoleId { get; set; }
        }

        [Authorize(Users = "Admin")]
        public ActionResult ManageUsers()
        {
           
              var query = db.UserProfiles.Join(db.webpages_UsersInRoles,
              b => b.UserId,
              c => c.UserId,
              (b, c) => new CustomerInfo
              {

                  UserId = b.UserId,
                  UserName=b.UserName,
                  RoleId = c.RoleId


                }).ToList();



            ViewBag.Clients = query;
            return View();
        }

        [Authorize(Users ="Admin")]
        public ActionResult ManageNews()
        {
            var fntitle = Request.Params["NewsTitle"];
            var fntext = Request.Params["NewsText"];
            if (fntitle != null)
            {
                ViewBag.Ntitle = fntitle.ToString();
                ViewBag.Ntext = fntext.ToString();

                News news = new News();
                news.Added = DateTime.Now;
                news.Title = ViewBag.Ntitle;
                news.Text = ViewBag.Ntext;
                ;
                db.News.Add(news);
                db.SaveChanges();

            }

            var query = from b in db.News
                        orderby b.Added descending
                        select b;
            ViewBag.AllNews = query;

            return View();
        }

        public ActionResult DeleteNews(int Id)
        {
            News news = db.News.Find(Id);
            db.News.Remove(news);
            db.SaveChanges();

            return RedirectToAction("ManageNews", "Admin");
        }

        [HttpGet]
        public ActionResult EditNews(int? id)
        {
            int pid = (int)id;
            News news = db.News.Find(pid);
            if (news != null)
            {
                return View(news);
            }
            return RedirectToAction("ManageNews", "Admin");
        }
        [HttpPost]
        public ActionResult EditNews(News news)
        {
            db.Entry(news).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ManageNews", "Admin");
        }

        public ActionResult SetRole(int Id)
        {
            webpages_UsersInRoles role = new webpages_UsersInRoles();
            webpages_UsersInRoles role1 = db.webpages_UsersInRoles.Find(Id, 3);
            db.webpages_UsersInRoles.Remove(role1);
            role.UserId = Id;
            role.RoleId = 2;
            db.webpages_UsersInRoles.Add(role);
            db.SaveChanges();



            return RedirectToAction("ManageUsers", "Admin");
        }
        public ActionResult DelManagerRole(int Id)
        {
            webpages_UsersInRoles role = new webpages_UsersInRoles();
            webpages_UsersInRoles role1 = db.webpages_UsersInRoles.Find(Id, 2);
            db.webpages_UsersInRoles.Remove(role1);
            role.UserId = Id;
            role.RoleId = 3;
            db.webpages_UsersInRoles.Add(role);
            db.SaveChanges();
            return RedirectToAction("ManageUsers", "Admin");
        }




    }
}
