using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Web.Security;

namespace WebApplication1.Controllers
{
    public class AdminsController : Controller
    {


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            using (var db = new MusicStoreContext())
            {
                var user = db.Admins.FirstOrDefault(a => a.Username == admin.Username && a.Password == admin.Password);
                if (user != null)
                {
                    Session["AdminId"] = user.AdminId;
                    Session["AdminUsername"] = user.Username;
                    Session["UserRole"] = "Admin"; // ✅ Add this line
                    Session["IsAdmin"] = true; 
                    return RedirectToAction("Index", "MusicItems");
                }

                else
                {
                    ViewBag.Message = "Invalid credentials.";
                    return View();
                }
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }



        private MusicStoreContext db = new MusicStoreContext();

        // GET: Admins
        public ActionResult Index()
        {

            if (Session["AdminId"] == null)
                return RedirectToAction("Login");

            return View(db.Admins.ToList());
        }

        // GET: Admins/Details/5
        public ActionResult Details(int? id)
        {

            if (Session["AdminId"] == null)
                return RedirectToAction("Login");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // GET: Admins/Create
        public ActionResult Create()
        {

            if (Session["AdminId"] == null)
                return RedirectToAction("Login");

            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdminId,Username,Password")] Admin admin)
        {
            if (Session["AdminId"] == null)
                return RedirectToAction("Login");
            if (ModelState.IsValid)
            {
                db.Admins.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin);
        }

        // GET: Admins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["AdminId"] == null)
                return RedirectToAction("Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdminId,Username,Password")] Admin admin)
        {
            if (Session["AdminId"] == null)
                return RedirectToAction("Login");
            if (ModelState.IsValid)
            {
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin);
        }

        // GET: Admins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["AdminId"] == null)
                return RedirectToAction("Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["AdminId"] == null)
                return RedirectToAction("Login");
            Admin admin = db.Admins.Find(id);
            db.Admins.Remove(admin);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult ViewUserOrders()
        {
            if (Session["AdminId"] == null)
                return RedirectToAction("Login");

            var orders = db.Orders
                           .Include(o => o.User)
                           .Include(o => o.OrderItems.Select(oi => oi.MusicItem))
                           .ToList();

            return View(orders);
        }


        //public ActionResult SalesReport()
        //{
        //    if (Session["UserRole"]?.ToString() != "Admin")
        //        return RedirectToAction("Login", "Admins");

        //    var db = new MusicStoreContext();

        //    var today = DateTime.Today;
        //    var last7 = today.AddDays(-7);
        //    var last30 = today.AddDays(-30);

        //    var report = new SalesReportViewModel
        //    {
        //        TodayTotal = db.Orders.Where(o => o.OrderDate >= today).Sum(o => (decimal?)o.TotalAmount) ?? 0,
        //        Last7DaysTotal = db.Orders.Where(o => o.OrderDate >= last7).Sum(o => (decimal?)o.TotalAmount) ?? 0,
        //        Last30DaysTotal = db.Orders.Where(o => o.OrderDate >= last30).Sum(o => (decimal?)o.TotalAmount) ?? 0,

        //        TodayOrders = db.Orders.Count(o => o.OrderDate >= today),
        //        Last7DaysOrders = db.Orders.Count(o => o.OrderDate >= last7),
        //        Last30DaysOrders = db.Orders.Count(o => o.OrderDate >= last30)
        //    };

        //    return View(report);
        //}

        public ActionResult SalesReport()
        {
            if (Session["UserRole"]?.ToString() != "Admin")
                return RedirectToAction("Login", "Admins");

            var db = new MusicStoreContext();

            var today = DateTime.Today;
            var last7 = today.AddDays(-7);
            var last30 = today.AddDays(-30);

            var report = new SalesReportViewModel
            {
                TodayTotal = db.Orders
                    .Where(o => DbFunctions.TruncateTime(o.OrderDate) == today)
                    .Sum(o => (decimal?)o.TotalAmount) ?? 0,

                Last7DaysTotal = db.Orders
                    .Where(o => DbFunctions.TruncateTime(o.OrderDate) >= last7)
                    .Sum(o => (decimal?)o.TotalAmount) ?? 0,

                Last30DaysTotal = db.Orders
                    .Where(o => DbFunctions.TruncateTime(o.OrderDate) >= last30)
                    .Sum(o => (decimal?)o.TotalAmount) ?? 0,

                TodayOrders = db.Orders
                    .Count(o => DbFunctions.TruncateTime(o.OrderDate) == today),

                Last7DaysOrders = db.Orders
                    .Count(o => DbFunctions.TruncateTime(o.OrderDate) >= last7),

                Last30DaysOrders = db.Orders
                    .Count(o => DbFunctions.TruncateTime(o.OrderDate) >= last30)
            };

            return View(report);
        }


        //public ActionResult ChangeDeliveryStatus(int orderId)
        //{
        //    var order = db.Orders.FirstOrDefault(o => o.OrderId == orderId);
        //    if (order == null)
        //    {
        //        TempData["Error"] = "Order not found.";
        //        return RedirectToAction("OrderList"); // Or your specific order list page
        //    }

        //    if (order.DeliveryStatus != "Delivered")
        //    {
        //        order.DeliveryStatus = "Delivered";
        //        db.SaveChanges();
        //        TempData["Success"] = "Order marked as delivered.";
        //    }
        //    else
        //    {
        //        TempData["Error"] = "Order is already delivered.";
        //    }

        //    return RedirectToAction("OrderList"); // Redirect back to the orders page
        //}

        public ActionResult ChangeDeliveryStatus(int orderId)
        {
            var order = db.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
            {
                TempData["Error"] = "Order not found.";
                return RedirectToAction("OrderList");
            }

            if (order.DeliveryStatus != "Delivered")
            {
                order.DeliveryStatus = "Delivered";  // Update to "Delivered"
                db.SaveChanges();
                TempData["Success"] = "Order marked as delivered.";
            }
            else
            {
                TempData["Error"] = "Order is already delivered.";
            }

            return RedirectToAction("ViewUserOrders");  // Redirect back to order list
        }

        public ActionResult UserList()
        {
            if (Session["AdminId"] == null)
                return RedirectToAction("Login", "Admins");

            var users = db.Users.ToList(); // Get all users from the database
            return View(users); // Pass users list to the view
        }


        // GET: Admins/DeleteUser/5
        // GET: Admins/DeleteUser/5
        public ActionResult DeleteUser(int? id)
        {
            if (Session["AdminId"] == null)
                return RedirectToAction("Login", "Admins");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user); // Return the user details to the view
        }

        // POST: Admins/DeleteUser/5
        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUserConfirmed(int id)
        {
            if (Session["AdminId"] == null)
                return RedirectToAction("Login", "Admins");

            User user = db.Users.Find(id);
            if (user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }

            return RedirectToAction("UserList"); // Redirect to the user list page after deletion
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
