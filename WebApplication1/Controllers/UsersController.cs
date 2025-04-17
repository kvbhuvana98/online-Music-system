using System.Web.Mvc;
using WebApplication1.Models;
using System.Linq;
using System.Data.Entity;
using System;


public class UsersController : Controller
{
    private MusicStoreContext db = new MusicStoreContext();

   // GET: Users/Login
   //old
    public ActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Login(User user)
    {
        var existingUser = db.Users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
        if (existingUser != null)
        {
            Session["UserId"] = existingUser.UserId;
            Session["Username"] = existingUser.Username;
            Session["UserRole"] = "User"; // ✅ Add this line
            return RedirectToAction("UserHome", "Store");
        }


        ViewBag.Error = "Invalid credentials!";
        return View();
    }

 





    // GET: Users/Register
    public ActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Register(User user)
    {
        if (ModelState.IsValid)
        {
            db.Users.Add(user);
            db.SaveChanges();
            return RedirectToAction("Login");
        }

        return View(user);
    }

    public ActionResult Logout()
    {
        Session.Clear();
        return RedirectToAction("Login");
    }

    public ActionResult MyOrders()
    {
        if (Session["UserId"] == null)
            return RedirectToAction("Login", "Users");

        int userId = Convert.ToInt32(Session["UserId"]);

        var orders = db.Orders
                       .Where(o => o.UserId == userId)
                       .Include(o => o.OrderItems.Select(oi => oi.MusicItem))
                       .ToList();

        return View(orders);
    }


    [HttpPost]
    public ActionResult CancelOrder(int id)
    {
        if (Session["UserId"] == null)
            return RedirectToAction("Login", "Users");

        var order = db.Orders.Include(o => o.OrderItems).FirstOrDefault(o => o.OrderId == id);

        if (order == null || order.UserId != (int)Session["UserId"])
            return HttpNotFound();

        if (order.DeliveryStatus == "Delivered")
        {
            TempData["Error"] = "Delivered orders cannot be canceled.";
            return RedirectToAction("MyOrders");
        }

        // Optional: if you want to soft-delete or mark as canceled
        order.DeliveryStatus = "Canceled";
        db.SaveChanges();

        TempData["Message"] = "Order canceled successfully.";
        return RedirectToAction("MyOrders");
    }


}
