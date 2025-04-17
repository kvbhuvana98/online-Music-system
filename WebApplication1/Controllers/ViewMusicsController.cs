using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ViewMusicController : Controller
    {
        private MusicStoreContext db = new MusicStoreContext();

        // List categories with their music
        public ActionResult Index()
        {
            var categories = db.MusicCategories.Include("ViewMusics").ToList();
            return View(categories);
        }

        // GET: ViewMusic/Create
        public ActionResult Create()
        {
            // Populate dropdown list with categories
            ViewBag.MusicCategoryId = new SelectList(db.MusicCategories, "MusicCategoryId", "Name");
            return View();
        }

        // POST: ViewMusic/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewMusic viewMusic)
        {
            if (ModelState.IsValid)
            {
                db.ViewMusics.Add(viewMusic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MusicCategoryId = new SelectList(db.MusicCategories, "MusicCategoryId", "Name", viewMusic.MusicCategoryId);
            return View(viewMusic);
        }
    }
}
