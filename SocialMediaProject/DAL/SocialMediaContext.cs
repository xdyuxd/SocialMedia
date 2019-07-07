using System;
using System.Collections.Generic;
using SocialMedia.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SocialMedia.DAL
{
    public class SocialMediaContext : DbContext
    {
        public SocialMediaContext() : base("SocialMediaContext")
        {

        }

        public DbSet<Client> clients { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}