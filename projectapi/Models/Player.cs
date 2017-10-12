using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

using projectapi.Models;

namespace projectapi
{
    public class Player
    {
        [BsonId]
        public Guid _id {get; set;}
        
        public string _Name {get; set;}
        public string _Password {get; set;}

        //public List<Item> _Items = new List<Item>();
        public List<Character> _Characters = new List<Character>();
    }

    public class ModifiedPlayer
    {
        [Required]
        public string _Name {get; set; }
    }

    public class NewPlayer
    {
        [Required]
        public string _Name {get; set;}
        [Required]
        public string _Password;
    }
}