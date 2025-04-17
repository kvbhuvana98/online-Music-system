using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class StoreController : Controller
    {
        private MusicStoreContext db = new MusicStoreContext();

        //public ActionResult UserHome()
        //{
        //    if (Session["UserId"] == null)
        //        return RedirectToAction("Login", "Users");

        //    var musicItems = db.MusicItems.ToList(); // Or filter by user if needed
        //    return View(musicItems);
        //}
        public ActionResult UserHome(string searchString, string category)
        {
            var items = db.MusicItems.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                items = items.Where(m => m.Title.Contains(searchString) || m.Artist.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(category))
            {
                items = items.Where(m => m.Category == category);
            }

            ViewBag.Categories = new SelectList(db.MusicItems.Select(m => m.Category).Distinct());
            return View(items.ToList());
        }


        // GET: Store
        public ActionResult Index()
        {

            var items = db.MusicItems.ToList();
            return View(items);
        }

        // GET: Store/Details/5
        public ActionResult Details(int id)
        {
            var item = db.MusicItems.Find(id);
            if (item == null)
                return HttpNotFound();

            return View(item);
        }


 // replace with your actual namespace

public ActionResult ReadOnlyBrowse()
    {
        using (var db = new MusicStoreContext())
        {
            var items = db.MusicItems.ToList();
            return View(items);
        }
    }

}
}
