using WebApplication1.Models;

namespace WebApplication1.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int MusicItemId { get; set; }
        public int Quantity { get; set; }

        public virtual Order Order { get; set; }
        public virtual MusicItem MusicItem { get; set; }
    }
}