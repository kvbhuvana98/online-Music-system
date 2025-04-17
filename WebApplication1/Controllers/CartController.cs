using System;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Data.Entity;
using System.Collections.Generic;


namespace WebApplication1.Controllers
{
    public class CartController : Controller
    {
        private MusicStoreContext db = new MusicStoreContext();

        //old index
        //public ActionResult Index()
        //{
        //    if (Session["UserId"] == null)
        //        return RedirectToAction("Login", "Users");

        //    int userId = Convert.ToInt32(Session["UserId"]);
        //    string cartId = GetCartId();

        //    var cartItems = db.CartItems
        //                      .Where(c => c.CartId == cartId && c.UserId == userId)
        //                      .Include(c => c.MusicItem)
        //                      .ToList();

        //    return View(cartItems);
        //}

        public ActionResult Index()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Users");

            int userId = Convert.ToInt32(Session["UserId"]);
            string cartId = GetCartId(); // Retrieve cart ID from session

            // Get the user's cart items for the current session
            var cartItems = db.CartItems
                              .Where(c => c.CartId == cartId && c.UserId == userId)
                              .Include(c => c.MusicItem)
                              .ToList();

            return View(cartItems);
        }



        public ActionResult AddToCart(int id)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Users");

            int userId = Convert.ToInt32(Session["UserId"]);
            string cartId = GetCartId(); // Use the cart ID based on the session or user

