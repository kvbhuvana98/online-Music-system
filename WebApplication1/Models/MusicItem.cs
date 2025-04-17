using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class MusicItem
    {
        [Key]
        public int MusicItemId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Artist { get; set; }

        public DateTime ReleaseDate { get; set; }

        public decimal Price { get; set; }
        public string ImagePath { get; set; }

    }
}