using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Http;
using SocialMedia.Models;
using SocialMedia.App_Start;
using MongoDB.Driver;
using MongoDB.Bson;

namespace SocialMedia.Controllers
{
    [RoutePrefix("api/client")]
    public class ClientApiController : ApiController
    {
        DatabaseConfig dbconfig = new DatabaseConfig();
        //public IMongoClient db = dbconfig.GetDatabase();
        //IMongoCollection<Client> collection = db.GetCollection<Client>("clients");

        //Added Client
        //Client c = new Client() { Name = "Shadow" };
        //collection.InsertOne(c);

        //Delete All Named Shadow
        //collection.DeleteMany(Builders<Client>.Filter.Where(x => x.Name == "Shadow"));

        //Search by Name or something
        //var list = collection.Find(Builders<Client>.Filter.Where(x => x.Name == "Yuri")).ToList();
        //list.ForEach(x => Debug.WriteLine(new JavaScriptSerializer().Serialize(x)));

        //Get all Clients
        //var list = collection.Find(new BsonDocument()).ToList();
        //list.ForEach(x => Debug.WriteLine(new JavaScriptSerializer().Serialize(x)));

        [HttpGet]
        [Route("")]
        public List<string> GetAllClient()
        {
            //var clients = db.GetCollection<Client>("clients");

            var a = new List<string>();
            a.Add("a");
            return a;
        }

        [HttpGet]
        [Route("{id}")]
        public List<string> GetClient()
        {
            var a = new List<string>();
            a.Add("a");
            return a;
        }
    }
}
