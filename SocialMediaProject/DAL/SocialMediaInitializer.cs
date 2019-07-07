using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocialMedia.Models;
using System.Diagnostics;

namespace SocialMedia.DAL
{
    public class SocialMediaInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<SocialMediaContext>
    {
        protected override void Seed(SocialMediaContext context)
        {


            //friends.ForEach(f => context.friends.Add(f));
        }
    }
}