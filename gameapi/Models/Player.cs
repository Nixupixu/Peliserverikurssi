using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

using gameapi.Models;

namespace gameapi
{
    public class Player
    {
        [BsonId]
        public Guid _id {get; set;}
        
        public string _Name {get; set;}
        public int _Level {get;set;}
        public int _Score {get; set;}

        public List<Item> _Items = new List<Item>();
        public List<string> _Tags = new List<string>();
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
    }
}