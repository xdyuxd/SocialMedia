using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialMedia.Models
{
    public class Person
    {
        private string fullname;

        [Display(Name = "Nickname")]
        [Required(ErrorMessage = "O nickname é obrigatorio")]
        public string Nickname { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O nome é obrigatorio")]
        public string Name { get; set; }

        [Display(Name = "Sobrenome")]
        public string Surname { get; set; }

        [BsonDateTimeOptions]
        [Display(Name = "Data de nascimento")]
        [Required(ErrorMessage = "A data de nascimento é obrigatorio")]
        public DateTime Birthdate { get; set; }

        [Required(ErrorMessage = "O email é obrigatorio")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatorio")]
        public string Password { get; set; }

        //Person ()
        //{
        //    setFullname();
        //}

        public void setFullname()
        {
            fullname = Name + ' ' + Surname; 
        }
    }
}