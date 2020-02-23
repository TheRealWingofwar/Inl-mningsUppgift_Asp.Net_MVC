using System;

namespace InlämningsUppgift_Asp.Net_MVC.Models
{
    public class Post
    {
        public int PostId { get; set; }

        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

    }
}