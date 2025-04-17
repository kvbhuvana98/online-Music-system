using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Feedback
    {
        public int FeedbackId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(1000)]
        public string Message { get; set; }

        public DateTime SubmittedAt { get; set; } = DateTime.Now;
    }

}