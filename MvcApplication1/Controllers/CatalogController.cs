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
    public class CatalogController : Controller
    {

        private UsersContext datab = new UsersContext();
        //
        // GET: /Catalog/

        public ActionResult Index()
        {
            return View();
        }


            //
            // GET: /Catalog/
            public ActionResult Genres()
            {
                 return View();
            }

        public ActionResult Books(int? id)
        {
            int pid = id == null ? 1 : (int)id;
            if (datab.Genres.Find(pid) == null)
                return View("Error");
            var model = datab.Genres.Find(pid).Book.ToList();
            var query = from b in datab.Genres
                        where b.GenreId == pid
                        select b;
            ViewBag.GenreTitle = query;
            return View(model);
        }



    }
}
