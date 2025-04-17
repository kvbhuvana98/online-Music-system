using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Collections.Generic;

namespace WebApplication1.Controllers
{
    public class ChartController : Controller
    {
        private MusicStoreContext db = new MusicStoreContext();

        public ActionResult Toppers()
        {
            var topItems = db.Votes
                .GroupBy(v => v.MusicItem)
                .Select(g => new ChartTopperViewModel
                {
                    MusicItem = g.Key,
                    VoteCount = g.Count()
                })
                .OrderByDescending(x => x.VoteCount)
                .Take(10)
                .ToList();

            return View(topItems);
        }
    }
}
