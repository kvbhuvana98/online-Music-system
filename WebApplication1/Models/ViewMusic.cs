using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ViewMusic
    {
        public int ViewMusicId { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string MusicUrl { get; set; } // URL or path to music file
        public string Category { get; set; }
        public virtual MusicCategory MusicCategory { get; set; }
        public int MusicCategoryId { get; set; }
    }

}