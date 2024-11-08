using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.DAL
{
    public class MongoStudent
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }  // Primary key for MongoDB

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
