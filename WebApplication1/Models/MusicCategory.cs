using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class MusicCategory
    {
        public int MusicCategoryId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ViewMusic> ViewMusics { get; set; }
    }

}