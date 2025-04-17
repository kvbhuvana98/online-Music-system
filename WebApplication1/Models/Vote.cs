using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Vote
    {
        public int VoteId { get; set; }
        public int MusicItemId { get; set; }
        public virtual MusicItem MusicItem { get; set; }
        public DateTime VotedOn { get; set; }
    }

}