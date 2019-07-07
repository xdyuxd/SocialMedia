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
        public int Id { get; set; }
        public string Cover_pic { get; set; }
        public string Profile_pic { get; set; }
        public string Bio { get; set; }
        public Dictionary<int, Post> Gallery { get; set; }

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