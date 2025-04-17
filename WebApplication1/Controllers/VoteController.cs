using System.Web.Mvc;
using System;
using WebApplication1.Models;
using System.Linq;

public class VoteController : Controller
{
    private MusicStoreContext db = new MusicStoreContext();

    public ActionResult Index()
    {
        var items = db.MusicItems.ToList();
        return View(items);
    }

    [HttpPost]
    public ActionResult Vote(int musicItemId)
    {
        var vote = new Vote
        {
            MusicItemId = musicItemId,
            VotedOn = DateTime.Now
        };

        db.Votes.Add(vote);
        db.SaveChanges();
        TempData["Message"] = "Thank you for voting!";

        return RedirectToAction("Index", "Vote");
    }
}
