using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class SalesReportViewModel
    {
        public decimal TodayTotal { get; set; }
        public decimal Last7DaysTotal { get; set; }
        public decimal Last30DaysTotal { get; set; }

        public int TodayOrders { get; set; }
        public int Last7DaysOrders { get; set; }
        public int Last30DaysOrders { get; set; }
    }

}