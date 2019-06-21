using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialMedia.Models
{
    public class Shout
    {
        public Client owner { get; set; }
        public string text { get; set; }

        public string timestamp { get; set; }

        public void delete()
        {
            //remove yourself.
        }
    }
}