using System.Collections.Generic;
using System;
using WebApplication1.Models;
using static System.Data.Entity.Migrations.Model.UpdateDatabaseOperation;

namespace WebApplication1.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual User User { get; set; }

        public decimal TotalAmount { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        //public string DeliveryStatus { get; set; } // e.g. "Processing", "Shipped", "Delivered"
        public string DeliveryStatus { get; set; } = "Processing";


        //        Add-Migration AddDeliveryStatusToOrder
        //Update-Database

    }
}
