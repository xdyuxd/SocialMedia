using System;

namespace SocialMedia.Models
{
    public class Person
    {
        private string fullname;

        public string nickname { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public DateTime birthdate { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        //Person ()
        //{
        //    setFullname();
        //}

        public void setFullname()
        {
            fullname = name + ' ' + surname; 
        }
    }
}