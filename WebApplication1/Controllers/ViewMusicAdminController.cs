using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

public class ViewMusicAdminController : Controller
{
    private MusicStoreContext db = new MusicStoreContext();

    public ActionResult Create()
    {
        ViewBag.MusicCategoryId = new SelectList(db.MusicCategories, "MusicCategoryId", "Name");
        return View();
    }

    [HttpPost]
    public ActionResult Create(ViewMusic music)
    {
        if (Session["IsAdmin"] == null || !(bool)Session["IsAdmin"])
            return RedirectToAction("Login", "Admins");
        if (ModelState.IsValid)
        {
            db.ViewMusics.Add(music);
            db.SaveChanges();
            return RedirectToAction("Create");
        }

        ViewBag.MusicCategoryId = new SelectList(db.MusicCategories, "MusicCategoryId", "Name", music.MusicCategoryId);
        return View(music);
    }

    // GET: ViewMusicAdmin/Delete/5
    public ActionResult Delete(int id)
    {
        if (Session["IsAdmin"] == null || !(bool)Session["IsAdmin"])
            return RedirectToAction("Login", "Admins");
        var music = db.ViewMusics.Find(id);
        if (music == null)
        {
            return HttpNotFound();
        }
        return View(music);
    }

    // POST: ViewMusicAdmin/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
       
        var music = db.ViewMusics.Find(id);
        db.ViewMusics.Remove(music);
        db.SaveChanges();
        return RedirectToAction("Index", "ViewMusic");
    }

}
