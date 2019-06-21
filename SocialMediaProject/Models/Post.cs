using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialMedia.Models
{
    public class Post
    {
        public Client owner { get; set; }
        //Photo url
        public string url { get; set; }
        public List<Client> like { get; set; }
        public string timestamp { get; set; }


        public int likeCount()
        {
            return like.Count();
        }
    }
}