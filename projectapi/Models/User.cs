using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

using projectapi.Models;

namespace projectapi
{
    public class User
    {
        [BsonId]
        public Guid _id {get; set;}

        public string _Name {get; set;}
        public string _Password {get; set;}

        public List<Character> _Characters = new List<Character>();
    }

    public class ModifiedUser
    {
        public string _Password {get; set;}
    }

    public class NewUser
    {
        public string _Name {get; set;}
        public string _Password {get; set;}
    }
}