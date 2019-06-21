using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialMedia.Models
{
    public class Client : Person
    {
        private List<Client> friend_list;

        //Methods
        public string cover_pic { get; set; }
        public string profile_pic { get; set; }
        public string nickname { get; set; }
        public string bio { get; set; }
        public Dictionary<int, Post> gallery { get; set; }

        public void addFriend(Client client)
        {
            friend_list.Add(client);
        }

        public void removeFriend(Client client)
        {
            friend_list.Remove(client);
        }
    }
}