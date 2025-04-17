using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }

        public int MusicItemId { get; set; }
        [ForeignKey("MusicItemId")]
        public virtual MusicItem MusicItem { get; set; }

        public string CartId { get; set; }
        public int UserId { get; set; }

        public int Quantity { get; set; }
    }
}
