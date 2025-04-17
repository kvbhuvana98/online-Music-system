using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class MusicItemsController : Controller
    {

        private MusicStoreContext db = new MusicStoreContext();

        // GET: MusicItems
        public ActionResult Index()
        {
            if (Session["AdminId"] == null)
            {
                return RedirectToAction("Login", "Admins");
            }
            return View(db.MusicItems.ToList());
        }

        // GET: MusicItems/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["AdminId"] == null)
            {
                return RedirectToAction("Login", "Admins");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MusicItem musicItem = db.MusicItems.Find(id);
            if (musicItem == null)
            {
                return HttpNotFound();
            }
            return View(musicItem);
        }

        // GET: MusicItems/Create
        public ActionResult Create()
        {
            if (Session["AdminId"] == null)
            {
                return RedirectToAction("Login", "Admins");
            }
            return View();
        }

        // POST: MusicItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MusicItemId,Title,Category,Artist,ReleaseDate,Price")] MusicItem musicItem, HttpPostedFileBase ImageFile)
        {
            if (Session["AdminId"] == null)
            {
                return RedirectToAction("Login", "Admins");
            }

            if (ModelState.IsValid)
            {
                // Save the uploaded image
                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    string fileName = Guid.NewGuid() + System.IO.Path.GetExtension(ImageFile.FileName);
                    string path = Server.MapPath("~/Images/");
                    System.IO.Directory.CreateDirectory(path); // Ensure folder exists
                    string fullPath = System.IO.Path.Combine(path, fileName);
                    ImageFile.SaveAs(fullPath);
                    musicItem.ImagePath = "/Images/" + fileName;
                }

                db.MusicItems.Add(musicItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(musicItem);
        }


        // GET: MusicItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["AdminId"] == null)
            {
                return RedirectToAction("Login", "Admins");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MusicItem musicItem = db.MusicItems.Find(id);
            if (musicItem == null)
            {
                return HttpNotFound();
            }
            return View(musicItem);
        }

        // POST: MusicItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
[ValidateAntiForgeryToken]
public ActionResult Edit([Bind(Include = "MusicItemId,Title,Category,Artist,ReleaseDate,Price")] MusicItem musicItem, HttpPostedFileBase ImageFile)
{
    if (Session["AdminId"] == null)
    {
        return RedirectToAction("Login", "Admins");
    }

    var existingItem = db.MusicItems.Find(musicItem.MusicItemId);
    if (existingItem == null)
    {
        return HttpNotFound();
    }

    if (ModelState.IsValid)
    {
        // Update fields
        existingItem.Title = musicItem.Title;
        existingItem.Category = musicItem.Category;
        existingItem.Artist = musicItem.Artist;
        existingItem.ReleaseDate = musicItem.ReleaseDate;
        existingItem.Price = musicItem.Price;

        // Replace image if new one uploaded
        if (ImageFile != null && ImageFile.ContentLength > 0)
        {
            string fileName = Guid.NewGuid() + System.IO.Path.GetExtension(ImageFile.FileName);
            string path = Server.MapPath("~/Images/");
            System.IO.Directory.CreateDirectory(path);
            string fullPath = System.IO.Path.Combine(path, fileName);
            ImageFile.SaveAs(fullPath);
            existingItem.ImagePath = "/Images/" + fileName;
        }

        db.Entry(existingItem).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index");
    }

    return View(musicItem);
}


        // GET: MusicItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["AdminId"] == null)
            {
                return RedirectToAction("Login", "Admins");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MusicItem musicItem = db.MusicItems.Find(id);
            if (musicItem == null)
            {
                return HttpNotFound();
            }
            return View(musicItem);
        }

        // POST: MusicItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["AdminId"] == null)
            {
                return RedirectToAction("Login", "Admins");
            }
            MusicItem musicItem = db.MusicItems.Find(id);
            db.MusicItems.Remove(musicItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