            var existingItem = db.CartItems
                                 .FirstOrDefault(c => c.MusicItemId == id && c.CartId == cartId && c.UserId == userId);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                var newItem = new CartItem
                {
                    MusicItemId = id,
                    CartId = cartId, // Ensure CartId is tied to session or user
                    Quantity = 1,
                    UserId = userId // Store the userId for proper association
                };
                db.CartItems.Add(newItem);
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }







        public ActionResult RemoveFromCart(int id)
        {
            var item = db.CartItems.Find(id);
            if (item != null)
            {
                db.CartItems.Remove(item);
                db.SaveChanges();
            }
            return RedirectToAction("Index");

        }

       

        public ActionResult PlaceOrder()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Users");

            int userId = Convert.ToInt32(Session["UserId"]);
            string cartId = GetCartId();

            var cartItems = db.CartItems
                .Include(c => c.MusicItem)
                .Where(c => c.CartId == cartId && c.UserId == userId)
                .ToList();

            if (!cartItems.Any())
                return RedirectToAction("Index", "Cart");

            //var order = new Order
            //{
            //    UserId = userId,
            //    OrderDate = DateTime.Now,
            //    OrderItems = new List<OrderItem>()
            //};

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                
                // or your custom logic
                OrderItems = new List<OrderItem>()
            };

            foreach (var item in cartItems)
            {
                var orderItem = new OrderItem
                {
                    MusicItemId = item.MusicItemId,
                    Quantity = item.Quantity
                };
                order.OrderItems.Add(orderItem);
            }



            // ✅ Fix total amount calculation
            order.TotalAmount = cartItems.Sum(ci => ci.MusicItem.Price * ci.Quantity);

            db.Orders.Add(order);
            db.CartItems.RemoveRange(cartItems);
            db.SaveChanges();

            TempData["OrderSuccess"] = "Your order has been placed!";
            return RedirectToAction("OrderSuccess");
        }






        [HttpPost]
        //public ActionResult ConfirmOrder()
        //{
        //    if (Session["UserId"] == null)
        //    {
        //        return RedirectToAction("Login", "Users");
        //    }

        //    int userId = Convert.ToInt32(Session["UserId"]);

        //    var cartItems = db.CartItems
        //                      .Where(c => c.UserId == userId)
        //                      .ToList();

        //    if (!cartItems.Any())
        //    {
        //        return RedirectToAction("Index"); // Nothing to confirm
        //    }

        //    // Optional: You can create an order record here in an "Orders" table

        //    // Clear the user's cart

        //    var order = new Order
        //    {
        //        UserId = userId,
        //        OrderDate = DateTime.Now,
        //        DeliveryStatus = "Processing", // Set status to 'Processing' initially
        //        OrderItems = new List<OrderItem>()
        //    };
        //    order.TotalAmount = cartItems.Sum(ci => ci.MusicItem.Price * ci.Quantity);

        //    db.CartItems.RemoveRange(cartItems);
        //    db.SaveChanges();



        //    TempData["OrderSuccess"] = "Thank you! Your order has been placed successfully.";
        //    return RedirectToAction("OrderSuccess");
        //}

        public ActionResult ConfirmOrder()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Users");
            }

            int userId = Convert.ToInt32(Session["UserId"]);

            var cartItems = db.CartItems
                              .Include(c => c.MusicItem) // ✅ Include related price info
                              .Where(c => c.UserId == userId)
                              .ToList();

            if (!cartItems.Any())
            {
                return RedirectToAction("Index");
            }

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                TotalAmount = 123,
                DeliveryStatus = "Processing",
                OrderItems = new List<OrderItem>()
            };

            foreach (var item in cartItems)
            {
                order.OrderItems.Add(new OrderItem
                {
                    MusicItemId = item.MusicItemId,
                    Quantity = item.Quantity
                });
            }

            order.TotalAmount = 500;

            db.Orders.Add(order);
            db.CartItems.RemoveRange(cartItems);
            db.SaveChanges();

            TempData["OrderSuccess"] = "Thank you! Your order has been placed successfully.";
            return RedirectToAction("OrderSuccess");
        }


        public ActionResult OrderSuccess()
        {
            return View();
        }






        //public ActionResult BuyNow(int id)
        //{
        //    var cartItem = db.CartItems.Include("MusicItem").FirstOrDefault(c => c.CartItemId == id);
        //    if (cartItem == null)
        //        return HttpNotFound();

        //    // You can show a summary or process order here
        //    return View(cartItem);
        //}
        public ActionResult BuyNow(int id)
        {
            var cartItem = db.CartItems.Include(c => c.MusicItem).FirstOrDefault(c => c.CartItemId == id);

            if (cartItem == null)
            {
                return HttpNotFound();
            }

            return View(cartItem);
        }






        //[HttpPost]
        //public ActionResult ConfirmBuyNow(int cartItemId)
        //{
        //    var item = db.CartItems.FirstOrDefault(c => c.CartItemId == cartItemId);
        //    if (item == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    // (Optional) Create order record here

        //    db.CartItems.Remove(item);
        //    db.SaveChanges();

        //    TempData["OrderSuccess"] = "You have successfully purchased the item!";
        //    return RedirectToAction("OrderSuccess");
        //}
        [HttpPost]
        public ActionResult ConfirmBuyNow(int cartItemId)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Users");

            int userId = Convert.ToInt32(Session["UserId"]);
            var item = db.CartItems.Include("MusicItem").FirstOrDefault(c => c.CartItemId == cartItemId && c.UserId == userId);

            if (item == null)
                return HttpNotFound();

            // 🔥 Create Order
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                OrderItems = new List<OrderItem>()
            };



            var orderItem = new OrderItem
            {
                MusicItemId = item.MusicItemId,
                Quantity = item.Quantity
            };

            order.OrderItems.Add(orderItem);
            db.Orders.Add(order);

            // Remove the cart item after order is created
            db.CartItems.Remove(item);

            db.SaveChanges();

            TempData["OrderSuccess"] = "You have successfully purchased the item!";
            return RedirectToAction("OrderSuccess", "Cart"); // 👈 Redirect to user's order page
        }

        public ActionResult IncreaseQuantity(int id)
        {
            var item = db.CartItems.Find(id);
            if (item != null)
            {
                item.Quantity++;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult DecreaseQuantity(int id)
        {
            var item = db.CartItems.Find(id);
            if (item != null && item.Quantity > 1)
            {
                item.Quantity--;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }






        private string GetCartId()
        {
            if (Session["CartId"] == null)
            {
                Session["CartId"] = System.Guid.NewGuid().ToString();
            }
            return Session["CartId"].ToString();
        }
    }
}
