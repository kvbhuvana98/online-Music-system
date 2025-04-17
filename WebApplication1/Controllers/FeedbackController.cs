using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

public class FeedbackController : Controller
{
    private MusicStoreContext db = new MusicStoreContext();

    [HttpGet]
    public ActionResult Submit()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Submit(Feedback feedback)
    {
        if (ModelState.IsValid)
        {
            db.Feedbacks.Add(feedback);
            db.SaveChanges();
            ViewBag.Message = "Thank you for your feedback!";
        }
        return View();
    }

    public ActionResult ViewAll()
    {
        if (Session["UserRole"]?.ToString() != "Admin")
        {
            return RedirectToAction("Login", "Admins"); // Or show unauthorized view
        }

        var allFeedbacks = db.Feedbacks.OrderByDescending(f => f.SubmittedAt).ToList();
        return View(allFeedbacks);
    }

}
